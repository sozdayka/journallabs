import { LabBlock } from "./LabBlock"
import { Student } from "./Student";
export class CreateJournalViewModel {
  LessonName: string = "";
  GroupName: string = "";
  IsExam: string = "";
  LabBlocksSettings: LabBlock[] = [];
  TeacherIds: string[] = [];

  Students: Student[] = [];

}
