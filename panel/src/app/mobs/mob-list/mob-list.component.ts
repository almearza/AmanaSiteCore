import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { Mob } from 'src/app/_models/mob';
import { MobService } from 'src/app/_services/mob.service';

@Component({
  selector: 'app-mob-list',
  templateUrl: './mob-list.component.html',
  styleUrls: ['./mob-list.component.css']
})
export class MobListComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  mobs: Mob[] = [];
  baseUrl = environment.baseUrl;
  imgUrl = environment.baseUrl.replace('api/', '') + 'images/mob/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private mobService: MobService,
    private toastr: ToastrService) { }
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

  lockads(mob: Mob) {
    this.mobService.active(mob.id).subscribe(() => {
      const msg = mob.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' المبادرة بنجاح');
      mob.active = !mob.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'mob/get-pagged-mobs',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.mobs = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'descr' }, { data: 'link' }, { data: 'typeName' }, { data: 'imgUrl' },
    { data: 'mAndFDate' }, { data: 'uploadedBy' }, { data: 'active' }, { data: 'lang' }]
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
}
