import { Component, OnInit, Input, SimpleChanges, OnDestroy, Output, EventEmitter } from '@angular/core';
import { Observable, Subscription } from 'rxjs/Rx';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { LotService } from '../../services/lotService';
import { Lot } from "../../models/lot";
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
import { AppConfig } from '../../../app.config';
import { StringFormat } from '../../pipes/stringFormat.pipe';
import { LockService } from '../../services/lockService';
import { TableLock } from '../../models/tableLock';
import { SharedService } from '../../services/sharedService';
import { Commentaire } from '../../models/commentaire';
import { ParamDetail } from '../../models/paramTable';


@Component({
    selector: 'lot-edit',
    templateUrl: './lotEdit.component.html',
    styleUrls: ['./lotEdit.component.css', '../app/app.shared.css']
})
export class LotEditComponent implements OnInit, OnDestroy {
    @Input() lot: Lot;
    @Input() isReadonly: boolean;
    @Output() closeTabAction = new EventEmitter<Boolean>();
    @Output() setLot = new EventEmitter<Lot>();
    public selectedLot: Lot = new Lot();
    public isLocked: boolean = false;
    public _isReadonly: boolean = true;
    public tableLock: TableLock;
    public processusList: ParamDetail[] = [];
    public comments: Commentaire[] = [];
    timerSub: Subscription;
    lockRefreshDelay: number = 60;
    public showError = false;

    constructor(private lotService: LotService, private config: AppConfig, private toastyService: ToastyService,
        private toastyConfig: ToastyConfig, private stringFormat: StringFormat, private lockService: LockService,
        private sharedService: SharedService) {
        this.toastyConfig.position = "top-right";
        this.lockRefreshDelay = 1000 * this.config.getLevel2Key('global', 'AppSettings', 'LockRefreshDelay');
        this.lotService.getProcessusList().subscribe((data: any) => {
            this.processusList = data;
        });

    }

    ngOnInit() {
        let timer = Observable.timer(this.lockRefreshDelay, this.lockRefreshDelay);
        //rafraichir le lock tous les LockRefreshDelay second
        this.timerSub = timer.subscribe(t => this.updateLock());
    }

    updateLock() {
        if (this.lot.PkLot > 0 && this.isLocked == false && this._isReadonly == false) {
            this.lockService.addLock('lot', this.lot.PkLot).subscribe((lock: any) => { console.log('updatelock' + lock) });
        }
    }

    editLot() {
        this.loadLot(false);
    }

    ngOnChanges(changes: SimpleChanges) {
        let chng = changes['lot'];
        if (chng !== undefined && chng !== null) {
            //remove lock from previous lot
            let previousLot = chng.previousValue;
            if (previousLot !== undefined && previousLot !== null) {
                if (previousLot.PkLot > 0) {
                    this.lockService.deleteLock('lot', previousLot.PkLot).subscribe((lock: any) => { console.log('delete lock :' + lock) });;
                }
            }
        }
        if (this.lot.PkLot > 0) {
            this.loadLot(this.isReadonly);
        }
        else {
            this.selectedLot = this.lot;
            this._isReadonly = false;
        }
    }

    ngOnDestroy(): void {
        this.timerSub.unsubscribe();
        if (this.lot.PkLot > 0 && this._isReadonly == false) {
            this.lockService.deleteLock('lot', this.lot.PkLot).subscribe((lock: any) => { console.log('delete lock :' + lock) });;
        }
    }

    loadLot(readOnly: boolean) {
        this.lotService.getLotById(this.lot.PkLot).subscribe((data: any) => {
            this.selectedLot = data;
            this.lockService.getLock('lot', this.lot.PkLot).subscribe((lock: any) => {
                this.tableLock = lock;
                if (this.tableLock === null || this.tableLock === undefined) {
                    this.isLocked = false;
                    this._isReadonly = readOnly;
                    if (!readOnly) {
                        //add lock on lot
                        this.lockService.addLock('lot', this.lot.PkLot).subscribe((lock: any) => { console.log('test' + lock) });
                    }
                }
                else { //lot is locked
                    this.isLocked = true;
                    this._isReadonly = true;
                }
            });
        });

        this.sharedService.getRecordComments('lot', this.lot.PkLot).subscribe((data: any) => {
            this.comments = data;
        });
    }

