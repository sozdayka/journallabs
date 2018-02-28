import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute, Params } from '@angular/router';

import { LogService } from '../../shared/log.service';

import { GroupService } from '../../shared/group.service';

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
    public groupService: GroupService
  ) {
  }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      this.Id = params['groupid'];   
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

}
