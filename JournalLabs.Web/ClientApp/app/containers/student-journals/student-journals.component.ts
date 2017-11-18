import {Component, OnInit} from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../shared/user.service';
import { TableData } from '../teacher-journals/table-data';
import { RowContentComponent } from '../teacher-journals/row-content.component';
@Component({
    selector: 'student-journals',
    templateUrl: './student-journals.component.html'
    
})
export class StudentJournalsComponent implements OnInit {

  

    public constructor() {
    }

    public ngOnInit(): void {
    }

  studentName: string = "";
    public StudentSearch() {}
}
