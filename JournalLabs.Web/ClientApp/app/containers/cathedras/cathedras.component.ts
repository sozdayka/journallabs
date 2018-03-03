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

      //this.groupsArr.push({
      //  gName: this.groupName,
        
    //});
    this.cathedraService.addCathedra(this.newCathedra).subscribe(responce => {
      this.newCathedra = new Cathedra();
      console.log("Group create successfully");
      this.loadCathedras();
    })
      //this.groupName = '';
    /* this.groupStudentCount = 0;*/
  }
  public changeCathedraName(cathedra: Cathedra) {
    this.cathedraService.updateCathedra(cathedra).subscribe(responce => {
      console.log("Change cathedra Name: " + cathedra.FullName);
    });
    
  }
  public removeCathedra(cathedra:Cathedra,cathedraDelrow){
    this.cathedraService.deleteCathedra(cathedra.Id).subscribe(responce => {
      console.log("Delete cathedra : " + cathedra.ShortName);
      this.cathedrasArray.splice(cathedraDelrow, 1);
 
    })


  }
}
