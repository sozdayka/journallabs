import { Component, OnInit } from '@angular/core';
import { TableData } from './table-data';
import { RowContentComponent } from './row-content.component';
import { JournalService } from '../../shared/journal.service';
import { JournalViewModel } from '../../models/journalViewModel';

@Component({
  selector: 'teacher-journals',
  templateUrl:'teacher-journals.component.html'
})
export class TeacherJournalsComponent implements OnInit {
  name: string;
  public journalViewModel: JournalViewModel = new JournalViewModel();
  public pasrseArray:any=[];
  countBlocks: number[] = []; 
  public rows: Array<any> = [];
  public isNewJournal: boolean = false;
  public lessonName: string = "";
  public studentsCount: number = 0;
  public labBlocksCount: number = 0;


  public constructor(public journalService:JournalService) {
  }

  public ngOnInit(): void {
    this.getJournal();
  }

  public createJournal() {
    this.journalService.addJournal(this.lessonName, this.studentsCount, this.labBlocksCount, localStorage.getItem('TeacherId')).subscribe(response => {
      this.isNewJournal = !this.isNewJournal
    });
  }

  public getJournal() {
    this.journalService.getJournal('42570A60-834E-44D1-AC0F-87BB12B07B65').subscribe(response => {
      var resp = JSON.stringify(response);
      this.journalViewModel = JSON.parse(resp);
      for (var i = 0; i < this.journalViewModel.StudentResultForJournal[0].StudentLabBlocks.length; i++) {
        this.countBlocks.push(i);
      }
      
      
      //this.isNewJournal = !this.isNewJournal
    });
  }
}
