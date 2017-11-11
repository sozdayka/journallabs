import { NgModule, Inject } from '@angular/core';
import { Router, RouterModule, PreloadAllModules } from '@angular/router';
import { CommonModule, APP_BASE_HREF } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { FormsModule, NgModel } from '@angular/forms';

import { Ng2BootstrapModule } from 'ngx-bootstrap';

// i18n support
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { StudentJournalsComponent } from './containers/student-journals/student-journals.component';
import { UserDetailComponent } from './components/user-detail/user-detail.component';
import { AdminComponent } from './containers/admin/admin.component';
// import { ChatComponent } from './containers/chat/chat.component';
import { NotFoundComponent } from './containers/not-found/not-found.component';
import { NgxBootstrapComponent } from './containers/ngx-bootstrap-demo/ngx-bootstrap.component';
import { Ng2TableModule } from 'ng2-expanding-table/components/ng-table-module';

import { LinkService } from './shared/link.service';
import { UserService } from './shared/user.service';
// import { ConnectionResolver } from './shared/route.resolver';
import { ORIGIN_URL } from './shared/constants/baseurl.constants';
import { TransferHttpModule } from '../modules/transfer-http/transfer-http.module';
import { TeacherJournalsComponent } from './containers/teacher-journals/teacher-journals.component';
import { SignInComponent } from './containers/sign-in/sign-in.component';
import { RowContentComponent } from './containers/teacher-journals/row-content.component';
import { LabBlockService } from './shared/lab-block.service';
import { JournalService } from './shared/journal.service';
import { KindOfWorkService } from './shared/kind-of-work.service';
import { StudentService } from './shared/student.service';

export function createTranslateLoader(http: Http, baseHref) {
    // Temporary Azure hack
    if (baseHref === null && typeof window !== 'undefined') {
        baseHref = window.location.origin;
    }
    // i18n files are in `wwwroot/assets/`
    return new TranslateHttpLoader(http, `${baseHref}/assets/i18n/`, '.json');
}

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        AdminComponent,
      StudentJournalsComponent,
        UserDetailComponent,
        // ChatComponent,
        NotFoundComponent,
      NgxBootstrapComponent,
      TeacherJournalsComponent,
      SignInComponent,
      RowContentComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        Ng2BootstrapModule.forRoot(), // You could also split this up if you don't want the Entire Module imported

        TransferHttpModule, // Our Http TransferData method
        Ng2TableModule,

        // i18n support
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [Http, [ORIGIN_URL]]
            }
        }),

        // App Routing
        RouterModule.forRoot([
            {
                path: '',
                redirectTo: 'student-journals',
                pathMatch: 'full'
            },           
            {
                path: 'admin', component: AdminComponent,
                data: {
                    title: 'Admin',
                    meta: [{ name: 'description', content: 'This is an Admin page Description!' }],
                    links: [
                        { rel: 'canonical', href: 'http://blogs.example.com/counter/something' },
                        { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/counter' }
                    ]
                }
            },
            {
              path: 'student-journals', component: StudentJournalsComponent,
                data: {
                  title: 'Student Journals',
                  meta: [{ name: 'description', content: 'Student Journals' }],
                    links: [
                        { rel: 'canonical', href: 'http://blogs.example.com/chat/something' },
                        { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/users' }
                    ]
                }
            },
            {
                path: 'ngx-bootstrap', component: NgxBootstrapComponent,
                data: {
                    title: 'Ngx-bootstrap demo!!',
                    meta: [{ name: 'description', content: 'This is an Demo Bootstrap page Description!' }],
                    links: [
                        { rel: 'canonical', href: 'http://blogs.example.com/bootstrap/something' },
                        { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/bootstrap-demo' }
                    ]
                }
            },
            {
              path: 'teacher-journals', component: TeacherJournalsComponent,
              data: {
                title: 'Teacher journals',
                meta: [{ name: 'description', content: 'Teacher journals' }],
                links: [
                  { rel: 'canonical', href: 'http://blogs.example.com/bootstrap/something' },
                  { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/bootstrap-demo' }
                ]
              }
            },
            {
              path: 'sign-in', component: SignInComponent,
              data: {
                title: 'Sign In',
                meta: [{ name: 'description', content: 'sign-in' }],
                links: [
                  { rel: 'canonical', href: 'http://blogs.example.com/bootstrap/something' },
                  { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/bootstrap-demo' }
                ]
              }},

            {
                path: '**', component: NotFoundComponent,
                data: {
                    title: '404 - Not found',
                    meta: [{ name: 'description', content: '404 - Error' }],
                    links: [
                        { rel: 'canonical', href: 'http://blogs.example.com/bootstrap/something' },
                        { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/bootstrap-demo' }
                    ]
                }
            }
          
        ], {
          // Router options
          useHash: false,
          preloadingStrategy: PreloadAllModules,
          initialNavigation: 'enabled'
        })
    ],
    providers: [
        LinkService,
        UserService,
        // ConnectionResolver,
        TranslateModule,
        LabBlockService,
        JournalService,
        StudentService,
        LabBlockService
    ]
})
export class AppModuleShared {
}
