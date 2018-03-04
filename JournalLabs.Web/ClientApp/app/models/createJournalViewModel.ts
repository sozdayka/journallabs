import { LabBlock } from "./LabBlock"
import { Student } from "./Student";
export class CreateJournalViewModel {
  LessonName: string = "";
  GroupName: string = "";
  IsExam: string = "";
  StudentsCount: number = 0;
  LabBlocksSettings: LabBlock[] = [];
  TeacherIds: string[] = [];

  Students: Student[] = [];

}
