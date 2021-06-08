import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { News } from 'src/app/_models/news';
import { environment } from 'src/environments/environment';


class DataTablesResponse {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
}

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  news: News[] = [];
  imgUrl ="https://amana-md.gov.sa/images/news/";//environment.baseUrl + '/images/news/';

  // We use this trigger because fetching the list of news can be quite long,
  // thus we ensure the data is fetched before rendering
  // dtTrigger: Subject<any> = new Subject<any>();

  constructor(private http: HttpClient, private modalService: BsModalService) { }
  ngOnInit(): void {
    // this.dtOptions = {
    //   pagingType: 'full_numbers',
    //   pageLength: 2
    // };
    // this.httpClient.get<News[]>( 'https://localhost:5001/api/temp')
    //   .subscribe(data => {
    //     this.news = (data as any).data;
    //     // Calling the DT trigger to manually render the table
    //     this.dtTrigger.next();
    //   });
    const that = this;
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        that.http
          .post<DataTablesResponse>(
            'https://localhost:5001/api/temp',
            dataTablesParameters, {}
          ).subscribe(resp => {
            that.news = resp.data;

            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: []
            });
          });
      },
      columns: [{ data: 'id' }, { data: 'title' }, { data: 'descr' }, { data: 'imgUrl' }, { data: 'typeName' }, { data: 'newsDate' }
        , { data: 'uploadedBy' }, { data: 'active' }, { data: 'newsResource' }, { data: 'lang' }]
    };
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

  }
  // ngOnDestroy(): void {
  //   // Do not forget to unsubscribe the event
  //   this.dtTrigger.unsubscribe();
  // }
}
