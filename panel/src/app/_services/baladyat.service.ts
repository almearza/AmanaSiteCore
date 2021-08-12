import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Baladya } from '../_models/baladya';

@Injectable({
  providedIn: 'root'
})
export class BaladyatService {

  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  handleBaladya(Baladya:any){
   return this.http.post<Baladya>(this.baseUrl+'Baladyat/handle-Baladyat',Baladya)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'Baladyat/activate-Baladyat/'+id,{});
  }
  getBaladyaById(id:number){
    return this.http.get<Baladya>(this.baseUrl+'Baladyat/get-Baladyat/'+id);
  }
}
