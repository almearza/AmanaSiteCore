import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AmanaService, ServiceType } from '../_models/amana-service';

@Injectable({
  providedIn: 'root'
})
export class AmanaServiceService {

  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  handleService(news:any){
   return this.http.post<AmanaService>(this.baseUrl+'amanaservices/handle-service',news)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'amanaservices/activate-service/'+id,{});
  }
  getServiceById(id:number){
    return this.http.get<AmanaService>(this.baseUrl+'amanaservices/get-service/'+id);
  }
  getServicesTypes(){
   return this.http.get<ServiceType[]>(this.baseUrl+'amanaservices/get-types')
  }
}
