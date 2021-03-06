import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {
  MatToolbarModule,
  MatIconModule,
  MatButtonModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatProgressSpinnerModule,
  MatSidenavModule,
  MatListModule,
  MatTableModule,
  MatSortModule,
  MatDialogModule,
  MatProgressBarModule
} from '@angular/material';

import {LayoutModule} from '@angular/cdk/layout';

import {AppComponent} from './app.component';
import {AuthService, CategoryService} from "./services";

import {PublicComponent, SecuredComponent} from "./components/layout";
import {AuthInterceptor, NoCacheInterceptor} from "./interceptors";
import {
  LoginComponent,
  CategoriesComponent,
  ConfirmDialogComponent,
  CreateDialogComponent,
  EditDialogComponent
} from "./components";
import {AuthGuard} from "./guards/auth.guard";

@NgModule({
  declarations: [
    AppComponent,
    PublicComponent,
    SecuredComponent,
    LoginComponent,
    CategoriesComponent,
    ConfirmDialogComponent,
    CreateDialogComponent,
    EditDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      {path: '', redirectTo: '/categories', pathMatch: 'full'},
      {path: 'login', component: LoginComponent},
      {path: 'categories', canActivate: [AuthGuard], component: CategoriesComponent},
    ]),

    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatListModule,
    MatTableModule,
    MatSortModule,
    MatDialogModule,
    MatProgressBarModule,

    LayoutModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: NoCacheInterceptor, multi: true},
    AuthService,
    CategoryService,
    AuthGuard
  ],
  entryComponents: [ConfirmDialogComponent, CreateDialogComponent, EditDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule {
}
