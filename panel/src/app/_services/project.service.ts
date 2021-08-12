import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Project } from '../_models/project';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  handleproject(project:any){
   return this.http.post<Project>(this.baseUrl+'project/handle-project',project)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'project/activate-project/'+id,{});
  }
  getProjectById(id:number){
    return this.http.get<Project>(this.baseUrl+'project/get-project/'+id);
  }
}