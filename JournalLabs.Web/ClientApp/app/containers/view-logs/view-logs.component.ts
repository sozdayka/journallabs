import { Component, OnInit } from '@angular/core';

import { LogService } from '../../shared/log.service';
@Component({
  selector: 'view-logs',
  templateUrl: 'view-logs.component.html'
})
export class ViewLogsComponent implements OnInit {
  public typeLogs='user'; // admin/user
  public viewLog=[];

  public logsArr: {ltext: string, ldate:string, ltype:string} []= [{
    ltext: 'Пользователь 123 авторизировался',
    ldate: '2019-02-10 10:00',
    ltype: 'user'
  }, {
    ltext: 'Admin добавил пользователя "123", и сварил борщ из мяса со вкусом мыла',
    ldate: '2019-02-11 12:47',
    ltype: 'admin'
  }, {
    ltext: 'Пользователь 123 создал журнал СЛон и пошел работать в Мак на научке',
    ldate: '2019-02-25 20:59',
    ltype: 'user'
  }];



  public constructor(

    public logService: LogService
  ) {
  }

  public ngOnInit(): void {
    this.filterChange("user");
  }

  public filterChange(view){
    this.typeLogs = view; 
console.log(this.typeLogs);
    this.viewLog = this.logsArr.filter(s => {
      return s.ltype==this.typeLogs;
    });


    // this.selected_groups = this.groups.filter(s => {
    //   this.studenFromGroup.forEach(eachObj => {
    //     if(eachObj.id== s.id && s.selected==true) {
    //       this.studList.push(eachObj);
    //     }
    //   });
    //   return s.selected;
    // });
  }

 
}
