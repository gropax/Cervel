import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DayHighlight } from '../models/highlights';

@Injectable({
  providedIn: 'root'
})
export class TimeParserService {

  constructor(protected http: HttpClient) { }

  public parseIntervals(timeExpr: string): Observable<ParseResult> {
    return this.http.get<ParseResult>(`api/time-parser/intervals`, { params: { timeExpr } });
  }

}

export class ParseResult {
  constructor(
    public timeExpr: string,
    public isSuccess: boolean,
    public intervals: TimeInterval[],
    public dayHighlights: DayHighlight[]) {
  }
}

export class TimeInterval {
  constructor(
    public start: Date,
    public end: Date) {
  }
}
