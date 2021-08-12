import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Project } from "../_models/project";
import { ProjectService } from "../_services/project.service";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class ProjectResolver implements Resolve<Project>{
    constructor(private ProjectService: ProjectService) {
    }
    resolve(route: ActivatedRouteSnapshot): Observable<Project> {
        return this.ProjectService.getProjectById(Number(route.paramMap.get('id')));
    }

}