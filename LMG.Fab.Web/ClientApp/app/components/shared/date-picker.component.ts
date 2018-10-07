import { Component, OnInit, Input, SimpleChanges, Output, EventEmitter } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { frLocale } from 'ngx-bootstrap/locale';

@Component({
    selector: 'date-picker',
    templateUrl: './date-picker.component.html',
    styleUrls: ['./date-picker.component.css']
})
export class DatePicker {
    @Input() date: any;
    @Input() readonly: boolean = false;
      
    @Output() dateChange = new EventEmitter<Date>();

    constructor(private _localeService: BsLocaleService) {
        defineLocale('fr', frLocale);
        this._localeService.use("fr");
    }

    setDate(event: any) {
        if (event == 'Invalid Date') {
            this.date = new Date();
            event = this.date;
            this.dateChange.emit(event);
            return;
        }
        if (event !== null && event !== undefined) {
            var formatDate = new Date(event.getFullYear(), event.getMonth(), event.getDate(), 10, 0, 0);
            this.dateChange.emit(formatDate);
        }
        else {
            this.dateChange.emit(event);
        }
    }
}
