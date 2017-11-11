import { Component,OnInit } from '@angular/core';
import { Router, CanActivate, NavigationEnd } from '@angular/router';
@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})

export class NavMenuComponent implements OnInit {
    collapse: string = "collapse";
    currentRole:string = "";
    constructor(public router: Router) {
      
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
