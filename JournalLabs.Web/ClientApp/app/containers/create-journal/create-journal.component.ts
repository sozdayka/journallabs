import { Component } from '@angular/core';
import { JournalService } from '../../shared/journal.service';
import { UserService } from '../../shared/user.service';
import { CreateJournalViewModel } from "../../models/createJournalViewModel"
import { User } from '../../models/User';
import { LabBlock } from '../../models/LabBlock';
import { LogService } from '../../shared/log.service';
@Component({
    selector: 'create-journal',
    templateUrl: './create-journal.component.html'
})
export class CreateJournalComponent {
  public assistantList: User[] = [];
  public labBlockCount: number = 0;
  public createJournalViewModel: CreateJournalViewModel = new CreateJournalViewModel();
  public constructor(public journalService: JournalService, public userService: UserService, public logService:LogService) {
    var assistants = this.userService.getAllAssistants().subscribe(response => {
      this.assistantList = response;
    });
  }
  public addAssistant(event: any, id: string) {
    if (event.target.checked) {
      this.createJournalViewModel.TeacherIds.push(id);
      return;
    }
    this.createJournalViewModel.TeacherIds = this.createJournalViewModel.TeacherIds.filter(obj => obj !== id);
  }
  public createJournal() {
    var teacherName = localStorage.getItem('TeacherName');
    this.createJournalViewModel.TeacherIds.push(localStorage.getItem('TeacherId'));
    this.journalService.addJournal(this.createJournalViewModel).subscribe(resp => {
      var logText = `${new Date().toLocaleString()} Преподаватель ${teacherName} создал журнал под названием ${this.createJournalViewModel.LessonName}, с количеством студентов ${this.createJournalViewModel.StudentsCount}, и количеством видов работ ${this.createJournalViewModel.LabBlocksSettings.length}`;
      this.logService.writeTeacherLog(logText).subscribe(response => {
        alert("Журнал успешно добавлен");
        location.reload();
      });
    });
  }
  public fillLabBlockSettingsArray() {
    this.createJournalViewModel.LabBlocksSettings = [];
    for (var i = 0; i < this.labBlockCount; i++) {
      this.createJournalViewModel.LabBlocksSettings.push(new LabBlock());
    }
  }
  public selectBooleanTypeField(key: LabBlock, isChecked: any) {
    key.IsBoolField = isChecked;
    if (isChecked) {
      key.IsCalculateMark = false;
    }
    
  }



  // igor add
  public getSelected() {
    this.groupselecte = true;

    this.studList = [];
    this.selected_groups = this.groups.filter(s => {
      this.studenFromGroup.forEach(eachObj => {
        if (eachObj.id == s.id && s.selected == true) {
          this.studList.push(eachObj);
        }
      });
      return s.selected;
    });

  }



  public groupselecte = false;


  public selected_groups;
  public filteredItems: any[] = new Array();

  public studList;
  public groups = [
    {
      name: 'kui-156',
      id: 1,
      selected: false
    },
    {
      name: 'kui 14-1',
      id: 2,
      selected: false
    }

  ]
  public studenFromGroup = [
    {
      name: 'kui-156',
      id: 1,
      selected: true,
      studenList: [
        {
          studentName: 'Vlad Sas',
          studentId: 0,
          studentSelected: true
        },
        {
          studentName: 'Artem Swenton',
          studentId: 1,
          studentSelected: false
        }

      ]
    }, {
      name: 'kui 14-1',
      id: 2,
      selected: false,
      studenList: [
        {
          studentName: 'Vadim Sulidov',
          studentId: 0,
          studentSelected: true
        },
        {
          studentName: 'Denis Tonoto',
          studentId: 1,
          studentSelected: false
        }, {
          studentName: 'Ivan Ivanov',
          studentId: 2,
          studentSelected: false
        },
        {
          studentName: 'Igor Sidorov',
          studentId: 3,
          studentSelected: false
        }

      ]
    }

  ]

}
