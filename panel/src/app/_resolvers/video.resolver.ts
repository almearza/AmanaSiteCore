import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Video } from "../_models/video";
import { VideoService } from "../_services/video.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class VideoResolver implements Resolve<Video>{
    constructor(private videoService:VideoService) {
    }
    resolve(route: ActivatedRouteSnapshot):Observable<Video>{
        return this.videoService.getvideoById(Number(route.paramMap.get('id')));
    }

}