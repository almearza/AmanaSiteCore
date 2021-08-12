import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdsListComponent } from './ads/ads-list/ads-list.component';
import { HandleAdsComponent } from './ads/handle-ads/handle-ads.component';
import { HandleServiceComponent } from './AmanaServices/handle-service/handle-service.component';
import { ServicesListComponent } from './AmanaServices/services-list/services-list.component';
import { BaladyatListComponent } from './baladyat/baladyat-list/baladyat-list.component';
import { HandleBaladyaComponent } from './baladyat/handle-baladya/handle-baladya.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { HomeComponent } from './home/home.component';
import { HandleMobComponent } from './mobs/handle-mob/handle-mob.component';
import { MobListComponent } from './mobs/mob-list/mob-list.component';
import { NewUserComponent } from './users/new-user/new-user.component';
import { HandleNewsComponent } from './news/handle-news/handle-news.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { HandleProjectComponent } from './project/handle-project/handle-project.component';
import { ProjectListComponent } from './project/project-list/project-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UsersListComponent } from './users/users-list/users-list.component';
import { HandleVideoComponent } from './video/handle-video/handle-video.component';
import { VideoListComponent } from './video/video-list/video-list.component';
import { AdminGuard } from './_guards/admin.guard';
import { AdsGuard } from './_guards/ads.guard';
import { AuthGuard } from './_guards/auth.guard';
import { NewsGuard } from './_guards/news.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { AdsResolver } from './_resolvers/ads.resolver';
import { BaladyaResolver } from './_resolvers/baladya.resolver';
import { MobResolver } from './_resolvers/mob.resolver';
import { NewsResolver } from './_resolvers/news.resolver';
import { ProjectResolver } from './_resolvers/project.resolver';
import { UserDetailedResolver } from './_resolvers/user-detailed.reslover';
import { VideoResolver } from './_resolvers/video.resolver';
import { AmanaServicesResolver } from './_resolvers/_services.resolver';
import { InfoComponent } from './info/info.component';
import { DocComponent } from './doc/doc.component';


const routes: Routes = [
  { path: '', component: HomeComponent }, 
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AdminGuard],
    children: [
      { path: 'users', component: UsersListComponent },
      { path: 'users/:username', component: UserEditComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { user: UserDetailedResolver } },
      { path: 'user/new', component: NewUserComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      
      { path: 'services', component: ServicesListComponent },
      { path: 'services/create', component: HandleServiceComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'services/edit/:id', component: HandleServiceComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { _service: AmanaServicesResolver } },

      { path: 'mobs', component: MobListComponent },
      { path: 'mobs/create', component: HandleMobComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'mobs/edit/:id', component: HandleMobComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { mob: MobResolver } },

      { path: 'baladyat', component: BaladyatListComponent },
      { path: 'baladyat/create', component: HandleBaladyaComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'baladyat/edit/:id', component: HandleBaladyaComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { baladya: BaladyaResolver } },
      { path: 'docs', component: DocComponent },
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [NewsGuard],
    children: [
      { path: 'news', component: NewsListComponent },
      { path: 'news/create', component: HandleNewsComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'news/edit/:id', component: HandleNewsComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { news: NewsResolver } },

      { path: 'video', component: VideoListComponent },
      { path: 'video/create', component: HandleVideoComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'video/edit/:id', component: HandleVideoComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { video: VideoResolver } },

      { path: 'projects', component: ProjectListComponent },
      { path: 'projects/create', component: HandleProjectComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'projects/edit/:id', component: HandleProjectComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { project: ProjectResolver } },
     
      { path: 'info', component: InfoComponent },
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AdsGuard],
    children: [
      { path: 'ads', component: AdsListComponent },
      { path: 'ads/create', component: HandleAdsComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'ads/edit/:id', component: HandleAdsComponent, canDeactivate: [PreventUnsavedChangesGuard], resolve: { ads: AdsResolver } },
    ]
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'change-password', component: ChangePasswordComponent, canActivate: [AuthGuard] },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
