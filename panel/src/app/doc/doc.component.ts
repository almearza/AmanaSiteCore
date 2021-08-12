import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { DtOptions } from 'src/app/_models/generalconfig';
import { environment } from 'src/environments/environment';
import { DataTablesResponse } from 'src/app/_models/datatable';
import { Doc } from 'src/app/_models/doc';
import { DocService } from 'src/app/_services/doc.service';
import { ConfirmService } from '../_services/confirm.service';
import { CreateDocModalComponent } from '../modals/create-doc-modal/create-doc-modal.component';

@Component({
  selector: 'app-doc',
  templateUrl: './doc.component.html',
  styleUrls: ['./doc.component.css']
})
export class DocComponent implements OnInit {


  dtOptions: DataTables.Settings = {};
  docs: Doc[] = [];
  baseUrl = environment.baseUrl;
  docLink = environment.baseUrl.replace('api/', '') + 'images/docs/';
  bsModalRef: BsModalRef;

  constructor(private http: HttpClient,
    private modalService: BsModalService,
    private docService: DocService,
    private toastr: ToastrService,
    private confirmService: ConfirmService) { }

  ngOnInit(): void {
    this.configDataTable();
  }

  addDoc() {
    const config = {
      class: "modal-dailog-centered"
    };
    this.bsModalRef = this.modalService.show(CreateDocModalComponent, config);

    this.bsModalRef.content.addDocEvent.subscribe(values => {
      this.docService.addDoc(values).subscribe(docResponse => {
        this.docs.push(docResponse);
      })
    });
  }

  deleteDoc(doc: Doc) {
    this.confirmService.confirm("حذف دليل", "هل تريد حذف هذا الدليل؟").subscribe(result => {
      if (result) {
        this.docService.deletedoc(doc.id).subscribe(() => {
          this.toastr.success('تم حذف الدليل بنجاح');
          this.docs = this.docs.filter(i => i.id != doc.id);
        });
      }
    });
  }

  configDataTable() {
    this.dtOptions = DtOptions;
    this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
      this.http
        .post<DataTablesResponse>(
          this.baseUrl + 'docs/get-pagged-docs',
          dataTablesParameters, {}
        ).subscribe(resp => {
          this.docs = resp.data;
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: []
          });
        });
    }
    this.dtOptions.columns = [{ data: 'id' }, { data: 'name' }, { data: 'link' }, { data: 'modifiedDate' }, { data: 'doneBy' }, { data: 'langCode' }]
  }
}
