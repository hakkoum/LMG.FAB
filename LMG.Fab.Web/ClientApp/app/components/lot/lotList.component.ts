import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { LotService } from '../../services/lotService';
import { Lot } from "../../models/lot";
import { AppConfig } from '../../../app.config';
import { ParamDetail } from '../../models/paramTable';
import { TitleService } from '../../services/titleService';

@Component({
    selector: 'lot-list',
    templateUrl: './lotList.component.html',
    styleUrls: ['./lotList.component.css', '../app/app.shared.css']
})
export class LotListComponent implements OnInit {
    public selectedLot: Lot = new Lot();
    public editMode = false;
    public lots: Lot[] = [];
    public lotsDisplayed: Lot[] = [];
    public processusList: ParamDetail[] = [];
    public showEditTab = false;
    public searchTabClass = "active";
    public editTabClass = "hiddenItem";
    public codeFilter: string = "";
    public libelleFilter: string = "";
    public dateDebutFilter: Date;
    public dateFinFilter: Date;
    public processIdFilter: number;
    public order: string = 'NomLot';
    public reverse: boolean = false;

    pagercount: number = 30;
    start: number = 0;

    constructor(private lotService: LotService, private config: AppConfig, private titleService: TitleService) {
        this.searchLot();
        this.lotService.getProcessusList().subscribe((data: any) => {
            this.processusList = data;
            let processAll = new ParamDetail();
            processAll.PkParamDetail = 0;
            processAll.LibelleCourt = "Tous les processus";
            this.processusList.unshift(processAll);
            this.processIdFilter = 0;
        });
        this.titleService.SetTitle(this.config.getlabel('lbl_title_lots'));
    }

    ngOnInit() {
    }

    selectLot(lot: Lot, selectWithEdit: boolean) {
        this.showEditTab = true;
        this.selectedLot = lot;
        this.editMode = selectWithEdit;
        this.displayEditTab();
    }

    searchLot() {
        this.lotService.searchLot(this.libelleFilter, this.codeFilter, this.dateDebutFilter, this.dateFinFilter, this.processIdFilter).subscribe((data: any) => {
            this.lots = data;
            this.lotsDisplayed = [];
            this.start = 0;
            this.addItem(0);
        });
    }

    addItem(startIndex: number) {
        if (this.pagercount + startIndex < this.lots.length) {
            for (let i = startIndex; i < this.pagercount + startIndex; ++i) {
                this.lotsDisplayed.push(this.lots[i]);
            }
        }
        else {
            if (startIndex == 0) {
                this.lotsDisplayed = this.lots;
            }
        }

    }

    addLot() {
        this.showEditTab = true;
        this.selectedLot = new Lot();
        this.displayEditTab();
    }

    nextLot() {
        if (this.selectedLot.PkLot > 0) {
            let currentIndex = this.lots.indexOf(this.selectedLot);
            if (currentIndex >= 0 && currentIndex < this.lots.length - 1) {
                this.selectedLot = this.lots[currentIndex + 1];
                this.editMode = false;
            }
        }
    }

    previousLot() {
        if (this.selectedLot.PkLot > 0) {
            let currentIndex = this.lots.indexOf(this.selectedLot);
            if (currentIndex > 0) {
                this.selectedLot = this.lots[currentIndex - 1];
                this.editMode = false;
            }
        }
    }

    displayEditTab() {
        this.searchTabClass = "";
        this.editTabClass = "displayItem active";
        $('#selectedLot').addClass('in').addClass('active');
        $('#search').removeClass('in').removeClass('active');
    }

    closeEditTab(close: boolean) {
        setTimeout(() => {
            if (close) {
                this.showEditTab = false;
                this.searchTabClass = "active";
                this.editTabClass = "hiddenItem";
                $('#selectedLot').removeClass('in').removeClass('active');
                $('#search').addClass('in').addClass('active');
                this.selectedLot = new Lot();
            }
        }, 200);
    }

    onScrollDown() {
        this.start = this.start + this.pagercount;
        this.addItem(this.start);
    }

    Disablelot() {

    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }

    checkAll(ev: any) {
        this.lots.forEach(x => x.IsSelected = ev.target.checked)
    }

    isAllChecked() {
        return this.lots.every(x => x.IsSelected);
    }
}
