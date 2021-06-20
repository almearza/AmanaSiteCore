import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AmanaService, ServiceType } from 'src/app/_models/amana-service';
import { AmanaServiceService } from 'src/app/_services/amana-service.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-handle-service',
  templateUrl: './handle-service.component.html',
  styleUrls: ['./handle-service.component.css']
})
export class HandleServiceComponent implements OnInit {
  servicesTypes: ServiceType[];
  _service: AmanaService;
  baseUrl = environment.baseUrl;
  serviceForm: FormGroup;
  validationErrors: string[] = [];
  formData: FormData = new FormData();
  imgUrl:any;


  @ViewChild("serviceForm") _serviceForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._serviceForm?.dirty) $event.returnValue = true;
  }
  
  constructor(
    private amanaService: AmanaServiceService,
    private fb: FormBuilder,
    private router: Router,
    private activatedRouter: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getTypes()
    this.activatedRouter.data.subscribe(data => {
      this._service = data._service;
      this.iniailizeForm();
      this.imgUrl=this.baseUrl.replace('api/','')+'images/services/'+this._service?.imgUrl;
    });
  }
  iniailizeForm() {
    this.serviceForm = this.fb.group({
      descr: [this._service?.descr || '', Validators.required],
      link: [this._service?.link || '', Validators.required],
      serviceImg: [''],
      typeId:[this._service?.typeId||'1', Validators.required],
      langCode: [this._service?.langCode.toString() || '1', Validators.required],
    });
   


    if (this._service == null || this._service?.id == 0) this.serviceForm.get('serviceImg').setValidators(Validators.required);
  }

  handle() {
    this.formData.append("id", this._service?.id.toString() || "0");
    this.formData.append("descr", this.serviceForm.get('descr').value);
    this.formData.append("link", this.serviceForm.get('link').value);
    this.formData.append("typeId", this.serviceForm.get('typeId').value);
    this.formData.append("langCode", this.serviceForm.get('langCode').value);



    this.amanaService.handleService(this.formData).subscribe(() => {
      this.serviceForm.reset();
      this.router.navigateByUrl('/services');
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
    }
    const reader = new FileReader();
    reader.readAsDataURL( files[0]);
    reader.onload = (_event) => { 
      this.imgUrl = reader.result; 
    }
  }
  getTypes() {
    this.amanaService.getServicesTypes().subscribe(_servicesTypes => {
      this.servicesTypes = _servicesTypes;
    });
  }
}
