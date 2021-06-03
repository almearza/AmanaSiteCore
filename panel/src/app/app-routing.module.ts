import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewUserComponent } from './new-user/new-user.component';
import { CreateNewsComponent } from './news/create-news/create-news.component';
import { EditNewsComponent } from './news/edit-news/edit-news.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UsersListComponent } from './users-list/users-list.component';
import { AdminGuard } from './_guards/admin.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { UserDetailedResolver } from './_resolvers/user-detailed.reslover';


const routes: Routes = [
  {path:'',component:HomeComponent},
  {
    path:'',
    runGuardsAndResolvers:'always',
    canActivate:[AdminGuard],
    children:[
      {path:'users',component:UsersListComponent},
      {path:'users/:username',component:UserEditComponent,canDeactivate:[PreventUnsavedChangesGuard],resolve:{user:UserDetailedResolver}},
      {path:'user/new',component:NewUserComponent,canDeactivate:[PreventUnsavedChangesGuard]},
      
      
      {path:'news',component:NewsListComponent},
      {path:'news/create',component:CreateNewsComponent},
      {path:'news/:newsId',component:EditNewsComponent},

      //member here represnt key of resolver 
      // {path:'admin',component:AdminPanelComponent,canActivate:[AdminGuard]},
    ]
  },
  {path:'not-found',component:NotFoundComponent},
  {path:'**',component:NotFoundComponent,pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
