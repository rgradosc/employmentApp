import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { routing, appRoutingProviders } from './app.routing';
import { DepartmentComponent } from './department/department.component';
import { ShowRemoveDepartmentComponent } from './department/show-remove-department/show-remove-department.component';
import { AddEditDepartmentComponent } from './department/add-edit-department/add-edit-department.component';
import { EmployeeComponent } from './employee/employee.component';
import { ShowRemoveEmployeeComponent } from './employee/show-remove-employee/show-remove-employee.component';
import { AddEditEmployeeComponent } from './employee/add-edit-employee/add-edit-employee.component';
import { SharedService } from './shared.service';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    DepartmentComponent,
    ShowRemoveDepartmentComponent,
    AddEditDepartmentComponent,
    EmployeeComponent,
    ShowRemoveEmployeeComponent,
    AddEditEmployeeComponent
  ],
  imports: [
    BrowserModule,
    routing,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    appRoutingProviders,
    SharedService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