    getFieldComment(fieldName: string) {
        let comment = this.comments.find(p => p.FieldComm == fieldName);
        if (comment !== undefined) {
            return comment.CommentaireHtml;
        }
        return "";
    }

    getComment(fieldName: string) {
        return this.comments.find(p => p.FieldComm == fieldName);

    }

    changeComment($event: Commentaire) {
        if (this.lot.PkLot > 0) {
            let commentaire = this.comments.find(p => p.FieldComm == $event.FieldComm);
            if (commentaire && commentaire.PkCommentaire > 0) {
                if ($event.CommentaireHtml == undefined || $event.CommentaireHtml == null || $event.CommentaireHtml.trim() == '') {
                    this.comments.splice(this.comments.indexOf(commentaire), 1);
                    this.sharedService.deleteComment(commentaire.PkCommentaire).subscribe((data: any) => {
                        console.log('delete comment ok : ');
                    });
                }
                else {
                    commentaire.CommentaireHtml = $event.CommentaireHtml;
                    commentaire.IsImportant = $event.IsImportant;
                    this.sharedService.updateComment(commentaire.PkCommentaire, commentaire.CommentaireHtml, commentaire.IsImportant).subscribe((data: any) => {
                        console.log('comment is updated');
                    });
                }

            }
            else {
                if ($event.CommentaireHtml.trim() != '') {
                    this.sharedService.addComment('lot', this.lot.PkLot, $event.FieldComm, $event.CommentaireHtml, $event.IsImportant).subscribe((data: any) => {
                        this.comments.push(data);
                        console.log('comment is addded');
                    });
                }
            }
        }
    }


    cancelEdit() {
        this._isReadonly = true;
        this.isLocked = false;
        this.lotService.getLotById(this.lot.PkLot).subscribe((data: any) => {
            this.selectedLot = data;
            this.lockService.deleteLock('lot', this.lot.PkLot).subscribe((lock: any) => { console.log('delete lock : ' + lock) });;
        });
    }

    saveLotAndClose(lotForm: any) {
        if (!lotForm.form.valid || this.selectedLot.DateArriveeLivres == null) {
            this.showError = true;
            var toastOptions: ToastOptions = {
                title: this.config.getlabel('lbl_lot_save_title'),
                msg: $('#formErrors').html.toString(),

                showClose: true,
                timeout: 300000000
            };
            return;
        }
        this.lotService.saveLot(this.selectedLot).subscribe((data: any) => {
            //this.selectedLot = data;
            this.setLot.emit(this.selectedLot);
            this._isReadonly = true;
            this.isLocked = false;
            var toastOptions: ToastOptions = {
                title: this.config.getlabel('lbl_lot_save_title'),
                msg: this.config.getlabel('lbl_lot_save_succes_msg'),
                showClose: true,
                timeout: 3000
            };
            this.toastyService.info(toastOptions);
            setTimeout(() => {
                this.closeTabAction.emit(true);
            }, 1000 * 2);

        },
            err => this.displayError(err)
        );
    }

    displayError(err: any) {
        var toastOptions: ToastOptions = {
            title: this.config.getlabel('lbl_lot_save_title'),
            msg: this.stringFormat.transform(this.config.getlabel('lbl_lot_save_error_msg'), [err.error.Message]),
            showClose: true,
            timeout: 300000000
        };
        this.toastyService.error(toastOptions);
    }

    saveLotAndNew(lotForm: any) {
        if (!lotForm.form.valid) {
            this.showError = true;
            return;
        }
        this.lotService.saveLot(this.selectedLot).subscribe((data: any) => {
            var toastOptions: ToastOptions = {
                title: this.config.getlabel('lbl_lot_save_title'),
                msg: this.config.getlabel('lbl_lot_save_succes_msg'),
                showClose: true,
                timeout: 3000
            };
            this.toastyService.info(toastOptions);
            this.selectedLot = new Lot();
            this._isReadonly = false;
            this.isLocked = false;
        },
            err => this.displayError(err)
        );
    }
}
