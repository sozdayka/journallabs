import { KindOfMark } from "./enums/KindOfMark"
export class LabBlock {
   Id:string = "";
   Date:string = "";
   Mark:number =0;
   MarkTeacherId: string = "";
   KindOfMark: KindOfMark = KindOfMark.FirstMark;
   UserFIO:string = "";
   LessonName:string = "";
   JournalId: string = "";
   IsKindOfWorkVisible: boolean = false;
   IsCalculateMark: boolean = false;
   IsVisibleToStudent: boolean = false;
   IsBoolField: boolean = false;
}
