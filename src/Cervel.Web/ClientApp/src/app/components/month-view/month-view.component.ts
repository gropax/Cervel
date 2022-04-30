import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-month-view',
  templateUrl: './month-view.component.html',
  styleUrls: ['./month-view.component.less']
})
export class MonthViewComponent implements OnInit {

  @Input() year!: number;
  @Input() month!: number;
  //@Input() dayHighlights: DayHighlight[] = [];

  public dayRange: DayInfo[] = [];

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

  constructor() {
  }

  ngOnInit(): void {
    this.dayRange = this.getDayRange();
  }

  public getDayRange() {
    var days = [];
    var dayNb = new Date(this.year, this.month, 0).getDate();
    var weekNb = 1;

    for (var i = 1; i <= dayNb; i++) {
      var date = new Date(this.year, this.month - 1, i);
      var dow = (date.getDay() + 6) % 7 + 1;
      if (dow == 1 && i != 1)
        weekNb++;

      days.push(new DayInfo(this.year, this.month, i, dow, weekNb));
    }

    return days;
  }

  private dayOfWeekClasses = [
    "sunday",
    "monday",
    "tuesday",
    "wednesday",
    "thursday",
    "friday",
    "saturday",
  ]

}

export class DayInfo {
  constructor(
    public year: number,
    public month: number,
    public day: number,
    public dayOfWeek: number,
    public weekNumber: number) {
  }
}
