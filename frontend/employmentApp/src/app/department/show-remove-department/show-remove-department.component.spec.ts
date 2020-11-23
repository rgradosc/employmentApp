import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowRemoveDepartmentComponent } from './show-remove-department.component';

describe('ShowRemoveDepartmentComponent', () => {
  let component: ShowRemoveDepartmentComponent;
  let fixture: ComponentFixture<ShowRemoveDepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowRemoveDepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowRemoveDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
