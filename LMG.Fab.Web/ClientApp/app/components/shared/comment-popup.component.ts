import { Component, OnInit, Input, SimpleChanges, Output, EventEmitter, OnChanges, ViewChild } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { Commentaire } from '../../models/commentaire';
import { AppConfig } from '../../../app.config';

@Component({
    selector: 'comment-popup',
    templateUrl: './comment-popup.component.html',
    styleUrls: ['./comment-popup.component.css']
})


export class CommentPopup implements OnChanges {
    @Input() fieldName: string;
    @Input() showComment: boolean = false;
    @Input() comment: Commentaire = new Commentaire();
    public commentHtml: string;
    public isImportant: boolean = false;
    @Input() canEdit: boolean;

    private isFirstBinding = false;

    @Output() commentChange = new EventEmitter<Commentaire>();

    constructor(private config: AppConfig) {

    }

    ngOnChanges(changes: SimpleChanges) {
        if (this.comment != undefined && !this.isFirstBinding) {
            this.isImportant = this.comment.IsImportant;
            this.commentHtml = this.comment.CommentaireHtml;
            this.isFirstBinding = true;
        }
    }

    editComment(popover: any, commentHtml: string) {
        if (this.comment == undefined) {
            this.comment = new Commentaire();
        }
        this.comment.IsImportant = this.isImportant;
        this.comment.CommentaireHtml = commentHtml;
        this.commentHtml = commentHtml;
        this.comment.FieldComm = this.fieldName;
        this.commentChange.emit(this.comment);
        popover.hide();
    }

    cancelEdit(popover: any) {
        if (this.comment != undefined) {
            this.isImportant = this.comment.IsImportant;
            this.commentHtml = this.comment.CommentaireHtml;
        }
        popover.hide();
    }

}
