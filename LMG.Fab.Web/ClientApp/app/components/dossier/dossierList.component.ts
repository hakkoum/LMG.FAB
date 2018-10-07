import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { LotService } from '../../services/lotService';
import { Lot } from "../../models/lot";
import { AppConfig } from '../../../app.config';
import { ParamDetail } from '../../models/paramTable';
import { TitleService } from '../../services/titleService';

@Component({
    selector: 'dossier-list',
    templateUrl: './dossierList.component.html',
    styleUrls: ['./dossierList.component.css', '../app/app.shared.css']
})
export class DossierListComponent implements OnInit {

    constructor(private lotService: LotService, private config: AppConfig, private titleService: TitleService) {
        this.titleService.SetTitle(this.config.getlabel('lbl_title_dossier'));
    }

    ngOnInit() {
    }

   
}
