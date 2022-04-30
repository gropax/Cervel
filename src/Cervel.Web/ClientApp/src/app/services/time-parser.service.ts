import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
    public intervals: TimeInterval[]) {
  }
}

export class TimeInterval {
  constructor(
    public start: Date,
    public end: Date) {
  }
}
