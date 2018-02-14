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
import { User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { AssistantsJournalViewModel } from '../../models/assistantsJournalViewModel';
import { TeacherJournalService } from '../../shared/teacher-journal';
import { TeacherJournal } from '../../models/teacherJournal';
@Component({
  selector: 'journal',
  templateUrl: 'journal.component.html'
})
export class JournalComponent implements OnInit {
  name: string;
  public journalViewModel: JournalViewModel = null;
  public oldJournalViewModel: JournalViewModel = null;
  public pasrseArray: any = [];
  public headerKindOfWork: KindOfMark[] = [];
  public studentId = "";
  public kindOfMark = KindOfMark;
  public assistantList: AssistantsJournalViewModel[] = [];
  public journalId: string = "";
  public currentRole: string = "";
  public currentTeacherId: string = "";
  public currentDate: string = "";
  public constructor(public journalService: JournalService,
    public studentService: StudentService,
    public labBlockService: LabBlockService,
    public kindOfWorkService: KindOfWorkService,
    public remarkService: RemarkService,
    private activatedRoute: ActivatedRoute,
    public userService: UserService,
    public teacherJournalService: TeacherJournalService
  ) {
  }

  public ngOnInit(): void {
    //this.activatedRoute.params.subscribe(params => {
    //  let teacherId = params["journalId"];
    //  this.getJournal(teacherId);
    //});
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    var date = "";
    var month = "";
    if (dd < 10) {
      date = '0' + dd;
    }
    if (mm < 10) {
      month = '0' + mm;
    }
    this.currentDate = date + '.' + month + '.' + yyyy;
    this.currentRole = localStorage.getItem('Role');
    this.currentTeacherId = localStorage.getItem('TeacherId');
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.headerKindOfWork = [];
      this.journalId = params['journalId'];
      this.studentId = params['studentId'];
      if (typeof (this.studentId) == 'undefined') {
        this.getJournal(this.journalId);
      }
      if (typeof (this.studentId) != 'undefined') {
        this.getStudentJournal(this.journalId, this.studentId);
      }
      var assistants = this.teacherJournalService.getAllJournalAssistants(this.journalId).subscribe(response => {
        this.assistantList = response;
      });
    });

  }

  public changeAssistant(event: any, assistant: AssistantsJournalViewModel) {
    if (event.target.checked) {
      var teacherJournal: TeacherJournal = new TeacherJournal();
      teacherJournal.JournalId = this.journalId;
      teacherJournal.TeacherId = assistant.Id
      this.teacherJournalService.addTeacherToJournal(teacherJournal).subscribe(
        result => {
          console.log("success add assistant");
          this.assistantList = [];
          this.teacherJournalService.getAllJournalAssistants(this.journalId).subscribe(response => {
            this.assistantList = response;
          });
        });     
      return;
    }
    this.teacherJournalService.deleteTeacherFromJournal(assistant.TeacherJournalId).subscribe(
      result => {
        console.log("success remove assistant");
      });
  }

  public getJournal(journalId: string) {
    this.journalService.getJournal(journalId).subscribe(response => {
      var resp = JSON.stringify(response);
      this.journalViewModel = JSON.parse(resp);

      for (let studentResultForJournal of this.journalViewModel.StudentResultForJournal)
        for (let studentLabBlock of studentResultForJournal.StudentLabBlocks)
          studentLabBlock.oldMark = studentLabBlock.Mark;

      for (var i = 0; i < this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks.length; i++) {
        this.headerKindOfWork.push(this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks[i].KindOfMark);
      }
    });
  }
  public getStudentJournal(journalId: string, studentId: string) {
    this.journalService.getJournalByIdAndStudentId(journalId, studentId).subscribe(response => {
      this.journalViewModel = JSON.parse(response._body);

      //for (let studentResultForJournal of this.journalViewModel.StudentResultForJournal)
      //  for (let studentLabBlock of studentResultForJournal.StudentLabBlocks)
      //    studentLabBlock.oldMark = studentLabBlock.Mark;

      for (var i = 0; i < this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks.length; i++) {
        this.headerKindOfWork.push(this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks[i].KindOfMark);
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
  public totalMark(labBlocks: LabBlock[]): number {
    let sum: number = 0;
    for (let labBlock of labBlocks) {
      if (labBlock.IsCalculateMark) {
        sum += labBlock.Mark;// > labBlock.SecondMark ? labBlock.FirstMark : labBlock.SecondMark;
      }

    }
    return sum;
  }
  public changeLabBlock(labBlock: LabBlock) {
    if (labBlock.Date != null && labBlock.Mark!=0) {    
    labBlock.MarkTeacherId = this.currentTeacherId;
    labBlock.MarkTeacherName = localStorage.getItem('TeacherName');

    if (labBlock.oldMark != labBlock.Mark && labBlock.oldMark!=0) {
      if (labBlock.oldMark > labBlock.Mark) {
        labBlock.Color = "red";        
      }
      if (labBlock.oldMark < labBlock.Mark) {
        labBlock.Color = "green";
      }     
    }
    labBlock.oldMark = labBlock.Mark;
    this.labBlockService.updateLabBlock(labBlock).subscribe(
      result => {
        console.log("success update labBlock");
      });
    }
  }
  public removeStudent(id: string) {
    debugger;
    this.studentService.deleteStudent(id).subscribe(
      result => {
        console.log("success remove student");
        location.reload();
      });
  }
}
