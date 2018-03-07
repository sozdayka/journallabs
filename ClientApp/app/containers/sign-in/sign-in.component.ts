import { Component } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { Router, CanActivate, NavigationEnd } from '@angular/router';
import { LogService } from '../../shared/log.service';
@Component({
  selector: 'sign-in',
  template: `
      <h1>Войти как преподаватель:</h1>
      <br />
      <p>Введите логин</p>
      <input type="text" [(ngModel)]="teacherModel.Login" name="Login"/>
      <br />
      <p>Введите пароль:</p>
      <input type="password" [(ngModel)]="teacherModel.Password" name="Password"/>
      <br />
      <button (click)="SignIn()">Войти</button>
  `
})
export class SignInComponent {
  /*Input Email:*/
  /*Input your Email and password to sign in into site*/
  /*Input password:*/
  /*Sign in*/
  public teacherModel: User = { Id: "", Login: "", Password: "111111", Role: "", CathedraId:"" };

  constructor(public router: Router,
    private userService: UserService, private logService: LogService) { }

  public SignIn() {
    //this.teacherModel.Role = "Teacher";
    this.userService.signInUser(this.teacherModel).subscribe(response => {
      var logText = `${new Date().toLocaleString()} Пользователь ${this.teacherModel.Login} успешно авторизирован`;
      this.logService.writeTeacherLog(logText,"user").subscribe(resp => {
        var result: User = JSON.parse(response._body);
        if (result.Role == "Teacher" || result.Role == "Assistant") {
          localStorage.setItem('Role', result.Role);
          localStorage.setItem('TeacherId', result.Id);
          localStorage.setItem('TeacherName', result.Login);
         location.reload();
          this.router.navigate(['teacher-journals']);
         return;
        }
        if (result.Role == "Admin") {
          localStorage.setItem('Role', result.Role);
         location.reload();
         this.router.navigate(['admin']);
         return;
        }
      });
    });
  }
}
