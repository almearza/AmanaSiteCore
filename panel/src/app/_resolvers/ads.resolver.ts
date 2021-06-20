import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Ads } from "../_models/Ads";
import { AdsService } from "../_services/ads.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class AdsResolver implements Resolve<Ads>{
    constructor(private adsService:AdsService) {
    }
    resolve(route: ActivatedRouteSnapshot):Observable<Ads>{
        return this.adsService.getAdsById(Number(route.paramMap.get('id')));
    }

}