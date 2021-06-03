import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NewsTypes } from '../_models/news';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
  getNewsTypes(){
    return this.http.get<NewsTypes[]>(this.baseUrl+'news/get-types');
  }
}
