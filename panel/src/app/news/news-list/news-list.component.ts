import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { DtOptions } from 'src/app/_models/generalconfig';
import { News } from 'src/app/_models/news';
import { NewsService } from 'src/app/_services/news.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  news: News[] = [];
  baseUrl = environment.baseUrl;
  imgUrl =//"https://amana-md.gov.sa/images/news/";//
    environment.baseUrl.replace('api/', '') + 'images/news/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private newsService: NewsService,
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
  lockNews(news: News) {
    this.newsService.active(news.id).subscribe(() => {
      const msg = news.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' الخبر بنجاح');
      news.active = !news.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'news/get-pagged-news',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.news = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'title' }, { data: 'descr' }, { data: 'imgUrl' }, { data: 'typeName' }, { data: 'newsDate' },
    { data: 'uploadedBy' }, { data: 'active' }, { data: 'newsResource' }, { data: 'lang' }]
  }
}
