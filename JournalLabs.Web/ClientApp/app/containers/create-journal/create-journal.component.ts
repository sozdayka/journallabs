import { Component } from '@angular/core';
import { JournalService } from '../../shared/journal.service';
import { UserService } from '../../shared/user.service';
import { CreateJournalViewModel } from "../../models/createJournalViewModel"
import { User } from '../../models/User';
import { LabBlock } from '../../models/LabBlock';
@Component({
    selector: 'create-journal',
    templateUrl: './create-journal.component.html'
})
export class CreateJournalComponent {
  public assistantList: User[] = [];
  public labBlockCount: number = 0;
  public createJournalViewModel: CreateJournalViewModel = new CreateJournalViewModel();
  public constructor(public journalService: JournalService,public userService:UserService) {
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
    this.createJournalViewModel.TeacherIds.push(localStorage.getItem('TeacherId'));
    this.journalService.addJournal(this.createJournalViewModel).subscribe(response => {
      alert("Журнал успешно добавлен");
      location.reload();
    });
  }
  public fillLabBlockSettingsArray() {
    this.createJournalViewModel.LabBlocksSettings = [];
    for (var i = 0; i < this.labBlockCount; i++) {
      this.createJournalViewModel.LabBlocksSettings.push(new LabBlock());
    }
  }
}
