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

@Component({
    selector: 'param-list',
    templateUrl: './paramList.component.html',
    styleUrls: ['./paramList.component.css', '../app/app.shared.css']
})
export class ParamListComponent implements OnInit {
    public selectedParam: ParamTable = new ParamTable();
    public selectedParamDetail: ParamDetail = new ParamDetail();
    public originalParam: ParamTable = new ParamTable();
    public paramList: ParamTable[] = [];
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

    constructor(private sharedService: SharedService, private config: AppConfig, private stringFormat: StringFormat,
        private toastyService: ToastyService, private modalService: BsModalService, private titleService: TitleService) {
        this.titleService.SetTitle(this.config.getlabel('lbl_title_param'));
    }

    ngOnInit() {
        this.sharedService.getParamTable().subscribe((data: any) => {
            this.paramList = data;
        });
    }

    addParam() {
        this.selectedParam = new ParamTable();
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
        //validate form
        if (!fromModal) {
            this.errors = [];
            this.existingCodeDetailError = "";
            if (this.selectedParam == undefined) {
                this.errors.push(this.config.getlabel('lbl_param_code_validation'));
            }
            if (this.selectedParam.Code == undefined || this.selectedParam.Code.trim() == "") {
                this.errors.push(this.config.getlabel('lbl_param_code_validation'));
            }
            if (this.selectedParam.LibelleCourt == undefined || this.selectedParam.LibelleCourt.trim() == "") {
                this.errors.push(this.config.getlabel('lbl_param_libelleCourt_validation'));
            }
            if (this.paramList.findIndex((p) => p.PkParamTable != this.selectedParam.PkParamTable && p.Code == this.selectedParam.Code) >= 0) {
                this.errors.push(this.stringFormat.transform(this.config.getlabel('lbl_param_existing_code_error'), [this.selectedParam.Code]));
            }
            if (this.selectedParam.ParamDetail.findIndex((p) => p.Code == '') >= 0) {
                this.errors.push(this.config.getlabel('lbl_param_detail_code_validation'));
            }
            if (this.errors.length > 0) {
                this.showError = true;
                return;
            }
            if (this.originalParam.Code != this.selectedParam.Code) {
                this.modalHeader = this.config.getlabel('lbl_param_validation_confirm_code_title');
                this.modalBody = this.config.getlabel('lbl_param_validation_confirm_code_message');
                this.modalType = "CodeChanged";
                this.confirmEditModal.show();
                return;
            }
        }
        else {
            this.confirmEditModal.hide();
        }

        this.isReadonly = true;
        //save to DB
        this.sharedService.saveParam(this.selectedParam).subscribe((data: ParamTable) => {
            var index = this.paramList.findIndex((p) => p.PkParamTable == data.PkParamTable);
            if (index >= 0) {
                this.paramList[index] = data;
                this.originalParam = data;
            }
            else {
                this.order = '';
                this.paramList.unshift(data);
                this.originalParam = data;
            }
            this.searchText = data.Code;
            var toastOptions: ToastOptions = {
                title: this.config.getlabel('lbl_param_save_title'),
                msg: this.config.getlabel('lbl_param_save_succes_msg'),
                showClose: true,
                timeout: 4000
            };
            this.toastyService.clearAll();
            this.toastyService.info(toastOptions);
        },
            error => {
                this.displayError(error)
                this.isReadonly = false;
            }
        );

    }

    confirmModal(type: string) {
        switch (type) {
            case "CodeChanged":
                this.saveParam(true);
            case "ParamDelete":
                this.deleteParam(this.selectedParam, true);
            case "ParamDetailDelete":
                this.deleteParamDetail(this.selectedParamDetail , true);

        }
    }

    deleteParam(param: ParamTable, fromModal: boolean) {
        if (!fromModal) {
            this.selectedParam = param;
            this.modalType = "ParamDelete";
            this.modalHeader = this.config.getlabel('lbl_param_confirm_delete_title');
            this.modalBody = this.stringFormat.transform(this.config.getlabel('lbl_param_confirm_delete_message'), [param.Code]);
            this.confirmEditModal.show();
            return;
        }
        else {
            this.confirmEditModal.hide();
            this.sharedService.deleteParam(this.selectedParam.PkParamTable).subscribe((data: any) => {
                this.paramList.splice(this.paramList.indexOf(this.selectedParam), 1);
                var toastOptions: ToastOptions = {
                    title: this.config.getlabel('lbl_param_save_title'),
                    msg: this.config.getlabel('lbl_param_save_succes_msg'),
                    showClose: true,
                    timeout: 3000
                };
                this.toastyService.clearAll();
                this.toastyService.info(toastOptions);
                this.searchText = '';
            },
                err => this.displayError(err)
            );

        }
    }

    deleteParamDetail(param: ParamDetail, fromModal: boolean) {
        if (!fromModal) {
            this.selectedParamDetail = param;
            this.modalType = "ParamDetailDelete";
            this.modalHeader = this.config.getlabel('lbl_param_confirm_delete_title');
            this.modalBody = this.stringFormat.transform(this.config.getlabel('lbl_param_detail_confirm_delete_message'), [param.Code]);
            this.confirmEditModal.show();
            return;
        }
        else {
            this.confirmEditModal.hide();
            var index = this.selectedParam.ParamDetail.findIndex((p) => p.Code == this.selectedParamDetail.Code);
            if (index >= 0) {
                this.selectedParam.ParamDetail.splice(index , 1);
            }
            this.expandedIndex = -1;
        }
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
        this.expandedIndex = -1;
    }

    addParamDetail() {
        if (this.selectedParam.ParamDetail.length == 0) {
            this.selectedParam.ParamDetail = [];
        }
        let det = new ParamDetail();
        det.Actif = false;
        det.Code = '';
        det.FkParamTable = this.selectedParam.PkParamTable;
        if (this.selectedParam.ParamDetail.find((p) => p.Code == '')) {
            this.showError = true;
            this.existingCodeDetailError = this.stringFormat.transform(this.config.getlabel('lbl_param_detail_existing_code_error'), ['']);
            return;
        }
        this.selectedParam.ParamDetail.unshift(det);
        this.expandedIndex = 0;
        this.expandRow(0);
    }

    selectParam(param: ParamTable, readOnly: boolean) {
        let copy = JSON.parse(JSON.stringify(param));
        this.selectedParam = copy;
        this.originalParam = param;
        this.showEditTab = true;
        this.isReadonly = readOnly;
        this.expandRow(-1);
        this.displayEditTab();
    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }

    setOrderDetail(value: string) {
        if (this.orderDetail === value) {
            this.reverseDetail = !this.reverseDetail;
        }
        this.orderDetail = value;
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

    expandRow(index: number): void {
        console.log('expand row : ' + index);
        if (this.isReadonly) {
            return;
        }
        if (this.isRowClicked) {
            this.isRowClicked = false;
            return;
        }
        if (index != -1) {
            this.isRowClicked = true;
            //this.orderDetail = '';
        }

        this.expandedIndex = index;// === this.expandedIndex ? -1 : index;
    }



}
