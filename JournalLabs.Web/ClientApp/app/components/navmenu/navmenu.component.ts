import { Component,OnInit } from '@angular/core';
import { Router, CanActivate, NavigationEnd } from '@angular/router';
import { JournalService } from "../../shared/journal.service";
import { Journal } from '../../models/Journal';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})

export class NavMenuComponent implements OnInit {
    collapse: string = "collapse";
    currentRole: string = "";
    public teacherJournals:Journal[]=[];
    constructor(public router: Router,
      private journalService: JournalService) {
      
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
        this.journalService.getAllJournalsByTeacherId(localStorage.getItem('TeacherId')).subscribe(response => {
          this.teacherJournals = JSON.parse(response._body);
        });
      }
      
    }

    signOut() {
      localStorage.removeItem('Role');
      location.reload();
      this.router.navigate(['sign-in']);
    }

    collapseMenu() {
          this.collapse = "collapse"
      }
}
