import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-department',
  templateUrl: './add-edit-department.component.html',
  styleUrls: ['./add-edit-department.component.css']
})
export class AddEditDepartmentComponent implements OnInit {

  constructor(private service: SharedService) { }

  @Input() department: any;
  id: number;
  name: string;

  ngOnInit(): void {
    this.id = this.department.id;
    this.name = this.department.name;
  }

  addDepartment() {
    const model = {
      id: this.id,
      name: this.name
    };

    this.service.addDepartment(model).subscribe(response => {
      alert(response.messageOk.description);
    });
  }

  updateDepartment() {
    const model = {
      id: this.id,
      name: this.name
    };

    this.service.updateDepartment(model).subscribe(response => {
      alert(response.messageOk.description);
    });
  }
}
