import { Component, OnInit , Input} from '@angular/core';
import { Observable } from "rxjs";

import { ModalService } from './modal.service';

@Component({
  selector: 'app-modal',
  template: `
    <ng-container *ngIf="isOpen$ | async as isOpen">
      <div class="modal" [class]="{ 'is-active': isOpen }">
        <div class="modal-background"></div>
        <div class="modal-card">
          <header class="modal-card-head">
            <p class="modal-card-title">{{ title }}</p>
            <button class="delete" aria-label="close" (click)="close()"></button>
          </header>
          <section class="modal-card-body">
            <ng-content></ng-content>
          </section>
          <footer class="modal-card-foot">
          </footer>
        </div>
      </div>
    </ng-container>
  `
})
export class ModalComponent implements OnInit {

  @Input() title!: string

  isOpen$!: Observable<boolean>

  constructor(private modalService : ModalService) { }

  ngOnInit() {
    this.isOpen$ = this.modalService.watch();
  }

  close() {
    this.modalService.close();
  }
}
