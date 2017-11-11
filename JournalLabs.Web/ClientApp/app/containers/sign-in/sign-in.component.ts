import { Component } from '@angular/core';
import { IUser } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { Router, CanActivate, NavigationEnd } from '@angular/router';
@Component({
  selector: 'sign-in',
  template: `
      <h1>Input your Email and password to sign in into site</h1>
      <br />
      <p>Input Email:</p>
      <input type="text" [(ngModel)]="teacherModel.Login" name="Login"/>
      <br />
      <p>Input password:</p>
      <input type="text" [(ngModel)]="teacherModel.Password" name="Password"/>
      <br />
      <button (click)="SignIn()">Sign in</button>
  `
})
export class SignInComponent {
  public teacherModel: IUser = { Id: "", Login: "", Password: "", Role: "" };

  constructor(public router: Router,
    private userService: UserService) { }

  public SignIn() {
    //this.teacherModel.Role = "Teacher";
    this.userService.signInUser(this.teacherModel).subscribe(response => {
      var result:IUser = JSON.parse(response._body);
      if (result.Role == "Teacher") {
        localStorage.setItem('Role', result.Role);
        location.reload();
        this.router.navigate(['journal']);
        return;
      }
      if (result.Role == "Admin") {
        localStorage.setItem('Role', result.Role);
        location.reload();
        this.router.navigate(['admin']);
        return;
      }
    });
  }
}
