import { Component, Output, OnInit, EventEmitter} from '@angular/core';
import { Group } from '../../models/Group';
import { Student } from '../../models/Student';
import { SubgroupStudents } from '../../models/SubgroupStudents';
import { AddStudentToJournalViewModel } from '../../models/addStudentToJournalViewModel';
import { GroupService } from '../../shared/group.service';
import { StudentService } from '../../shared/student.service';

@Component({
  selector: 'group-students',
    templateUrl: './group-students.component.html'
})
export class GroupStudentsComponent implements OnInit {
  @Output() groupStudents: EventEmitter<Student[]> = new EventEmitter<Student[]>();;
    public groupsArray: Group[] = [];
    public isGroupSelected: boolean = false;
    public subgroupStudents: SubgroupStudents[] = [];
    public students: Student[] = [];

    constructor(private groupService: GroupService, private studentService: StudentService
    ) { } //private studentService: StudentService
    ngOnInit() {
      this.loadGroups();
    }
    public loadGroups() {
      this.groupService.getGroups().subscribe(data => {
        this.groupsArray = [];
        var responseArray = JSON.stringify(data);
        this.groupsArray = JSON.parse(responseArray);
        console.log("Groups loaded successfully");
      });
    }
    public getGroupStudents(group: Group, event: any, index: number) {
      if (event.target.checked) {
        this.studentService.getStudentsByGroupId(group.Id).subscribe(data => {
          this.subgroupStudents.push({
            groupName: group.Name,
            students: data
          });
          console.log("Group loaded successfully");
        });
        return;
      }
      this.subgroupStudents = this.subgroupStudents.filter(obj => obj.groupName !== group.Name);
      //this.subgroupStudents.splice(this.subgroupStudents.indexOf(x=>x.), 1);
    }
    public selectStudent(student: Student, event: any, index: number) {
      if (event.target.checked) {
        this.students.push(student);
        this.groupStudents.emit(this.students);
        return;
      }
      this.students = this.students.filter(obj => obj !== student);
      this.groupStudents.emit(this.students);
      //this.addStudentToJournalViewModel.Students.splice(index,1);
    }

}
