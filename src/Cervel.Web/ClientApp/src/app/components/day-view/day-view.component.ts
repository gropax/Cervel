import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Highlight } from '../../models/highlights';

@Component({
  selector: 'app-day-view',
  templateUrl: './day-view.component.html',
  styleUrls: ['./day-view.component.less']
})
export class DayViewComponent implements OnInit {

  @Input() year!: number;
  @Input() month!: number;
  @Input() day!: number;
  @Input() highlights: Highlight[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
