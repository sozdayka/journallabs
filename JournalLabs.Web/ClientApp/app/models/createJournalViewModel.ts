import { LabBlock } from "./LabBlock"
export class CreateJournalViewModel {
  LessonName: string = "";
  GroupName: string = "";
  IsExam: string = "";
  StudentsCount: number = 0;
  LabBlocksSettings: LabBlock[] = [];
  TeacherIds: string[] = [];

  // public Guid? Id { get; set; }
  //       public bool IsExam { get; set; }
  //       public string GroupName { get; set; }
  //       public string LessonName { get; set; }
  //       public virtual List<LabBlock> LabBlocks { get; set; }
}
