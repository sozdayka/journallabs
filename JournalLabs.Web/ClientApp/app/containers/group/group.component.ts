import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute, Params } from '@angular/router';

import { LogService } from '../../shared/log.service';
@Component({
  selector: 'group',
  templateUrl: 'group.component.html'
})
export class GroupComponent implements OnInit {
 

  Id: number;
  GroupName: string;
  StudentCount: number;

  groupId = 0;
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

    public logService: LogService
  ) {
  }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      //sss
      this.Id = params['groupid'];
      alert(this.Id);    
    });
  
    


  }


  public addStudent():void{

      this.stugentArr.push({
        sName: this.StudentName,
       
      });
      this.StudentName = '';
     

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
