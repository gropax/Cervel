import { Component, OnInit } from '@angular/core';
import { combineLatest } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { ApplicationStateService } from '../../services/application-state.service';
import { MenuAction, ToolbarService } from '../../services/toolbar.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.less']
})
export class ToolbarComponent implements OnInit {

  private toolbarActionsSubject = new BehaviorSubject<MenuAction[]>([]);
  private menuActionsSubject = new BehaviorSubject<MenuAction[]>([]);

  public toolbarActions$ = this.toolbarActionsSubject.asObservable();
  public menuActions$ = this.menuActionsSubject.asObservable();

  public displaySidenavButton$ = combineLatest(
    this.appState.isHandset$,
    this.toolbarService.isSidenavButton$,
    (isHandset, isSidenav) => isHandset && isSidenav);

  constructor(
    private appState: ApplicationStateService,
    public toolbarService: ToolbarService) {
  }

  ngOnInit() {
    combineLatest(
      this.appState.isHandset$,
      this.toolbarService.actions$
    ).subscribe(([isHandset, actions]) => {
      var topbarMaxActions = this.getTopbarMaxActions(actions.length, isHandset);
      this.toolbarActionsSubject.next(actions.slice(0, topbarMaxActions));
      this.menuActionsSubject.next(actions.slice(topbarMaxActions));
    });
  }

  getTopbarMaxActions(actions: number, isHandset: boolean) {
    return isHandset && actions > 2 ? 1 : 2;
  }

}

