import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Mob } from "../_models/mob";
import { MobService } from "../_services/mob.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class MobResolver implements Resolve<Mob>{
    constructor(private _service:MobService) {
    }
    resolve(route: ActivatedRouteSnapshot):Observable<Mob>{
        return this._service.getmobById(Number(route.paramMap.get('id')));
    }

}