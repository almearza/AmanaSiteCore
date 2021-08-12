import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { Info } from 'src/app/_models/info';
import { environment } from 'src/environments/environment';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormBuilder } from '@angular/forms';
import { InfoService } from 'src/app/_services/info.service';
import { EventEmitter } from '@angular/core';


@Component({
  selector: 'app-create-info-modal',
  templateUrl: './create-info-modal.component.html',
  styleUrls: ['./create-info-modal.component.css']
})
export class CreateInfoModalComponent implements OnInit {
  info: Info;
  baseUrl = environment.baseUrl;
  infoForm: FormGroup;
  validationErrors: string[] = [];
  formData: any;
  @Input() addInfoEvent = new EventEmitter();
  imgUrl:any;

  @ViewChild("infoForm") _infoForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._infoForm?.dirty) $event.returnValue = true;
  }
  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private infoService: InfoService) { }

  ngOnInit(): void {
    this.imgUrl=this.baseUrl.replace('api/','')+'images/info/'+this.info?.imgUrl;
    this.formData = new FormData();
    this.iniailizeForm();
  }
  iniailizeForm() {
    this.infoForm = this.fb.group({
      infoImg: ['', Validators.required],
      langCode: [this.info?.langCode.toString() || '1'],
    });
  }

  addInfo() {
    this.formData.append("langCode", this.infoForm.get('langCode').value);
    this.addInfoEvent.emit(this.formData);
    this.infoForm.reset();
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
      const reader = new FileReader();
      reader.readAsDataURL( files[i]);
      reader.onload = (_event) => { 
        this.imgUrl = reader.result; 
      }
    }

  }
}
