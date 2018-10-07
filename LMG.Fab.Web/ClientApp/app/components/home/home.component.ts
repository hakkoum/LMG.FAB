import { Component } from '@angular/core';
import { TitleService } from '../../services/titleService';
import { AppConfig } from '../../../app.config';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css', '../app/app.shared.css']
})
export class HomeComponent {
    constructor(private titleService: TitleService, private config: AppConfig) {
        this.titleService.SetTitle(this.config.getlabel('lbl_title_home'));
    }
}

