import { Component, OnInit } from '@angular/core';

import { LogService } from '../../shared/log.service';
import { CathedraService } from '../../shared/cathedra.service';
import { Cathedra } from '../../models/Cathedra';
@Component({
  selector: 'cathedras',
  templateUrl: 'cathedras.component.html'
})
export class CathedrasComponent implements OnInit {

  public pulpitArr: {fName:string, sName:string}[] = [{
    fName: 'ІТех',
    sName: 'INTERNET-технологіі'
  }, {
    fName: 'Комп"ютерні мережі',
    sName: 'КМ'
  }, {
    fName: 'Захист інформаціі в КСМ',
    sName: 'ЗІКС'
  }];

  //http://journallabs.pp.ua/api/Cathedra/GetCathedras
  //http://journallabs.pp.ua/api/Cathedra/GetCathedras

  public newCathedra: Cathedra = new Cathedra();
  public cathedrasArray: Cathedra[]= [];

  public constructor(

    public logService: LogService,
    public cathedraService: CathedraService
  ) {
  }

  public ngOnInit(): void {
    this.loadCathedras();
  }
  public loadCathedras() {
    this.cathedraService.getCathedras().subscribe(data => {
      this.cathedrasArray = [];
      var responseArray = JSON.stringify(data);
      this.cathedrasArray = JSON.parse(responseArray);
      console.log("Cathedrs loaded successfully");
    });
  }
  public addCathedra():void{

    this.cathedraService.addCathedra(this.newCathedra).subscribe(responce => {
      this.newCathedra = new Cathedra();
      console.log("Cathedra create successfully");
      this.loadCathedras();
    })
      //this.groupName = '';
    /* this.groupStudentCount = 0;*/
  }
  public changeCathedraShortName(cathedra: Cathedra){
    //console.log("Change group Name: "+groupsArr.gName);
      this.cathedraService.updateCathedra(cathedra).subscribe(
        result => {
          console.log("Cathedra update successfully");
        //   var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил имя студента под Id ${student.Id} на ${student.StudentName}`;
        //   this.logService.writeTeacherLog(logText).subscribe(resp => {
        //     console.log("success update user name");
        //   });

        });
    }
  
  public changeCathedraFullName(cathedra: Cathedra){
    this.cathedraService.updateCathedra(cathedra).subscribe(
      result => {
        console.log("Cathedra update successfully");
      //   var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил имя студента под Id ${student.Id} на ${student.StudentName}`;
      //   this.logService.writeTeacherLog(logText).subscribe(resp => {
      //     console.log("success update user name");
      //   });

      });
  }
  public removeCathedra(cathedra:Cathedra,cathedraDelrow){
    this.cathedraService.deleteCathedra(cathedra.Id).subscribe(responce => {
      console.log("Delete cathedra : " + cathedra.ShortName);
      this.cathedrasArray.splice(cathedraDelrow, 1);
 
    })


  }
}
