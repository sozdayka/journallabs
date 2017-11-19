import { Component, OnInit } from '@angular/core';
import { JournalService } from '../../shared/journal.service';
import { StudentService } from '../../shared/student.service';
import { LabBlockService } from '../../shared/lab-block.service';
import { KindOfWorkService } from '../../shared/kind-of-work.service';
import { JournalViewModel } from '../../models/journalViewModel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Student } from "../../models/Student"
import { KindOfWork } from "../../models/kind-of-work"
import { LabBlock } from "../../models/LabBlock"
@Component({
  selector: 'journal',
  templateUrl:'journal.component.html'
})
export class JournalComponent implements OnInit {
  name: string;
  public journalViewModel: JournalViewModel = null;
  public pasrseArray:any=[];
  countBlocks: number[] = []; 
  public rows: Array<any> = [];
  public isNewJournal: boolean = false;
  public lessonName: string = "";
  public studentsCount: number = 0;
  public labBlocksCount: number = 0;

  
  public constructor(public journalService: JournalService,
    public studentService: StudentService,
    public labBlockService: LabBlockService,
    public kindOfWorkService: KindOfWorkService,
    private activatedRoute: ActivatedRoute
  ) {
  }

  public ngOnInit(): void {
    //this.activatedRoute.params.subscribe(params => {
    //  let teacherId = params["journalId"];
    //  this.getJournal(teacherId);
    //});
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      let journalId = params['journalId'];
      let studentId = params['studentId'];
      if (typeof (studentId)=='undefined') {
        this.getJournal(journalId);
      }
      if (typeof (studentId) != 'undefined') {
        this.getStudentJournal(journalId, studentId);
      }

    });
    
  }

  public createJournal() {
    this.journalService.addJournal(this.lessonName, this.studentsCount, this.labBlocksCount, localStorage.getItem('TeacherId')).subscribe(response => {
      this.isNewJournal = !this.isNewJournal;
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
}
