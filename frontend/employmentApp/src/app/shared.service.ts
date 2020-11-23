import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageResponse } from './response/message.response';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIURL = 'http://localhost/employmentApi/'; // 'http://localhost:3363/'
  readonly PHOTOURL = 'http://localhost/employmentApi/Content/Employee/'; // 'http://localhost:3363/Content/Employee/'

  constructor(private http: HttpClient) { }

  getDepartments(): Observable<MessageResponse> {
    return this.http.get<any>(`${this.APIURL}api/department`);
  }

  addDepartment(model: any): Observable<MessageResponse> {
    return this.http.post<any>(`${this.APIURL}api/department`, model);
  }

  updateDepartment(model: any): Observable<MessageResponse> {
    return this.http.put<any>(`${this.APIURL}api/department`, model);
  }

  deleteDeparment(id: any): Observable<MessageResponse> {
    return this.http.delete<any>(`${this.APIURL}api/department/${id}`);
  }

  getEmployees(): Observable<MessageResponse> {
    return this.http.get<any>(`${this.APIURL}api/employee`);
  }

  addEmployee(model: any): Observable<MessageResponse> {
    return this.http.post<any>(`${this.APIURL}api/employee`, model);
  }

  updateEmployee(model: any): Observable<MessageResponse> {
    return this.http.put<any>(`${this.APIURL}api/employee`, model);
  }

  deleteEmployee(id: any): Observable<MessageResponse> {
    return this.http.delete<any>(`${this.APIURL}api/employee/${id}`);
  }

  uploadPhoto(file: any): Observable<MessageResponse> {
    return this.http.post<any>(`${this.APIURL}api/employee/upload`, file);
  }
}
