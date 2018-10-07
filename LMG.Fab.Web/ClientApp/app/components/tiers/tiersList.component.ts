import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { AppConfig } from '../../../app.config';
import { ParamDetail } from '../../models/paramTable';
import { Tiers, Prestation, Machine } from '../../models/tiers';
import { SharedService } from '../../services/sharedService';
import { ToastOptions, ToastyService } from 'ng2-toasty';
import { TitleService } from '../../services/titleService';

@Component({
    selector: 'tiers-list',
    templateUrl: './tiersList.component.html',
    styleUrls: ['./tiersList.component.css', '../app/app.shared.css']
})
export class TiersListComponent implements OnInit {
    public selectedTiers: Tiers = new Tiers();
    public selectedPresta: Prestation = new Prestation();
    public editMode = false;
    public tiersList: Tiers[] = [];
    public typeTiersList: ParamDetail[] = [];
    public typePrestationList: ParamDetail[] = [];
    public showEditTab = false;
    public searchTabClass = "active";
    public editTabClass = "hiddenItem";
    public nomFilter: string = "";
    public adresseFilter: string = "";
    public typeTiersFilter: number = 0;
    public typePrestationFilter: number = 0;
    public order: string = 'Nom';
    public reverse: boolean = false;
    public expandedIndex = -1;
    public expandedPrestaIndex = -1;
    public isRowClicked: boolean = false;
    public prestationToAdd: number = 0;
    public showAddPresta: boolean = false;

    pagercount: number = 30;
    start: number = 0;

    constructor(private sharedService: SharedService, private config: AppConfig, private toastyService: ToastyService, private titleService: TitleService) {
        this.sharedService.getParamValues("TYPE").subscribe((data: any) => {
            this.typeTiersList = data;
        });
        this.sharedService.getParamValues("TRFO").subscribe((data: any) => {
            this.typePrestationList = data;
            this.sharedService.getParamValues("TREA").subscribe((data: any) => {
                this.typePrestationList = this.typePrestationList.concat(data);
                this.sharedService.getParamValues("TREM").subscribe((data: any) => {
                    this.typePrestationList = this.typePrestationList.concat(data);
                    this.sharedService.getParamValues("TYCO").subscribe((data: any) => {
                        this.typePrestationList = this.typePrestationList.concat(data);
                    });
                });
            });
        });
        this.searchTiers();
        this.titleService.SetTitle(this.config.getlabel('lbl_title_tiers'));
    }

    ngOnInit() {
    }

    searchTiers() {
        this.sharedService.getTiers().subscribe((data: any) => {
            this.tiersList = data;
        });
    }

    selectTiers(tiers: Tiers, selectWithEdit: boolean) {
        this.showEditTab = true;
        this.selectedTiers = tiers;
        this.editMode = selectWithEdit;
        this.displayEditTab();
    }

    addTiers() {
        this.showEditTab = true;
        this.selectedTiers = new Tiers();
        this.displayEditTab();
        this.editMode = true;
    }

    displayEditTab() {
        this.searchTabClass = "";
        this.editTabClass = "displayItem active";
        $('#selectedTiers').addClass('in').addClass('active');
        $('#search').removeClass('in').removeClass('active');
    }

    closeEditTab(close: boolean) {
        setTimeout(() => {
            if (close) {
                this.showEditTab = false;
                this.searchTabClass = "active";
                this.editTabClass = "hiddenItem";
                $('#selectedTiers').removeClass('in').removeClass('active');
                $('#search').addClass('in').addClass('active');
                this.selectedTiers = new Tiers();
            }
        }, 200);
    }

    cancelEditTiers() {
        this.editMode = false;
        this.expandedIndex = -1;
    }

    editTiers() {
        this.editMode = true;
    }

    saveTiers() {
        this.editMode = false;
        var toastOptions: ToastOptions = {
            title: this.config.getlabel('lbl_tiers_save_title'),
            msg: this.config.getlabel('lbl_tiers_save_succes_msg'),
            showClose: true,
            timeout: 3000
        };
        this.toastyService.clearAll();
        this.toastyService.info(toastOptions);
        this.expandedIndex = -1;
    }


    editPrestation(presta: Prestation) {
        this.selectedPresta = presta;
    }

    addPresta() {
        let newPresta = new Prestation();
        newPresta.PkPrestation = 0;
        let val = this.typePrestationList.find(p => p.PkParamDetail == this.prestationToAdd);
        if (val != undefined) {
            newPresta.FkParamDetailTrfo = val.PkParamDetail;
        }
      

        if (this.selectedTiers.Prestation == undefined || this.selectedTiers.Prestation.length == 0) {
            this.selectedTiers.Prestation = [];
        }

        this.selectedTiers.Prestation.unshift(newPresta);

        this.prestationToAdd = 0;
        this.showAddPresta = false;

    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }

    getTiersType(type: number): string {
        let val = this.typeTiersList.find(p => p.PkParamDetail == type);
        if (val != undefined && val != null) {
            return val.LibelleCourt;
        }
        return "";
    }

    getPrestationType(type: number): string {
        let val = this.typePrestationList.find(p => p.PkParamDetail == type);
        if (val != undefined && val != null) {
            return val.LibelleCourt;
        }
        return "";
    }

    addMachine(pres: Prestation) {
        let machine = new Machine();
        if (pres.Machine == undefined || pres.Machine.length == 0) {
            pres.Machine = [];
        }
        pres.Machine.unshift(machine);
        this.expandedIndex = 0;
    }

    deletePresta(index: number) {
        this.selectedTiers.Prestation.splice(index, 1);
    }

    deleteMachine(pres: Prestation, index_machine: number) {
        pres.Machine.splice(index_machine, 1);
        this.expandedIndex = -1;
    }

    expandRow(index: number, PrestaIndex: number): void {
        if (!this.editMode) {
            return;
        }
        if (this.isRowClicked) {
            this.isRowClicked = false;
            return;
        }
        if (index != -1) {
            this.isRowClicked = true;
        }

        this.expandedPrestaIndex = PrestaIndex;
        this.expandedIndex = index;
    }

    showMachine(pres: Prestation) {
        let typePresta = this.typePrestationList.find(p => p.PkParamDetail == pres.FkParamDetailTrfo);
        if (typePresta !== undefined && typePresta !== null ) {
            if (typePresta.Code == 'IMP BANDE' || typePresta.Code == 'IMP HTX' || typePresta.Code == 'IMP JAQ' || typePresta.Code == 'IMPRIMEUR' || typePresta.Code == 'IMP COUV') {
                return true;
            }
        }
        return false;
    }

}
