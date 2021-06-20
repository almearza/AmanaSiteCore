import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { AmanaService } from "../_models/amana-service";
import { AmanaServiceService } from "../_services/amana-service.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class AmanaServicesResolver implements Resolve<AmanaService>{
    constructor(private _service:AmanaServiceService) {
    }
    resolve(route: ActivatedRouteSnapshot):Observable<AmanaService>{
        return this._service.getServiceById(Number(route.paramMap.get('id')));
    }

}