import { Component } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { Router, CanActivate, NavigationEnd } from '@angular/router';
@Component({
  selector: 'sign-in',
  template: `
      <h1>Войти как преподователь:</h1>
      <br />
      <p>Введите логин</p>
      <input type="text" [(ngModel)]="teacherModel.Login" name="Login"/>
      <br />
      <p>Введите пароль:</p>
      <input type="text" [(ngModel)]="teacherModel.Password" name="Password"/>
      <br />
      <button (click)="SignIn()">Войти</button>
  `
})
export class SignInComponent {
  /*Input Email:*/
  /*Input your Email and password to sign in into site*/
  /*Input password:*/
  /*Sign in*/
  public teacherModel: User = { Id: "", Login: "", Password: "", Role: "" };

  constructor(public router: Router,
    private userService: UserService) { }

  public SignIn() {
    //this.teacherModel.Role = "Teacher";
    this.userService.signInUser(this.teacherModel).subscribe(response => {
      var result:User = JSON.parse(response._body);
      if (result.Role == "Teacher") {
        localStorage.setItem('Role', result.Role);
        localStorage.setItem('TeacherId', result.Id);
        location.reload();
        //this.router.navigate(['teacher-journals']);
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
