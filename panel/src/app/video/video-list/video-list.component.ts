import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { Video } from 'src/app/_models/video';
import { VideoService } from 'src/app/_services/video.service';
@Component({
  selector: 'app-video-list',
  templateUrl: './video-list.component.html',
  styleUrls: ['./video-list.component.css']
})
export class VideoListComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  video: Video[] = [];
  baseUrl = environment.baseUrl;
  imgUrl =//"https://amana-md.gov.sa/images/video/";//
    environment.baseUrl.replace('api/', '') + 'images/video/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private videoService: VideoService,
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
  lockvideo(video: Video) {
    this.videoService.active(video.id).subscribe(() => {
      const msg = video.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' الاعلان بنجاح');
      video.active = !video.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'video/get-pagged-videos',
          dataTablesParameters, {}
        ).subscribe(resp => {
          console.log(resp.data);
          
          this.video = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'title' }, { data: 'descr' },{data:'link'}
    , { data: 'imgUrl' }, { data: 'uploadDate' },{ data: 'uploadedBy' }, { data: 'active' }, { data: 'lang' }]
  }

}
