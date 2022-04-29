import { Input, OnChanges, SimpleChanges } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-page-content',
  templateUrl: './page-content.component.html',
  styleUrls: ['./page-content.component.less']
})
export class PageContentComponent implements OnInit {

  @Input() loading!: boolean;

  constructor() { }

  ngOnInit() {
  }

}

