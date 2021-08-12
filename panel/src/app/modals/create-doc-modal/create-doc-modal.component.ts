import { Component, EventEmitter, HostListener, Input, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BsModalRef } from "ngx-bootstrap/modal";
import { Doc } from "src/app/_models/doc";
import { environment } from "src/environments/environment";


@Component({
  selector: 'app-create-doc-modal',
  templateUrl: './create-doc-modal.component.html',
  styleUrls: ['./create-doc-modal.component.css']
})
export class CreateDocModalComponent implements OnInit {
  doc: Doc;
  baseUrl = environment.baseUrl;
  docForm: FormGroup;
  validationErrors: string[] = [];
  formData: any;
  @Input() addDocEvent = new EventEmitter();

  @ViewChild("docForm") _docForm: FormGroup;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._docForm?.dirty) $event.returnValue = true;
  }
  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.formData = new FormData();
    this.iniailizeForm();
  }
  iniailizeForm() {
    this.docForm = this.fb.group({
      name:['',Validators.required],
      docFile: ['', Validators.required],
      langCode: [this.doc?.langCode.toString() || '1'],
    });
  }

  addDoc() {
    this.formData.append("langCode", this.docForm.get('langCode').value);
    this.addDocEvent.emit(this.formData);
    this.docForm.reset();
    this.formData = null;
    this.bsModalRef.hide();
  }

  setFiles(event) {
    let files = event.srcElement.files
    if (!files) {
      return
    }
    this.formData = new FormData();//reset formData
    for (let i = 0; i < files.length; i++) {
      this.formData.append(i.toString(), files[i], files[i].name);
    }

  }
}
