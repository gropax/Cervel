import { animate, state, style, transition, trigger } from '@angular/animations';
import { Input, OnChanges, SimpleChanges } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Highlight } from '../../models/highlights';

@Component({
  selector: 'app-day-view',
  templateUrl: './day-view.component.html',
  styleUrls: ['./day-view.component.less'],
  animations: [
    trigger('changeState', [
      state('active', style({
        backgroundColor: '#7b1fa2',
      })),
      state('inactive', style({
        backgroundColor: '#747474'
      })),
      transition('* => active', animate('200ms')),
      transition('* => inactive', animate('200ms'))
    ])
  ]
})
export class DayViewComponent implements OnInit, OnChanges {

  @Input() year!: number;
  @Input() month!: number;
  @Input() day!: number;
  @Input() highlights: Highlight[] = [];

  public state: 'active' | 'inactive' = 'inactive';

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    var highlights = changes['highlights'].currentValue;
    this.state = highlights.length > 0 ? 'active' : 'inactive';
  }

  ngOnInit(): void {
  }

}
