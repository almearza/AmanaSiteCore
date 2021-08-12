import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { Info } from 'src/app/_models/info';
import { InfoService } from 'src/app/_services/info.service';
import { CreateInfoModalComponent } from '../modals/create-info-modal/create-info-modal.component';
import { ConfirmService } from '../_services/confirm.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  infos: Info[] = [];
  baseUrl = environment.baseUrl;
  imgUrl = environment.baseUrl.replace('api/', '') + 'images/info/';
  bsModalRef: BsModalRef;

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private infoService: InfoService,
    private toastr: ToastrService,
    private confirmService: ConfirmService) { }
  ngOnInit(): void {
    this.configDataTable();
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

  addInfo() {
    const config = {
      class: "modal-dailog-centered"
    };
    this.bsModalRef = this.modalService.show(CreateInfoModalComponent, config);

    this.bsModalRef.content.addInfoEvent.subscribe(values => {
      this.infoService.addInfo(values).subscribe(infoResponse => {
        this.infos.push(infoResponse);
      })
    });
  }

  deleteInfo(info: Info) {
    this.confirmService.confirm("حذف انفوجرافك", "هل تريد حذف هذا الانفوجرافك؟").subscribe(result => {
      if (result) {
        this.infoService.deleteInfo(info.id).subscribe(() => {
          this.toastr.success('تم حذف الانفوجرافك بنجاح');
          this.infos = this.infos.filter(i => i.id != info.id);
        });
      }
    });
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'info/get-pagged-info',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.infos = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'imgUrl' }, { data: 'modifiedDate' }, { data: 'doneBy' }, { data: 'langCode' }]
  }
}
