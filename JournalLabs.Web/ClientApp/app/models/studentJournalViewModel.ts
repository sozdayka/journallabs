import { Student } from "./Student"
import { LabBlock } from "./LabBlock"

export class StudentJournalViewModel {
  StudentInfo: Student = new Student();
  StudentLabBlocks: LabBlock[] = [];
}
