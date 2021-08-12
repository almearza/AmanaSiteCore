import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Info } from '../_models/info';

@Injectable({
  providedIn: 'root'
})
export class InfoService {
  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  addInfo(info:any){
   return this.http.post<Info>(this.baseUrl+'info/add-info',info)
  }
  deleteInfo(id:number){
    return this.http.delete(this.baseUrl+'info/delete-info/'+id,{});
  }
}
