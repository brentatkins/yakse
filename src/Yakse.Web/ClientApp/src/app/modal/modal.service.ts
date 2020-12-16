import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ModalService {
  private isOpen$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  watch() {
    return this.isOpen$.asObservable();
  }

  open() {
    this.isOpen$.next(true);
  }

  close() {
    this.isOpen$.next(false);
  }
}
