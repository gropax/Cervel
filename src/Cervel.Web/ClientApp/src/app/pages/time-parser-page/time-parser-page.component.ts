import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Notification, NotificationService } from '../../services/notification.service';
import { MainActionType, MenuAction, ToolbarMode, ToolbarService } from '../../services/toolbar.service';


@Component({
  selector: 'app-time-parser-page',
  templateUrl: './time-parser-page.component.html',
  styleUrls: ['./time-parser-page.component.less']
})
export class TimeParserPageComponent implements OnInit {
  
  private actions: MenuAction[];
  public loading: boolean = false;

  constructor(
    private router: Router,
    private toolbarService: ToolbarService,
    private notificationService: NotificationService) {
    this.actions = [
      //new MenuAction("Create", "done", () => { }),
    ];
  }

  ngOnInit() {
    this.toolbarService.setTitle("Time Parser");
    this.toolbarService.setMode(ToolbarMode.Navigation);
    this.toolbarService.setMainAction(MainActionType.Sidenav);
    //this.toolbarService.setActions(this.actions);
  }

}
