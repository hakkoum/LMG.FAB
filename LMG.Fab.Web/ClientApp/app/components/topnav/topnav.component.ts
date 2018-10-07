import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { UserService } from '../../services/userService';
import { UserProfil } from "../../models/UserProfil";
import { AppConfig } from '../../../app.config';
import { TitleService } from '../../services/titleService';

@Component({
    selector: 'top-nav',
    templateUrl: './topnav.component.html',
    styleUrls: ['./topnav.component.css']
})
export class TopNavComponent implements OnInit {
    public userProfil: UserProfil = new UserProfil();
    public envClass = "dev";

    constructor(private userService: UserService, private config: AppConfig, private titleService: TitleService) {

    }

    toggleClicked(event: MouseEvent) {
        if (event.srcElement == null) return;
        var target = event.srcElement.id;
        var body = $('body');
        var menu = $('#sidebar-menu');

        // toggle small or large menu
        if (body.hasClass('nav-md')) {
            menu.find('li.active ul').hide();
            menu.find('li.active').addClass('active-sm').removeClass('active');
        } else {
            menu.find('li.active-sm ul').show();
            menu.find('li.active-sm').addClass('active').removeClass('active-sm');
        }
        body.toggleClass('nav-md nav-sm');

    }

    ngOnInit() {
        switch (this.config.get('global', 'environment')) {
            case "Recette":
                this.envClass = "rec";
                break;
            case "Production":
                this.envClass = "prod";
                break;
            default:
                this.envClass = "dev";
                break;
        }
        this.userService.getUserProfil().subscribe(o => {
            this.userProfil = o;
        });
    }

}
