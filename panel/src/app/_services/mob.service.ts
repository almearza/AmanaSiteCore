import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Mob, MobType } from '../_models/mob';

@Injectable({
  providedIn: 'root'
})
export class MobService {

  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  handlemob(mob:any){
   return this.http.post<Mob>(this.baseUrl+'mob/handle-mob',mob)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'mob/activate-mob/'+id,{});
  }
  getmobById(id:number){
    return this.http.get<Mob>(this.baseUrl+'mob/get-mob/'+id);
  }
  getMobTypes(){
    return this.http.get<MobType[]>(this.baseUrl+'mob/get-types');
  }
}
