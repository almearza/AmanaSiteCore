import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Video } from '../_models/video';
@Injectable({
  providedIn: 'root'
})
export class VideoService {
  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  handlevideo(video:any){
   return this.http.post<Video>(this.baseUrl+'video/handle-video',video)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'video/activate-video/'+id,{});
  }
  getvideoById(id:number){
    return this.http.get<Video>(this.baseUrl+'video/get-video/'+id);
  }
}
