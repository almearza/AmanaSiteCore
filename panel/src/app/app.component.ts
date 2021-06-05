import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // title='الموقع الخارجي - لوحة التحكم';
  ngOnInit(): void {
    this.setCurrentUser();
    // this.titleService.setTitle(this.title);
  }
  setCurrentUser() {
    //if we close browser and open it agian we get this value from storage and set the observable
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
    }
  }
  constructor(private accountService: AccountService,private titleService: Title) {
  }
}
