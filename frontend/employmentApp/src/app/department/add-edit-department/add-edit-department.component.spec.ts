import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditDepartmentComponent } from './add-edit-department.component';

describe('AddEditDepartmentComponent', () => {
  let component: AddEditDepartmentComponent;
  let fixture: ComponentFixture<AddEditDepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEditDepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
