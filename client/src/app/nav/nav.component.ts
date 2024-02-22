import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  //loggedIn = false;


  constructor(public accountService: AccountService, private router: Router, 
    private toaster: ToastrService) {

  }

  ngOnInit(): void {

  }

  login() {
    console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: response => {
        //console.log(response);
        this.router.navigateByUrl("/members");
        //this.loggedIn = true;
      },
      //error: error => console.log(error)
      error: error => this.toaster.error(error.error)
    });

  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl("/");
    //this.loggedIn = false;
  }


}
