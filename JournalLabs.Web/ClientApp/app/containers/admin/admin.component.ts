import { Component } from '@angular/core';
import { IUser } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { NgModel } from '@angular/forms';

@Component({
    selector: 'admin',
    templateUrl: './admin.component.html'
})
export class AdminComponent {
  public teacherModel: IUser = {Id:"", Login: "", Password:"", Role:"Teacher"};

    constructor(private userService: UserService) { }

    public SignUp() {      
      //this.teacherModel.Role = "Teacher";
      this.userService.addUser(this.teacherModel).subscribe(response => {
        console.log("Success Sign up teacher");
      });
    }
}
