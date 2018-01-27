import { Component, OnInit } from '@angular/core';
import { JournalService } from '../../shared/journal.service';
import { StudentService } from '../../shared/student.service';
import { LabBlockService } from '../../shared/lab-block.service';
import { KindOfWorkService } from '../../shared/kind-of-work.service';
import { RemarkService } from '../../shared/remark.service';
import { JournalViewModel } from '../../models/journalViewModel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Student } from "../../models/Student"
import { KindOfWork } from "../../models/kind-of-work"
import { LabBlock } from "../../models/LabBlock"
import { Remark } from "../../models/Remark"
import { KindOfMark } from '../../models/enums/KindOfMark';
@Component({
  selector: 'journal',
  templateUrl:'journal.component.html'
})
export class JournalComponent implements OnInit {
  name: string;
  public journalViewModel: JournalViewModel = null;
  public pasrseArray:any=[];
  countBlocks: number[] = []; 
  public studentId = "";
  public kindOfMark= KindOfMark ;
  public constructor(public journalService: JournalService,
    public studentService: StudentService,
    public labBlockService: LabBlockService,
    public kindOfWorkService: KindOfWorkService,
    public remarkService: RemarkService,
    private activatedRoute: ActivatedRoute
  ) {
  }

  public ngOnInit(): void {
    //this.activatedRoute.params.subscribe(params => {
    //  let teacherId = params["journalId"];
    //  this.getJournal(teacherId);
    //});
    
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.countBlocks = [];
      let journalId = params['journalId'];
      this.studentId = params['studentId'];
      if (typeof (this.studentId)=='undefined') {
        this.getJournal(journalId);
      }
      if (typeof (this.studentId) != 'undefined') {
        this.getStudentJournal(journalId, this.studentId);
      }

    });
    
  }

  public getJournal(journalId:string) {
    this.journalService.getJournal(journalId).subscribe(response => {
      var resp = JSON.stringify(response);
      this.journalViewModel = JSON.parse(resp);
      for (var i = 0; i < this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks.length; i++) {
        this.countBlocks.push(i);
      }          
    });
  }
  public getStudentJournal(journalId: string,studentId:string) {
    this.journalService.getJournalByIdAndStudentId(journalId, studentId).subscribe(response => {
        this.journalViewModel = JSON.parse(response._body);
      for (var i = 0; i < this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks.length; i++) {
        this.countBlocks.push(i);
      }
    });
  }

  public changeUserName(student: Student) {
    this.studentService.updateStudent(student).subscribe(
      result => {
        console.log("success update user name");
      });
  }
  public changeKindOfWorkName(kindOfWork: KindOfWork) {
    this.kindOfWorkService.updateKindOfWork(kindOfWork).subscribe(
      result => {
        console.log("success update kindOfWork name");
      });
  }
  public changeRemark(remark: Remark) {
    this.remarkService.updateRemark(remark).subscribe(
      result => {
        console.log("success update remark");
      });
  }
  public totalMark(labBlocks: LabBlock[]):number
  {
    let sum:number = 0;
    for (let labBlock of labBlocks) {
      sum += labBlock.Mark;// > labBlock.SecondMark ? labBlock.FirstMark : labBlock.SecondMark;
    }
    return sum;
  }
  public changeLabBlock(labBlock: LabBlock) {
    this.labBlockService.updateLabBlock(labBlock).subscribe(
      result => {
        console.log("success update labBlock");
      });
  }
}
