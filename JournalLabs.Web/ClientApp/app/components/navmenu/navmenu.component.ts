import { Component,OnInit } from '@angular/core';
import { Router, CanActivate, NavigationEnd } from '@angular/router';
import { JournalService } from "../../shared/journal.service";
import { Journal } from '../../models/Journal';
import { StudentJournal } from '../../models/studentJournal';
import { LogService } from '../../shared/log.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})

export class NavMenuComponent implements OnInit {
    collapse: string = "collapse";
    currentRole: string = "";
    public teacherJournals: Journal[] = [];
    public studentJournals: StudentJournal[] = [];
    public studentName:string="";
    constructor(public router: Router,
      private journalService: JournalService,
      private logService: LogService) {
      
    }
    collapseNavbar(): void {
        if (this.collapse.length > 1) {
            this.collapse = "";
        } else {
            this.collapse = "collapse";
        }
    }

    ngOnInit(): void {

      if (localStorage.getItem('Role') != null) {
        this.currentRole = localStorage.getItem('Role');
      }
      if (localStorage.getItem('TeacherId')!=null) {
        this.journalService.getAllJournalsByTeacherId(localStorage.getItem('TeacherId'), this.currentRole).subscribe(response => {
          if (response._body!="null") {
            this.teacherJournals = JSON.parse(response._body);
            this.router.navigate(['journal'], { queryParams: { journalId: this.teacherJournals[0].Id } });
          }       
        });
      }
      if (this.studentName != "") {
        this.journalService.getAllStudentJournalsByStudentName(this.studentName).subscribe(response => {
          this.studentJournals = JSON.parse(response._body);
          this.router.navigate(['journal'], { queryParams: { journalId: this.studentJournals[0].JournalId, studentId: this.studentJournals[0].StudentId } });
        });
      }
      this.router.navigate(['sign-in']);
    }

    signOut() {
      localStorage.removeItem('Role');
      localStorage.removeItem('TeacherId');
      location.reload();
      this.router.navigate(['sign-in']);
    }

    collapseMenu() {
          this.collapse = "collapse"
    }

    public StudentSearch() {
      this.journalService.getAllStudentJournalsByStudentName(this.studentName).subscribe(response => {
        this.studentJournals = JSON.parse(response._body);
        this.router.navigate(['journal'], { queryParams: { journalId: this.studentJournals[0].JournalId, studentId: this.studentJournals[0].StudentId } });
      });
    console.log(this.studentName);
    }
    public addDevelopmentRemak(event: any) {
      this.logService.writeDevelopmentLog(`${new Date().toLocaleString()} -- ${event.target.value}`).subscribe(resp => {
        console.log("Лог успешно записан");
      }); 
    }
}
