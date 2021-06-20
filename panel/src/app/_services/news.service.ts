import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { News, NewsTypes } from '../_models/news';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
  getNewsTypes(){
    return this.http.get<NewsTypes[]>(this.baseUrl+'news/get-types');
  }
  handleNews(news:any){
   return this.http.post<News>(this.baseUrl+'news/handle-news',news)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'news/activate-news/'+id,{});
  }
  getNewsById(id:number){
    return this.http.get<News>(this.baseUrl+'news/get-news/'+id);
  }
}
