import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute, Params } from '@angular/router';

import { LogService } from '../../shared/log.service';

import { GroupService } from '../../shared/group.service';

import { StudentService } from '../../shared/student.service';

import { Group } from '../../models/Group';

import { Student } from '../../models/Student';
import { StudentGroupService } from '../../shared/student-group.service';
import { AddStudentToGroup } from '../../models/addStudentToGroup';

@Component({
  selector: 'group',
  templateUrl: 'group.component.html'
})
export class GroupComponent implements OnInit {
 
  groupInfo: Group = new Group();
  student: AddStudentToGroup = new AddStudentToGroup();
  groupStudents: Student[] = [];
  GroupName: string;
  StudentCount: number;

  groupId:string = "";
  StudentName = '';
  
 
  public stugentArr: [{sName: string}] = [{
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
    public studentService: StudentService,
    public studentGroupService: StudentGroupService
  ) {
  }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      //sss
      this.groupId = params['groupid'];
      this.loadGroupInfo(this.groupId);
      this.loadStudents();
    });    
  }

  public loadGroupInfo(goripId: string) {
    this.groupService.getGroup(goripId).subscribe(data => {
      this.groupInfo = data;
      console.log("Group loaded successfully");
    });
  }

  public loadStudents() {
    this.studentService.getStudentsByGroupId(this.groupId).subscribe(data => {
      this.groupStudents = data;
      console.log("Group loaded successfully");
    });
  }
  public addStudent(): void{
    this.student.GroupId = this.groupId;
    this.studentGroupService.addStudentToGroup(this.student).subscribe(
      response => {
        this.groupStudents.push({
          Id: JSON.parse(response._body),
          StudentName:this.student.Student.StudentName
        });
        this.student.Student = new Student();
        console.log("Student create successfully");
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
/*
  public changeKindOfWorkName(kindOfWork: KindOfWork) {
    this.kindOfWorkService.updateKindOfWork(kindOfWork).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил настройки Вида работы под Id ${kindOfWork.Id} на
                      название ${kindOfWork.NameKindOfWork}, видимость для ассистента ${kindOfWork.IsKindOfWorkVisible}, видимость для студента ${kindOfWork.IsVisibleToStudent}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success update kindOfWork name");
        });
      });
  }
*/  
}
