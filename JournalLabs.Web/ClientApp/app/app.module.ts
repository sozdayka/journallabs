import { NgModule, Inject, NgModuleFactory, NgModuleFactoryLoader, RendererFactory2, NgZone } from '@angular/core';

import { RouterModule, PreloadAllModules } from '@angular/router';
import { CommonModule, APP_BASE_HREF } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { ServerModule, ɵServerRendererFactory2 } from '@angular/platform-server';
import { ɵAnimationEngine } from '@angular/animations/browser';
import { BrowserAnimationsModule, ɵAnimationRendererFactory } from '@angular/platform-browser/animations';

import { Ng2BootstrapModule } from 'ngx-bootstrap';

// i18n support
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './containers/home/home.component';
import { UsersComponent } from './containers/users/users.component';
import { UserDetailComponent } from './components/user-detail/user-detail.component';
import { CounterComponent } from './containers/counter/counter.component';
import { ChatComponent } from './containers/chat/chat.component';
import { NotFoundComponent } from './containers/not-found/not-found.component';
import { NgxBootstrapComponent } from './containers/ngx-bootstrap-demo/ngx-bootstrap.component';

import { LinkService } from './shared/link.service';
import { UserService } from './shared/user.service';
import { LabBlockService } from './shared/lab-block.service';
import { JournalService } from './shared/journal.service';
import { ConnectionResolver } from './shared/route.resolver';
import { ORIGIN_URL } from './shared/constants/baseurl.constants';
import { TransferHttpModule } from '../modules/transfer-http/transfer-http.module';

export function createTranslateLoader(http: Http, baseHref) {
  // Temporary Azure hack
  if (baseHref === null && typeof window !== 'undefined') {
    baseHref = window.location.origin;
  }
  // i18n files are in `wwwroot/assets/`
  return new TranslateHttpLoader(http, `${baseHref}/assets/i18n/`, '.json');
}
// declarations
export function instantiateServerRendererFactory(
  renderer: RendererFactory2, engine: ɵAnimationEngine, zone: NgZone) {
  return new ɵAnimationRendererFactory(renderer, engine, zone);
}

const createRenderer = ɵServerRendererFactory2.prototype.createRenderer;
ɵServerRendererFactory2.prototype.createRenderer = function () {
  const result = createRenderer.apply(this, arguments);
  const setProperty = result.setProperty;
  result.setProperty = function () {
    try {
      setProperty.apply(this, arguments);
    } catch (e) {
      if (e.message.indexOf('Found the synthetic') === -1) {
        throw e;
      }
    }
  };
  return result;
}

export const SERVER_RENDER_PROVIDERS = [
  {
    provide: RendererFactory2,
    useFactory: instantiateServerRendererFactory,
    deps: [ɵServerRendererFactory2, ɵAnimationEngine, NgZone]
  }
];
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    UsersComponent,
    UserDetailComponent,
    HomeComponent,
    ChatComponent,
    NotFoundComponent,
    NgxBootstrapComponent
  ],
  imports: [
    CommonModule,
    HttpModule,
    FormsModule,
    ServerModule,
    BrowserAnimationsModule,
    Ng2BootstrapModule.forRoot(), // You could also split this up if you don't want the Entire Module imported

    TransferHttpModule, // Our Http TransferData method

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
        redirectTo: 'home',
        pathMatch: 'full'
      },
      {
        path: 'home', component: HomeComponent,

        // *** SEO Magic ***
        // We're using "data" in our Routes to pass in our <title> <meta> <link> tag information
        // Note: This is only happening for ROOT level Routes, you'd have to add some additional logic if you wanted this for Child level routing
        // When you change Routes it will automatically append these to your document for you on the Server-side
        //  - check out app.component.ts to see how it's doing this
        data: {
          title: 'Homepage',
          meta: [{ name: 'description', content: 'This is an example Description Meta tag!' }],
          links: [
            { rel: 'canonical', href: 'http://blogs.example.com/blah/nice' },
            { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/' }
          ]
        }
      },
      {
        path: 'counter', component: CounterComponent,
        data: {
          title: 'Counter',
          meta: [{ name: 'description', content: 'This is an Counter page Description!' }],
          links: [
            { rel: 'canonical', href: 'http://blogs.example.com/counter/something' },
            { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/counter' }
          ]
        }
      },
      {
        path: 'users', component: UsersComponent,
        data: {
          title: 'Users REST example',
          meta: [{ name: 'description', content: 'This is User REST API example page Description!' }],
          links: [
            { rel: 'canonical', href: 'http://blogs.example.com/chat/something' },
            { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/users' }
          ]
        }
      },
      {
        path: 'chat', component: ChatComponent,
        // Wait until the resolve is finished before loading the Route
        resolve: { connection: ConnectionResolver },
        data: {
          title: 'SignalR chat example',
          meta: [{ name: 'description', content: 'This is an Chat page Description!' }],
          links: [
            { rel: 'canonical', href: 'http://blogs.example.com/chat/something' },
            { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/chat' }
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

      { path: 'lazy', loadChildren: './containers/lazy/lazy.module#LazyModule' },

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
    ConnectionResolver,
    TranslateModule,
    SERVER_RENDER_PROVIDERS,
    LabBlockService,
    JournalService
  ]
})
export class AppModuleShared {
}
