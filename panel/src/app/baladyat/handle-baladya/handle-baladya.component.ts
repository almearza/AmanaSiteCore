import { DatePipe } from '@angular/common';
import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Baladya } from 'src/app/_models/baladya';
import { QuillConfiguration } from 'src/app/_models/generalconfig';
import { BaladyatService } from 'src/app/_services/baladyat.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-handle-baladya',
  templateUrl: './handle-baladya.component.html',
  styleUrls: ['./handle-baladya.component.css']
})
export class HandleBaladyaComponent implements OnInit {

  baladya: Baladya;
  baseUrl = environment.baseUrl;
  baladyaForm: FormGroup;
  validationErrors: string[] = [];

  quillConfiguration = QuillConfiguration;
  minDate: Date;
  hasExpireDate = false;
  imgUrl:any;

  @ViewChild("baladyaForm") _baladyaForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._baladyaForm?.dirty) $event.returnValue = true;
  }

  constructor(
    private fb: FormBuilder,
    private baladyatService: BaladyatService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private datePipe: DatePipe
  ) {
  }

  ngOnInit(): void {
    this.activatedRouter.data.subscribe(data => {
      this.baladya = data.baladya;
      this.iniailizeForm();
    });
  }
  iniailizeForm() {
   
    this.baladyaForm = this.fb.group({
      name: [this.baladya?.name || '', Validators.required],
      descr: [this.baladya?.descr || '', Validators.required],
      location: [this.baladya?.location || '', Validators.required],
      email: [this.baladya?.email || '', Validators.required],
      phoneNumber: [this.baladya?.phoneNumber || '', Validators.required],
      baladyaType: [this.baladya?.baladyaType || '1'],
      langCode: [this.baladya?.langCode.toString() || '1'],
    });
   
  }

  handle() {
    let formData = new FormData();
    formData.append("id", this.baladya?.id.toString() || "0");
    formData.append("name", this.baladyaForm.get('name').value);
    formData.append("descr", this.baladyaForm.get('descr').value);
    formData.append("email", this.baladyaForm.get('email').value);
    formData.append("phoneNumber", this.baladyaForm.get('phoneNumber').value);
    formData.append("location", this.baladyaForm.get('location').value);
    formData.append("baladyaType", this.baladyaForm.get('baladyaType').value);
    formData.append("langCode", this.baladyaForm.get('langCode').value);

    

    this.baladyatService.handleBaladya(formData).subscribe(() => {
      this.baladyaForm.reset();
      this.router.navigateByUrl('/baladyat');
    }, error => {
      this.validationErrors = error;
    });
  }
}
