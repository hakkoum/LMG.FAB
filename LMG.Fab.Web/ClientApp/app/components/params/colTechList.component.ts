import { Component, OnInit, NgZone, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { AppConfig } from '../../../app.config';
import { ParamDetail, ParamTable } from '../../models/paramTable';
import { SharedService } from '../../services/sharedService';
import { StringFormat } from '../../pipes/stringFormat.pipe';
import { BsModalRef, BsModalService, ModalDirective } from 'ngx-bootstrap';
import { ToastOptions, ToastyService } from 'ng2-toasty';
import { TitleService } from '../../services/titleService';
import { CollectionTechnique } from '../../models/collection_technique';

@Component({
    selector: 'colTech-list',
    templateUrl: './colTechList.component.html',
    styleUrls: ['./colTechList.component.css', '../app/app.shared.css']
})
export class ColTechListComponent implements OnInit {
    public selectedParam: CollectionTechnique = new CollectionTechnique();
    public originalParam: CollectionTechnique = new CollectionTechnique();
    public paramList: CollectionTechnique[] = [];
    public searchText: string = "";
    public showEditTab = false;
    public listParamTabClass = "active";
    public editTabClass = "hiddenItem";
    public listParamContentClass = "in active";
    public editContentClass = "";
    public order: string = 'Code';
    public reverse: boolean = false;
    public orderDetail: string = 'Code';
    public reverseDetail: boolean = false;
    public isReadonly: boolean = false;
    public isRowClicked: boolean = false;
    public expandedIndex = -1;
    public showError: boolean = false;
    public existingCodeDetailError: string = "";
    public errors: string[] = [];
    modalEl: any = null;
    @ViewChild('confirmEditModal') confirmEditModal: ModalDirective;
    public modalHeader = "";
    public modalBody = "";
    public modalType: string = "";
    public paramVern: ParamDetail[] = [];
    public paramPell: ParamDetail[] = [];
    public paramGamm: ParamDetail[] = [];

    constructor(private sharedService: SharedService, private config: AppConfig, private stringFormat: StringFormat,
        private toastyService: ToastyService, private modalService: BsModalService, private titleService: TitleService) {
        this.titleService.SetTitle(this.config.getlabel('lbl_title_coll'));
    }

    ngOnInit() {
        this.sharedService.getCollectionTechnique().subscribe((data: any) => {
            this.paramList = data;
        });

        this.sharedService.getParamValues("GAMM").subscribe((data: any) => {
            this.paramGamm = data;
        });
        this.sharedService.getParamValues("VERN").subscribe((data: any) => {
            this.paramVern = data;
        });
        this.sharedService.getParamValues("PELL").subscribe((data: any) => {
            this.paramPell = data;
        });
    }

    addParam() {
        this.selectedParam = new CollectionTechnique();
        this.showEditTab = true;
        this.isReadonly = false;
        this.displayEditTab();
    }

    cancelEditParam() {
        this.isReadonly = true;
        let copy = JSON.parse(JSON.stringify(this.originalParam));
        this.selectedParam = copy;
        this.expandedIndex = -1;
    }

    saveParam(fromModal: boolean): void {

        var toastOptions: ToastOptions = {
            title: this.config.getlabel('lbl_param_save_title'),
            msg: this.config.getlabel('lbl_param_save_succes_msg'),
            showClose: true,
            timeout: 4000
        };
        this.toastyService.clearAll();
        this.toastyService.info(toastOptions);

        this.isReadonly = true;
    }

    deleteParam(param: CollectionTechnique, fromModal: boolean) {
        //if (!fromModal) {
        //    this.selectedParam = param;
        //    this.modalType = "ParamDelete";
        //    this.modalHeader = this.config.getlabel('lbl_param_confirm_delete_title');
        //    this.modalBody = this.stringFormat.transform(this.config.getlabel('lbl_param_confirm_delete_message'), [param.Code]);
        //    this.confirmEditModal.show();
        //    return;
        //}
        //else {
        //    this.confirmEditModal.hide();
        //    this.sharedService.deleteParam(this.selectedParam.PkParamTable).subscribe((data: any) => {
        //        this.paramList.splice(this.paramList.indexOf(this.selectedParam), 1);
        //        var toastOptions: ToastOptions = {
        //            title: this.config.getlabel('lbl_param_save_title'),
        //            msg: this.config.getlabel('lbl_param_save_succes_msg'),
        //            showClose: true,
        //            timeout: 3000
        //        };
        //        this.toastyService.clearAll();
        //        this.toastyService.info(toastOptions);
        //        this.searchText = '';
        //    },
        //        err => this.displayError(err)
        //    );

        //}
    }

    displayError(err: any) {
        var toastOptions: ToastOptions = {
            title: this.config.getlabel('lbl_param_save_title'),
            msg: this.stringFormat.transform(this.config.getlabel('lbl_save_error_msg'), [err.error.Message]),
            showClose: true,
            timeout: 300000000
        };
        this.toastyService.clearAll();
        this.toastyService.error(toastOptions);
    }

    editParam() {
        this.isReadonly = false;
    }

    selectParam(param: CollectionTechnique, readOnly: boolean) {
        let copy = JSON.parse(JSON.stringify(param));
        this.selectedParam = copy;
        this.originalParam = param;
        this.showEditTab = true;
        this.isReadonly = readOnly;
        this.displayEditTab();
    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }

    displayEditTab() {
        this.closeEditTab(true);
        setTimeout(() => {
            this.listParamTabClass = "";
            this.editTabClass = "displayItem active";
            this.editContentClass = "in active";
            this.listParamContentClass = "";
        }, 200);
    }

    closeEditTab(close: boolean) {
        setTimeout(() => {
            if (close) {
                this.showEditTab = false;
                this.listParamTabClass = "active";
                this.editTabClass = "hiddenItem";
                this.listParamContentClass = "in active";
                this.editContentClass = "";
            }
        }, 200);

    }

}
