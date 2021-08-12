import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { NewsDetailsModalComponent } from 'src/app/modals/news-details-modal/news-details-modal.component';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { BaladyatService } from 'src/app/_services/baladyat.service';
import { Baladya } from 'src/app/_models/baladya';


@Component({
  selector: 'app-baladyat-list',
  templateUrl: './baladyat-list.component.html',
  styleUrls: ['./baladyat-list.component.css']
})
export class BaladyatListComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  baladyat: Baladya[] = [];
  baseUrl = environment.baseUrl;
  imgUrl = environment.baseUrl.replace('api/', '') + 'images/baladya/';

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private baladyaService: BaladyatService,
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

  lockbaladya(baladya: Baladya) {
    this.baladyaService.active(baladya.id).subscribe(() => {
      const msg = baladya.active ? 'تعطيل' : 'تفعيل';
      this.toastr.success('تم ' + msg + ' البلدية بنجاح');
      baladya.active = !baladya.active;
    })
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'baladyat/get-pagged-Baladyat',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.baladyat = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'name' }, { data: 'descr' }, { data: 'email' }, { data: 'phoneNumber' }, { data: 'location' }
      ,{ data: 'modifiedDate' },{ data: 'baladyaType' }, { data: 'doneBy' }, { data: 'active' }, { data: 'langCode' }]
  }
 
}
