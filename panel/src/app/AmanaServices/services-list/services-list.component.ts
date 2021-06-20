import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { AmanaService } from 'src/app/_models/amana-service';
import { AmanaServiceService } from 'src/app/_services/amana-service.service';
@Component({
  selector: 'app-services-list',
  templateUrl: './services-list.component.html',
  styleUrls: ['./services-list.component.css']
})
export class ServicesListComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  amanaservices: AmanaService[] = [];
  baseUrl = environment.baseUrl;
  imgUrl = environment.baseUrl.replace('api/', '') + 'images/services/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private amanaService: AmanaServiceService,
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
  lockads(aService: AmanaService) {
    this.amanaService.active(aService.id).subscribe(() => {
      const msg = aService.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' الخدمة بنجاح');
      aService.active = !aService.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'amanaservices/get-pagged-services',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.amanaservices = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'descr' }, { data: 'link' }, { data: 'imgUrl' },
    { data: 'uploadedDate' }, { data: 'uploadedBy' }, { data: 'active' }, { data: 'lang' }]
  }
}
