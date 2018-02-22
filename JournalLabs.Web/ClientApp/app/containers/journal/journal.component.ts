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
import { HeaderKindOfWorkBlock } from '../../models/headerKindOfWorkBlock';
import { User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { AssistantsJournalViewModel } from '../../models/assistantsJournalViewModel';
import { TeacherJournalService } from '../../shared/teacher-journal';
import { TeacherJournal } from '../../models/teacherJournal';
import { KindOfMark } from "../../models/enums/KindOfMark"
import { LogService } from '../../shared/log.service';
@Component({
  selector: 'journal',
  templateUrl: 'journal.component.html'
})
export class JournalComponent implements OnInit {
  name: string;
  public journalViewModel: JournalViewModel = null;
  //public headerKindOfWork: KindOfMark[] = [];
  public headerKindOfWork: HeaderKindOfWorkBlock[] = [];
  public kindOfMark = KindOfMark;
  public studentId = "";
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
    public teacherJournalService: TeacherJournalService,
    public logService: LogService
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

    var date = dd.toString();
    var month = mm.toString();
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
          var teacherName = localStorage.getItem('TeacherName');
          var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} предоставил доступ к журналу ${this.journalViewModel.JournalModel.LessonName} ассистенту ${assistant.Name}`;
          this.logService.writeTeacherLog(logText).subscribe(resp => {
            console.log("success add assistant");
            this.assistantList = [];
            this.teacherJournalService.getAllJournalAssistants(this.journalId).subscribe(response => {
              this.assistantList = response;
            });
          });          
        });     
      return;
    }
    this.teacherJournalService.deleteTeacherFromJournal(assistant.TeacherJournalId).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} предоставил заблокировал доступ к журналу ${this.journalViewModel.JournalModel.LessonName} ассистенту ${assistant.Name}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success remove assistant");
        });
      });
  }

  public getJournal(journalId: string) {
    this.journalService.getJournal(journalId).subscribe(response => {
      var resp = JSON.stringify(response);
      this.journalViewModel = JSON.parse(resp);

      for (let studentResultForJournal of this.journalViewModel.StudentResultForJournal)
        for (let studentLabBlock of studentResultForJournal.StudentLabBlocks)
          studentLabBlock.oldMark = studentLabBlock.Mark;
      //--------review
      var labBlocksForOneStudent = this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks;

      for (var i = 0; i < labBlocksForOneStudent.length; i++) {       
        this.headerKindOfWork.push({
          KindOfMark: labBlocksForOneStudent[i].KindOfMark,
          isVisible: labBlocksForOneStudent[i].KindOfMark == KindOfMark.FirstMark ? true : false,
          kindOfWorkId: labBlocksForOneStudent[i].KindOfWorkId
        });
      }
      //---------------
    });
  }
  public getStudentJournal(journalId: string, studentId: string) {
    this.journalService.getJournalByIdAndStudentId(journalId, studentId).subscribe(response => {
      this.journalViewModel = JSON.parse(response._body);
      //--------review
      var labBlocksForOneStudent = this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks;

      for (var i = 0; i < labBlocksForOneStudent.length; i++) {
        this.headerKindOfWork.push({
          KindOfMark: labBlocksForOneStudent[i].KindOfMark,
          isVisible: labBlocksForOneStudent[i].KindOfMark == KindOfMark.FirstMark ? true : false,
          kindOfWorkId: labBlocksForOneStudent[i].KindOfWorkId
        });
      }
      //----------- 
    });
  }

  public changeUserName(student: Student) {
    this.studentService.updateStudent(student).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил имя студента под Id ${student.Id} на ${student.StudentName}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success update user name");
        });

      });
  }
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
  public changeRemark(remark: Remark) {
    this.remarkService.updateRemark(remark).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил блок заметок под Id ${remark.Id} на текст заметки ${remark.RemarkText}, видимость студента  ${remark.IsHideStudent}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success update remark");
        });       
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

  public changeLabBlock(labBlock: LabBlock, event: any) {
    if (labBlock.Date != null) {    
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
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил блок лабораторной работы под Id ${labBlock.Id} на дату ${labBlock.Date}, оценку ${labBlock.Mark}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success update labBlock");
        });        
      });
    }
  }

  public removeStudent(id: string) {
    this.studentService.deleteStudent(id).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} удалил студента под Id ${id}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success remove student");
          location.reload();
        }); 
      });
  }
  public addStudentToJournal() {
    this.journalService.addStudentToJournal(this.journalId).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} добавил нового студента`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success add student");
          location.reload();
        }); 
      });
  }
  public changeVisibleKindOfWork(idKindOfWork: string, isChecked: any) {
    this.kindOfWorkService.updateVisibleKindOfWork(idKindOfWork, isChecked).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил видимость вида работы под Id ${idKindOfWork} на ${isChecked}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success update visible kindOfWork");
          location.reload();
        });
      });
  }
  public removeKindOfWork(idKindOfWork: string) {
    this.kindOfWorkService.deleteKindOfWork(idKindOfWork).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} удалил вид работы под Id ${idKindOfWork}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success remove kindOfWork");
          location.reload();
        }); 
      });
  }
  public addKindOfWorkToJournal() {
    this.journalService.addKindOfWorkToJournal(this.journalId).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} добавил новый вид работы в журнале под ID ${this.journalId}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success add kindOfWork");
          location.reload();
        }); 
      });
  }
  public changeVisibleSecondBlock(key: KindOfWork, isChecked: any) {

    key.isSecondBlockVisible = isChecked;

    var kindOfWork = this.headerKindOfWork.filter(x => x.KindOfMark == KindOfMark.SecondMark && x.kindOfWorkId == key.Id)[0];
    if (kindOfWork) {
      kindOfWork.isVisible = isChecked;
    }

    for (var i = 0; i < this.journalViewModel.StudentResultForJournal.length; i++) {
      var labBlock = this.journalViewModel.StudentResultForJournal[i].StudentLabBlocks.filter(x => x.KindOfMark == KindOfMark.SecondMark && x.KindOfWorkId == key.Id)[0];
      if (labBlock) {
        labBlock.IsSecondBlock = isChecked;
      }
    }
  }
  public changeVisibleKindOfWorkForStudent(idKindOfWork: string, isChecked: any) {
    this.kindOfWorkService.updateVisibleKindOfWorkForStudent(idKindOfWork, isChecked).subscribe(
      result => {
        var teacherName = localStorage.getItem('TeacherName');
        var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} изменил видимость вида работы для студента под Id ${idKindOfWork} на ${isChecked}`;
        this.logService.writeTeacherLog(logText).subscribe(resp => {
          console.log("success update visible kindOfWork for student");
          location.reload();
        });
      });
  }
  
}
