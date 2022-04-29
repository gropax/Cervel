import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { BehaviorSubject, Observable, combineLatest, pipe } from 'rxjs';
import { ApplicationStateService } from './application-state.service';
import { map } from 'rxjs/operators';


export class MainAction {
  constructor(
    public type: MainActionType,
    public action: Function = () => { }) {
  }

  public static none() {
    return new MainAction(MainActionType.None, () => { });
  }
}

export enum MainActionType {
  Sidenav,
  Back,
  Cancel,
  None,
}

export class MenuAction {
  constructor(
    public name: string,
    public icon: string,
    public action: Function) {
  }
}

export enum ToolbarMode {
  Navigation,
  Contextual,
}

@Injectable({
  providedIn: 'root'
})
export class ToolbarService {

  private modeSubject = new BehaviorSubject<ToolbarMode>(ToolbarMode.Navigation);
  private titleSubject = new BehaviorSubject<string>("");
  private mainActionSubject = new BehaviorSubject<MainAction>(MainAction.none());
  private actionsSubject = new BehaviorSubject<MenuAction[]>([]);

  public mode$ = this.modeSubject.asObservable();
  public title$ = this.titleSubject.asObservable();
  public mainAction$ = this.mainActionSubject.asObservable();
  public actions$ = this.actionsSubject.asObservable();

  public isNavigationMode$ = this.mode$.pipe(map(m => m == ToolbarMode.Navigation));
  public isBackButton$ = this.mainAction$.pipe(map(b => b && b.type == MainActionType.Back));
  public isSidenavButton$ = this.mainAction$.pipe(map(b => b && b.type == MainActionType.Sidenav));
  public isCancelButton$ = this.mainAction$.pipe(map(b => b && b.type == MainActionType.Cancel));

  public mainAction: Function = () => { };

  constructor(
    private location: Location,
    private applicationStateService: ApplicationStateService) {
    this.mainAction$.subscribe(a => {
      console.log(a);
      if (a) this.mainAction = a.action;
    });
  }

  setMode(mode: ToolbarMode) {
    this.modeSubject.next(mode);
  }

  setTitle(title: string) {
    this.titleSubject.next(title);
  }

  setMainAction(type: MainActionType, action: Function = () => { }) {
    let fn: Function;
    switch (type) {
      case MainActionType.Sidenav:
        fn = () => this.applicationStateService.openSidenav(); break;
      case MainActionType.Back:
        fn = () => action || this.location.back(); break;
      case MainActionType.Cancel:
        fn = action; break;
      default:
        throw new Error(`Unsupported MainActionType: ${type}.`);
    }
    this.mainActionSubject.next(new MainAction(type, fn));
  }

  setActions(actions: MenuAction[]) {
    this.actionsSubject.next(actions);
  }
}

