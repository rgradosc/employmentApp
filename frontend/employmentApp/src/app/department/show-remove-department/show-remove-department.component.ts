import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-remove-department',
  templateUrl: './show-remove-department.component.html',
  styleUrls: ['./show-remove-department.component.css']
})
export class ShowRemoveDepartmentComponent implements OnInit {

  constructor(private service: SharedService) { }

  departmentList: any = [];
  modalTitle: string;
  activateAddEditComponent: boolean;
  department: any;

  ngOnInit(): void {
    this.wireUpList();
  }

  deleteClick(department) {
    if (confirm('Are your sure?')) {
      this.service.deleteDeparment(department.Id).subscribe(
        response => {
          alert(response.messageOk.description);
        });
    }
  }

  updateClick(department) {
    this.department = department;
    this.modalTitle = 'Update department';
    this.activateAddEditComponent = true;
  }

  closeClick() {
    this.activateAddEditComponent = false;
    this.wireUpList();
  }

  addClick() {
    this.department = {
      Id: 0,
      Name: ''
    };

    this.modalTitle = 'Add new department';
    this.activateAddEditComponent = true;
  }

  wireUpList() {
    this.service.getDepartments().subscribe(
      response => {
        this.departmentList = response.data;
      });
  }
}
