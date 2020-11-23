import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-remove-employee',
  templateUrl: './show-remove-employee.component.html',
  styleUrls: ['./show-remove-employee.component.css']
})
export class ShowRemoveEmployeeComponent implements OnInit {

  constructor(private service: SharedService) { }

  employeeList: any = [];
  modalTitle: string;
  activateAddEditComponent: boolean;
  employee: any;

  ngOnInit(): void {
    this.wireUpList();
  }

  deleteClick(employee) {
    if (confirm('Are your sure?')) {
      this.service.deleteEmployee(employee.id).subscribe(
        response => {
          alert(response.messageOk.description);
        });
    }
  }

  updateClick(employee) {
    this.employee = employee;
    this.modalTitle = 'Update employee';
    this.activateAddEditComponent = true;
  }

  closeClick() {
    this.activateAddEditComponent = false;
    this.wireUpList();
  }

  addClick() {
    this.employee = {
      id: 0,
      name: ''
    };

    this.modalTitle = 'Add new employee';
    this.activateAddEditComponent = true;
  }

  wireUpList() {
    this.service.getEmployees().subscribe(
      response => {
        this.employeeList = response.data;
      });
  }
}
