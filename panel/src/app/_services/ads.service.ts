import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Ads } from '../_models/ads';

@Injectable({
  providedIn: 'root'
})
export class AdsService {
  baseUrl=environment.baseUrl;
  constructor(private http:HttpClient) { }
 
  handleAds(ads:any){
   return this.http.post<Ads>(this.baseUrl+'ads/handle-ads',ads)
  }
  active(id:number){
    return this.http.put(this.baseUrl+'ads/activate-ads/'+id,{});
  }
  getAdsById(id:number){
    return this.http.get<Ads>(this.baseUrl+'ads/get-ads/'+id);
  }
}