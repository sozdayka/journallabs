import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute, Params } from '@angular/router';

import { LogService } from '../../shared/log.service';

import { GroupService } from '../../shared/group.service';

import { StudentService } from '../../shared/student.service';

import { Group } from '../../models/Group';

import { Student } from '../../models/Student';

@Component({
  selector: 'group',
  templateUrl: 'group.component.html'
})
export class GroupComponent implements OnInit {
 
  groupInfo: Group = new Group();
  student: Student = new Student();
  GroupName: string;
  StudentCount: number;

  groupId = 0;
  StudentName = '';
  
 
//  public stugentArr: any = [{
public stugentArr: {sName: string} []= [{
    sName: 'Igor Rosliakov'
  }, {
    sName: 'Vlad Sas'
  }, {
    sName: 'Artem Swenton'
  }, {
    sName: 'Taras Ziza'
  }];



  public constructor(
    private route: ActivatedRoute,

    public logService: LogService,
    public groupService: GroupService,
    public studentService: StudentService
  ) {
  }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      //sss
      this.loadGroupInfo(params['groupid']);  
    });    
  }

  public loadGroupInfo(goripId: string) {
    this.groupService.getGroup(goripId).subscribe(data => {
      this.groupInfo = data;
      console.log("Group loaded successfully");
    });
  }

  public loadStudents() {
    //this.groupService.getGroup(goripId).subscribe(data => {
    //  this.groupInfo = data;
    //  console.log("Group loaded successfully");
    //});
  }
  public addStudent(): void{
    this.studentService.addStudent(this.student).subscribe(
      response => {
        this.student = new Student();
        console.log("Student create successfully");
      });
      this.stugentArr.push({
        sName: this.student.StudentName,       
      });
     

  }
  public changeGroupName(){

    console.log("Change group Name: "+ this.GroupName);
  }
  public changeGroupStudentCount(stugentArr){
    console.log("Change group Count: "+stugentArr.sName);
  }
  public removeGroup(pulpitDelete){
    //this.pulpitArr.splice(pulpitDelete.sName, 1);
    console.log("Delete from group, student Name: "+this.stugentArr[pulpitDelete].sName);
    this.stugentArr.splice(pulpitDelete, 1);
    
  }

}
