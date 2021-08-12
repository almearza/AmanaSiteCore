import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination'
import { ButtonsModule } from 'ngx-bootstrap/buttons'
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { HasRoleDirective } from './_directives/has-role.directive';
import { HomeComponent } from './home/home.component';
import { UsersListComponent } from './users/users-list/users-list.component';
import { NotFoundComponent } from './not-found/not-found.component'
import { NgxSpinnerModule } from 'ngx-spinner';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { FooterComponent } from './footer/footer.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { ConfirmDailogComponent } from './confirm-dailog/confirm-dailog.component';
import { NewUserComponent } from './users/new-user/new-user.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { HandleNewsComponent } from './news/handle-news/handle-news.component';
import { QuillModule } from 'ngx-quill';
import { DataTablesModule } from 'angular-datatables';
import { NewsDetailsModalComponent } from './modals/news-details-modal/news-details-modal.component';
import { AdsListComponent } from './ads/ads-list/ads-list.component';
import { HandleAdsComponent } from './ads/handle-ads/handle-ads.component';
import { DatePipe } from '@angular/common';
import { HandleServiceComponent } from './AmanaServices/handle-service/handle-service.component';
import { ServicesListComponent } from './AmanaServices/services-list/services-list.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { HandleMobComponent } from './mobs/handle-mob/handle-mob.component';
import { MobListComponent } from './mobs/mob-list/mob-list.component';
import { SafeHtmlPipe } from './_pipes/safehtml';
import { HandleVideoComponent } from './video/handle-video/handle-video.component';
import { VideoListComponent } from './video/video-list/video-list.component';
import { HandleBaladyaComponent } from './baladyat/handle-baladya/handle-baladya.component';
import { BaladyatListComponent } from './baladyat/baladyat-list/baladyat-list.component';
import { HandleProjectComponent } from './project/handle-project/handle-project.component';
import { ProjectListComponent } from './project/project-list/project-list.component';
import { CreateInfoModalComponent } from './modals/create-info-modal/create-info-modal.component';
import { InfoComponent } from './info/info.component';
import { DocComponent } from './doc/doc.component';
import { CreateDocModalComponent } from './modals/create-doc-modal/create-doc-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HasRoleDirective,
    HomeComponent,
    UsersListComponent,
    NotFoundComponent,
    FooterComponent,
    RolesModalComponent,
    UserEditComponent,
    ConfirmDailogComponent,
    NewUserComponent,
    TextInputComponent,
    DateInputComponent,
    NewsListComponent,
    HandleNewsComponent,
    NewsDetailsModalComponent,
    AdsListComponent,
    HandleAdsComponent,
    HandleServiceComponent,
    ServicesListComponent,
    ChangePasswordComponent,
    HandleMobComponent,
    MobListComponent,
    SafeHtmlPipe,
    HandleVideoComponent,
    VideoListComponent,
    HandleBaladyaComponent,
    BaladyatListComponent,
    HandleProjectComponent,
    ProjectListComponent,
    InfoComponent,
    CreateInfoModalComponent,
    DocComponent,
    CreateDocModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    BsDropdownModule.forRoot(),
    FormsModule,
    CollapseModule.forRoot(),
    NgxSpinnerModule,
    ToastrModule.forRoot({
      positionClass: "toast-bottom-right"
    }),
    ModalModule.forRoot(),
    ReactiveFormsModule,
    QuillModule.forRoot(),
    DataTablesModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    DatePipe,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
