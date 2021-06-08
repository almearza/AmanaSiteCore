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
  createNews(news:any){
    return this.http.post<News>(this.baseUrl+'news/create-news',news);
  }
}
