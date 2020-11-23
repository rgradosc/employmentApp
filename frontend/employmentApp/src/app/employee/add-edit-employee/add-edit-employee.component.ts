import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.css']
})
export class AddEditEmployeeComponent implements OnInit {

  constructor(private service: SharedService) { }

  @Input() employee: any;
  id: number;
  name: string;
  department: string;
  dateOfJoining: string;
  photoUrl: string;
  photoFileName: string;
  photoFilePath: string;
  departmentsList: any = [];

  ngOnInit(): void {
    this.photoUrl = this.service.PHOTOURL;
    this.photoFileName = 'anonymous.png';
    this.photoFilePath = `${this.photoUrl}${this.photoFileName}`;
    this.loadDepartments();
  }

  loadDepartments() {
    this.service.getDepartments().subscribe(
      response => {
        this.departmentsList = response.data;
      },
      error => {
        console.error(error);
      });
  }

  addEmployee() {
    const model = {
      id: this.id,
      name: this.name,
      department: this.department,
      dateOfJoining: this.dateOfJoining,
      photoFileName: this.photoFileName
    };

    this.service.addEmployee(model).subscribe(
      response => {
        alert(response.messageOk.description);
      },
      error => {
        console.error(error);
      });
  }

  updateEmployee() {
    const model = {
      id: this.id,
      name: this.name,
      department: this.department,
      dateOfJoining: this.dateOfJoining,
      photoFileName: this.photoFileName
    };

    this.service.updateEmployee(model).subscribe(
      response => {
        alert(response.messageOk.description);
      },
      error => {
        console.error(error);
      });
  }

  uploadPhoto(event) {
    const file = event.target.files[0];
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    this.service.uploadPhoto(formData).subscribe(
      response => {
        this.photoFileName = response.data;
        this.photoFilePath = `${this.photoUrl}${this.photoFileName}`;
      },
      error => {
        console.error(error);
      });
  }
}
