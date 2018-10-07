import * as $ from 'jquery';
import { NgModule, APP_INITIALIZER, Pipe } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserModule, DomSanitizer } from "@angular/platform-browser";
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TopNavComponent } from './components/topnav/topnav.component';
import { HomeComponent } from './components/home/home.component';
import { UserService } from './services/userService';
import { LotService } from './services/lotService';
import { LotListComponent } from './components/lot/lotList.component';
import { LotEditComponent } from './components/lot/lotEdit.component';
import { AuditService } from './services/auditService';
import { AuditListComponent } from './components/audit/auditList.component';
import { ModalModule, PaginationModule, BsDatepickerModule } from 'ngx-bootstrap';
import { ToastyModule } from 'ng2-toasty';
import { AppConfig } from '../app.config';
import { StringFormat } from './pipes/stringFormat.pipe';
import { DatePicker } from './components/shared/date-picker.component';
import { HttpModule } from '@angular/http';
import { LockService } from './services/lockService';
import { InfiniteScrollModule } from 'angular2-infinite-scroll';
import { OrderPipe } from './pipes/orderBy.pipe';
import { Validators, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CKEditorModule } from 'ng2-ckeditor';
import { PopoverModule } from 'ng2-popover';
import { SafeHTML } from './pipes/safeHtml.pipe';
import { TooltipModule } from "ng2-tooltip";
import { SharedService } from './services/sharedService';
import { CommentPopup } from './components/shared/comment-popup.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ParamListComponent } from './components/params/paramList.component';
import { FilterPipe } from './pipes/filterList.pipe';
import { alertListComponent } from './components/alerts/alertList.component';
import { DossierListComponent } from './components/dossier/dossierList.component';
import { TiersListComponent } from './components/tiers/tiersList.component';
import { TitleService } from './services/titleService';
import { ColTechListComponent } from './components/params/colTechList.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TopNavComponent,
        HomeComponent,
        LotListComponent,
        LotEditComponent,
        AuditListComponent,
        ParamListComponent,
        alertListComponent,
        DossierListComponent,
        TiersListComponent,
        StringFormat,
        OrderPipe,
        SafeHTML,
        FilterPipe,
        DatePicker,
        CommentPopup,
        ColTechListComponent
    ],
    providers: [
        UserService, HttpClientModule, HttpModule, LotService, AuditService, StringFormat, LockService, OrderPipe, SafeHTML, FilterPipe, TitleService,
        FormBuilder,
        SharedService,
        AppConfig,
        {
            provide: APP_INITIALIZER, useFactory: (config: AppConfig) => () => config.loadInstance('/dist/assets/labels.json','labels'), deps: [AppConfig], multi: true
        },
        {
            provide: APP_INITIALIZER, useFactory: (config: AppConfig) => () => config.loadInstance('/dist/assets/config.json', 'global'), deps: [AppConfig], multi: true
        }
    ],
    bootstrap: [AppComponent],
    imports: [
        CommonModule,
        HttpClientModule,
        HttpModule,
        FormsModule,
        ModalModule.forRoot(),
        BrowserModule,
        ToastyModule.forRoot(),
        BsDatepickerModule.forRoot(),
        InfiniteScrollModule,
        PaginationModule.forRoot(),
        TooltipModule,       
        CKEditorModule, PopoverModule,
        NgxDatatableModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'lot-list', component: LotListComponent },
            { path: 'audit-list', component: AuditListComponent },
            { path: 'param-list', component: ParamListComponent },
            { path: 'dossier-list', component: DossierListComponent },
            { path: 'tiers-list', component: TiersListComponent },   
            { path: 'coll-list', component: ColTechListComponent },  
            { path: '**', redirectTo: 'home' }
        ])
    ],
    exports: [StringFormat, OrderPipe]
})
export class AppModuleShared {
}

