import { Component } from '@angular/core';
import {User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { NgModel } from '@angular/forms';

@Component({
    selector: 'admin',
    templateUrl: './admin.component.html'
})
export class AdminComponent {
  public teacherModel: User = {Id:"", Login: "", Password:"", Role:"Admin"};

    constructor(private userService: UserService) { }

    public SignUp() {      
      //this.teacherModel.Role = "Teacher";
      this.userService.addUser(this.teacherModel).subscribe(response => {
        console.log("Success Sign up teacher");
      });
    }
}
