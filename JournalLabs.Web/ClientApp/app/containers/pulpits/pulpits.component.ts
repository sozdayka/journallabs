import { Component, OnInit } from '@angular/core';

import { LogService } from '../../shared/log.service';
@Component({
  selector: 'pulpits',
  templateUrl: 'pulpits.component.html'
})
export class PulpitsComponent implements OnInit {
  fullName: string;
  shortName: string;

  pulpitShortName = '';
  pulpitFullName = '';


  public pulpitArr: [{fName: string, sName: string}] = [{
    fName: 'ІТех',
    sName: 'INTERNET-технологіі'
  }, {
    fName: 'Комп"ютерні мережі',
    sName: 'КМ'
  }, {
    fName: 'Захист інформаціі в КСМ',
    sName: 'ЗІКС'
  }];



  public constructor(

    public logService: LogService
  ) {
  }

  public ngOnInit(): void {
    


  }


  public addPulPit():void{

      this.pulpitArr.push({
        fName: this.pulpitShortName,
        sName: this.pulpitFullName
      });
      this.pulpitShortName = '';
      this.pulpitFullName = '';

  }
  public changePulPitFullName(pulpitArr){
    console.log("Change pulput FullName: "+pulpitArr.fName);
  }
  public changePulPitShortName(pulpitArr){
    console.log("Change pulput ShortName: "+pulpitArr.sName);
  }
  public removePulPit(pulpitDelete){
    //this.pulpitArr.splice(pulpitDelete.sName, 1);
    console.log("Delete pulput ShortName: "+this.pulpitArr[pulpitDelete].sName);
    this.pulpitArr.splice(pulpitDelete, 1);
    
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
