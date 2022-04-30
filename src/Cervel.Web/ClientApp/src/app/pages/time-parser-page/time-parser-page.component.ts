import { ElementRef } from '@angular/core';
import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject, debounce, debounceTime, filter, map, mergeMap } from 'rxjs';
import { Notification, NotificationService } from '../../services/notification.service';
import { MonthHighlights, TimeParserService, YearHighlights } from '../../services/time-parser.service';
import { MainActionType, MenuAction, ToolbarMode, ToolbarService } from '../../services/toolbar.service';


@Component({
  selector: 'app-time-parser-page',
  templateUrl: './time-parser-page.component.html',
  styleUrls: ['./time-parser-page.component.less']
})
export class TimeParserPageComponent implements OnInit {
  
  private actions: MenuAction[];
  public loading: boolean = false;

  public timeExpr: string = "";
  private timeExprSubject = new BehaviorSubject(this.timeExpr);
  private timeExpr$ = this.timeExprSubject.asObservable();

  public monthRange = new Array(12);
  public highlights: MonthHighlights = {};

  constructor(
    private router: Router,
    private toolbarService: ToolbarService,
    private notificationService: NotificationService,
    private timeParserService: TimeParserService
  ) {
    this.actions = [
      //new MenuAction("Create", "done", () => { }),
    ];
  }

  ngOnInit() {
    this.toolbarService.setTitle("Time Parser");
    this.toolbarService.setMode(ToolbarMode.Navigation);
    this.toolbarService.setMainAction(MainActionType.Sidenav);
    //this.toolbarService.setActions(this.actions);

    this.timeExpr$
      .pipe(
        debounceTime(500),
        filter(e => !!e),
        mergeMap(e => this.timeParserService.parseIntervals(e))
      )
      .subscribe(res => {
        console.log(res.timeExpr);
        if (res.isSuccess)
          this.highlights = res.highlights[2022];
      });
  }

  public updateTimeExpr($event: string) {
    this.timeExprSubject.next($event);
  }

}
