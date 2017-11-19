import { Component } from '@angular/core';
import { JournalService } from '../../shared/journal.service';

@Component({
    selector: 'create-journal',
    templateUrl: './create-journal.component.html'
})
export class CreateJournalComponent {
  public lessonName: string = "";
  public studentsCount: number = 0;
  public labBlocksCount: number = 0;

  public constructor(public journalService: JournalService) {
  }
  public createJournal() {
    this.journalService.addJournal(this.lessonName, this.studentsCount, this.labBlocksCount, localStorage.getItem('TeacherId')).subscribe(response => {
      alert("Журнал успешно добавлен");
      location.reload();
    });
  }

}
