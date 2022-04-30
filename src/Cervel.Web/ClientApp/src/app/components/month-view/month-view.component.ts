import { Component, Input, OnInit } from '@angular/core';
import { DayHighlight } from '../../models/highlights';

@Component({
  selector: 'app-month-view',
  templateUrl: './month-view.component.html',
  styleUrls: ['./month-view.component.less']
})
export class MonthViewComponent implements OnInit {

  @Input() year!: number;
  @Input() month!: number;
  @Input() dayHighlights: DayHighlight[] = [];

  private monthNames = [
    "Janvier",
    "Février",
    "Mars",
    "Avril",
    "Mai",
    "Juin",
    "Juillet",
    "Août",
    "Septembre",
    "Octobre",
    "Novembre",
    "Décembre",
  ];
  public monthName() {
    return this.monthNames[this.month - 1];
  }

  constructor() { }

  ngOnInit(): void {
  }

}
