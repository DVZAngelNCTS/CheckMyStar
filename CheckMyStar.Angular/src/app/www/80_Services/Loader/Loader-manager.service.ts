import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoaderManager {
  private global = new BehaviorSubject<boolean>(false);
  global$ = this.global.asObservable();

  private localLoaders = new Map<string, BehaviorSubject<boolean>>();

  showGlobal() { this.global.next(true); }
  hideGlobal() { this.global.next(false); }

  register(id: string) {
    if (!this.localLoaders.has(id)) {
      this.localLoaders.set(id, new BehaviorSubject<boolean>(false));
    }
    return this.localLoaders.get(id)!.asObservable();
  }

  show(id: string) {
    this.localLoaders.get(id)?.next(true);
  }

  hide(id: string) {
    this.localLoaders.get(id)?.next(false);
  }
}
