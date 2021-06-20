import { DatePipe } from '@angular/common';
import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ads } from 'src/app/_models/ads';
import { QuillConfiguration } from 'src/app/_models/generalconfig';
import { AdsService } from 'src/app/_services/ads.service';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-handle-ads',
  templateUrl: './handle-ads.component.html',
  styleUrls: ['./handle-ads.component.css']
})
export class HandleAdsComponent implements OnInit {

  ads: Ads;
  baseUrl = environment.baseUrl;
  adsForm: FormGroup;
  validationErrors: string[] = [];
  formData: FormData = new FormData();
  quillConfiguration = QuillConfiguration;
  minDate: Date;
  hasExpireDate = false;
  imgUrl:any;

  @ViewChild("adsForm") _adsForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._adsForm?.dirty) $event.returnValue = true;
  }

  constructor(
    private fb: FormBuilder,
    private adsService: AdsService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private datePipe: DatePipe
  ) {
  }

  ngOnInit(): void {
    this.activatedRouter.data.subscribe(data => {
      this.ads = data.ads;
      this.iniailizeForm();
      this.imgUrl=this.baseUrl.replace('api/','')+'images/ads/'+this.ads?.imgUrl;
    });
  }
  iniailizeForm() {
    let expireDate=this.datePipe.transform(this.ads?.expireDate ,'yyyy-MM-dd') || '';
   
    this.adsForm = this.fb.group({
      title: [this.ads?.title || '', Validators.required],
      descr: [this.ads?.descr || '', Validators.required],
      canExpire: [this.ads?.canExpire || false, Validators.required],
      expireDate: ['', Validators.required],
      link: [this.ads?.link || ''],
      adsImg: [''],
      langCode: [this.ads?.langCode.toString() || '1'],
    });
    this.minDate = new Date();
    this.minDate.setHours(this.minDate.getHours() + 24);
    
    
    
    if (this.ads == null || this.ads?.id == 0) this.adsForm.get('adsImg').setValidators(Validators.required);

    this.adsForm.get('canExpire').valueChanges.subscribe(value => this.handleChange(value));
    if(!this.ads?.canExpire){
      this.adsForm.get('expireDate').disable()
      this.adsForm.get('expireDate').setValidators(null)
    }else{
      this.adsForm.get('expireDate').setValue(expireDate);
    }
  }

  handle() {
    let expireDate = this.adsForm.get('expireDate').value;
    this.formData.append("id", this.ads?.id.toString() || "0");
    this.formData.append("title", this.adsForm.get('title').value);
    this.formData.append("descr", this.adsForm.get('descr').value);
    this.formData.append("canExpire", this.adsForm.get('canExpire').value);
    this.formData.append("expireDate", this.datePipe.transform(expireDate,'yyyy-MM-dd')||'');
    this.formData.append("link", this.adsForm.get('link').value);
    this.formData.append("langCode", this.adsForm.get('langCode').value);

    

    this.adsService.handleAds(this.formData).subscribe(() => {
      this.adsForm.reset();
      this.router.navigateByUrl('/ads');
    }, error => {
      this.validationErrors = error;
    });
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
  handleChange(event: boolean) {
    if (event == true) {
      this.adsForm.get('expireDate').enable()
      this.adsForm.get('expireDate').setValidators(Validators.required)
    }
    if (event == false) {
      this.adsForm.get('expireDate').disable()
      this.adsForm.get('expireDate').setValidators(null)
    }
  }
}
