import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Baladya } from "../_models/Baladya";
import { BaladyatService } from "../_services/baladyat.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class BaladyaResolver implements Resolve<Baladya>{
    constructor(private _service:BaladyatService) {
    }
    resolve(route: ActivatedRouteSnapshot):Observable<Baladya>{
        return this._service.getBaladyaById(Number(route.paramMap.get('id')));
    }

}