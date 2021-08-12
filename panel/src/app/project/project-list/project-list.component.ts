import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { Project } from 'src/app/_models/project';
import { ProjectService } from 'src/app/_services/project.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  projects: Project[] = [];
  baseUrl = environment.baseUrl;
  imgUrl =environment.baseUrl.replace('api/', '') + 'images/projects/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private projectService: ProjectService,
    private toastr: ToastrService) { }
  ngOnInit(): void {
    this.configDataTable();
  }

  openModalForDescr(newsDetails: string) {
    const config = {
      class: "modal-dailog-centered",
      initialState: {
        newsDetails
      }
    };
    this.modalService.show(NewsDetailsModalComponent, config);
  }
  openModalForImg(imgUrl: string) {
    const config = {
      class: "modal-dailog-centered",
      initialState: {
        imgUrl
      }
    };
    this.modalService.show(NewsDetailsModalComponent, config);
  }
  lockproject(project: Project) {
    this.projectService.active(project.id).subscribe(() => {
      const msg = project.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' المشروع بنجاح');
      project.active = !project.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'project/get-pagged-projects',
          dataTablesParameters, {}
        ).subscribe(resp => { 
          this.projects = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'title' }, { data: 'descr' },{data:'intro'}
    , { data: 'imgUrl' }, { data: 'modifiedDate' },{ data: 'doneBy' }, { data: 'active' }, { data: 'langCode' }]
  }
}
