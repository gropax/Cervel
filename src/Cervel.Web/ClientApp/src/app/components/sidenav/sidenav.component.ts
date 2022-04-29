import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { combineLatest } from 'rxjs';
import { ApplicationStateService } from '../../services/application-state.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.less']
})
export class SidenavComponent implements OnInit {

  @ViewChild('drawer', { static: true }) drawer: MatSidenav;

  constructor(public appState: ApplicationStateService) {
  }

  ngOnInit(): void {
    combineLatest(
      this.appState.isHandset$,
      this.appState.sidenavOpen$
    ).subscribe(([isHandset, sidenavOpen]) =>
      isHandset && !sidenavOpen ? this.drawer.close() : this.drawer.open());
  }

}

