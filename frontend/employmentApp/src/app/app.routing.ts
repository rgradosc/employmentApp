import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DepartmentComponent } from './department/department.component';
import { EmployeeComponent } from './employee/employee.component';

const appRoutes: Routes = [
    { path: 'department', component: DepartmentComponent },
    { path: 'employee', component: EmployeeComponent }
];

export const appRoutingProviders: any[] = [];
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
