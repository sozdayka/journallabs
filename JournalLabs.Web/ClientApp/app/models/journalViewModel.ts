import { Journal } from "./Journal"
import { KindOfWork } from "./kind-of-work"
import { StudentJournalViewModel } from "./studentJournalViewModel"

export class JournalViewModel {
  JournalModel: Journal = new Journal();
  KindsOfWorkForJournal: KindOfWork[] = [];
  StudentResultForJournal: StudentJournalViewModel[] = [];
}
