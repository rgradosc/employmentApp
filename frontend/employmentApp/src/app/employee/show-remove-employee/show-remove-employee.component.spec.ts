import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowRemoveEmployeeComponent } from './show-remove-employee.component';

describe('ShowRemoveEmployeeComponent', () => {
  let component: ShowRemoveEmployeeComponent;
  let fixture: ComponentFixture<ShowRemoveEmployeeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowRemoveEmployeeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowRemoveEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
