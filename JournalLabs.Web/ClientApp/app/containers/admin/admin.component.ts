import { Component } from '@angular/core';
import {User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { NgModel } from '@angular/forms';
import { LogService } from '../../shared/log.service';

@Component({
    selector: 'admin',
    templateUrl: './admin.component.html'
})
export class AdminComponent {
  public teacherModel: User = { Id: "", Login: "", Password: "111111", Role: "Teacher", CathedraId:"" };

  constructor(private userService: UserService, private logService: LogService) { }

    public SignUp() {      
      //this.teacherModel.Role = "Teacher";
      this.userService.addUser(this.teacherModel).subscribe(response => {
        var logText = `${new Date().toLocaleString()} Преподаватель ${this.teacherModel.Login} успешно добавлен`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          alert("Преподаватель успешно добавлен");
          location.reload();
        });       
      });
    }
}
