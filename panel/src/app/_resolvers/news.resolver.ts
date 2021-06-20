import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { News } from "../_models/news";
import { NewsService } from "../_services/news.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class NewsResolver implements Resolve<News>{
    constructor(private newsService:NewsService) {
    }
    resolve(route: ActivatedRouteSnapshot):Observable<News>{
        return this.newsService.getNewsById(Number(route.paramMap.get('id')));
    }

}