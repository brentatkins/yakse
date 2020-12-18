import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-movement-hint',
  template: `
    <span
      *ngIf="movement !== undefined && movement > 0"
      class="icon has-text-success animate__animated animate_slower animate__fadeOut"
    >
      <i class="fas fa-angle-up"></i>
    </span>
    <span
      *ngIf="movement !== undefined && movement < 0"
      class="icon has-text-danger animate__animated animate_slower animate__fadeOut"
    >
      <i class="fas fa-angle-down"></i>
    </span>
    <span *ngIf="movement === undefined" class="icon is-invisible">
      <i class="fas fa-info-circle"></i>
    </span>
  `,
})
export class MovementHintComponent implements OnInit, OnChanges {
  @Input() price!: number;

  movement?: number;

  constructor() {}

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.price.previousValue !== undefined) {
      // hack alert: if movement is in the same direction, animation won't run again (html output is the same)
      // with React, I could set the key prop to force a reload, not sure how to do this in angular
      // so setting the movement too undefined to force an update
      this.movement = undefined
      setTimeout(() => {
        this.movement = changes.price.currentValue - changes.price.previousValue;
      }, 1)
    }
  }
}
