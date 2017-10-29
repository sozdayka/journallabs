import { ILabBlock } from "./LabBlock"

export interface IJournal {
   Id:string;
   TeacherId: string;
   LabBlocks: ILabBlock[];
}
