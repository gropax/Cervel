import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

export class Notification {
  constructor(
    public message: string) {
  }
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) {}

  public error(error: any) {
    console.log(error);
    this.snackBar.open(error.message, 'Close');
  }

  public notify(notificaton: Notification) {
    this.snackBar.open(notificaton.message, 'Close');
  }
}

