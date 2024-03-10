import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { HomeComponent } from './components/home/home.component';
import { UserComponent } from './components/user/user.component';
import { EditComponent } from './components/edit/edit.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';

const routes: Routes = [
  { 
    path: "",
    redirectTo: "/signin",
    pathMatch: "full"
  },
  {
    path: "signin",
    component: SignInComponent
  },
  {
    path: "signup",
    component: SignUpComponent
  },
  {
    path: "post",
    component: HomeComponent
  },
  {
    path: "user/:username/post",
    component: UserComponent
  },
  {
    path: "user/:username",
    component: UserEditComponent
  },
  {
    path: "post/:id",
    component: EditComponent
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
