import { Component, OnInit } from '@angular/core';

import { LogService } from '../../shared/log.service';
@Component({
  selector: 'groups',
  templateUrl: 'groups.component.html'
})
export class GroupsComponent implements OnInit {
  Name: string;
  

  groupName = '';
  groupStudentCount = 0;


  public groupsArr: [{gName: string}] = [{
    gName: 'Kiu 15-6'
  }, {
    gName: 'PI 14-3'
  }, {
    gName: 'APK 13-6'
    
  }];



  public constructor(

    public logService: LogService
  ) {
  }

  public ngOnInit(): void {
    


  }

 
  public addGroup():void{

      this.groupsArr.push({
        gName: this.groupName,
        
      });
      this.groupName = '';
     /* this.groupStudentCount = 0;*/

  }
  public changeGroupName(groupsArr){
    console.log("Change group Name: "+groupsArr.gName);
  }
  public changeGroupStudentCount(groupsArr){
    console.log("Change group Count: "+groupsArr.gName);
  }
  public removeGroup(pulpitDelete){
    //this.pulpitArr.splice(pulpitDelete.sName, 1);
    console.log("Delete group ShortName: "+this.groupsArr[pulpitDelete].gName);
    this.groupsArr.splice(pulpitDelete, 1);
    
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
