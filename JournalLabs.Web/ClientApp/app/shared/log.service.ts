import { Injectable, Inject } from '@angular/core';
import { Http, URLSearchParams, Headers, RequestOptions } from '@angular/http';
import { APP_BASE_HREF } from '@angular/common';
import { ORIGIN_URL } from './constants/baseurl.constants';
import { User } from '../models/User';
import { TransferHttp } from '../../modules/transfer-http/transfer-http';
import { Observable } from 'rxjs/Observable';
import { REQUEST } from './constants/request';
import { AssistantsJournalViewModel } from '../models/assistantsJournalViewModel';
import { TeacherJournal } from '../models/teacherJournal';

@Injectable()
export class LogService {
  constructor(
    private transferHttp:
      TransferHttp,
    private http: Http,
    @Inject(REQUEST) private baseUrl: string) {

  }

  writeTeacherLog(data: string): Observable<any> {
    //return this.transferHttp.get(`${this.baseUrl}/api/Log/WriteTeacherLog?data=` + data);
    return this.transferHttp.get(`${this.baseUrl}/api/Group/GetGroups`);
  }

  writeDevelopmentLog(data: string): Observable<any> {
    return this.transferHttp.get(`${this.baseUrl}/api/Log/WriteDevelopmentLog?data=` + data);
  }
}
