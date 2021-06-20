import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { Ads} from 'src/app/_models/ads';
import { AdsService } from 'src/app/_services/ads.service';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';


@Component({
  selector: 'app-ads-list',
  templateUrl: './ads-list.component.html',
  styleUrls: ['./ads-list.component.css']
})
export class AdsListComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  ads: Ads[] = [];
  baseUrl = environment.baseUrl;
  imgUrl =//"https://amana-md.gov.sa/images/ads/";//
    environment.baseUrl.replace('api/', '') + 'images/ads/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private adsService: AdsService,
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
  lockads(ads: Ads) {
    this.adsService.active(ads.id).subscribe(() => {
      const msg = ads.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' الاعلان بنجاح');
      ads.active = !ads.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'ads/get-pagged-ads',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.ads = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'title' }, { data: 'descr' }, { data: 'canExpire' },{data:'expireDate'},{data:'link'}
    , { data: 'imgUrl' }, { data: 'uploadDate' },{ data: 'uploadedBy' }, { data: 'active' }, { data: 'lang' }]
  }
}
