import { DatePipe } from '@angular/common';
import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuillConfiguration } from 'src/app/_models/generalconfig';
import { Mob, MobType } from 'src/app/_models/mob';
import { MobService } from 'src/app/_services/mob.service';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-handle-mob',
  templateUrl: './handle-mob.component.html',
  styleUrls: ['./handle-mob.component.css']
})
export class HandleMobComponent implements OnInit {
  mob: Mob;
  baseUrl = environment.baseUrl;
  imgUrl:any;
  mobForm: FormGroup;
  validationErrors: string[] = [];
  formData: FormData = new FormData();
  mobTypes:MobType[];
  quillConfiguration = QuillConfiguration;

  @ViewChild("mobForm") _mobForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._mobForm?.dirty) $event.returnValue = true;
  }

  constructor(
    private fb: FormBuilder,
    private mobService: MobService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private datePipe: DatePipe
  ) {
  }

  ngOnInit(): void {
    this.activatedRouter.data.subscribe(data => {
      this.mob = data.mob;
      this.iniailizeForm();
      this.imgUrl=this.baseUrl.replace('api/','')+'images/mob/'+this.mob?.imgUrl;
    });
  }
  iniailizeForm() {
    this.getTypes();
    this.mobForm = this.fb.group({
      title: [this.mob?.title || '', Validators.required],
      descr: [this.mob?.descr || '', Validators.required],
      link: [this.mob?.link || '', Validators.required],
      mobImg: [''],
      langCode: [this.mob?.langCode.toString() || '1'],
      typeId:[this.mob?.typeId||'1', Validators.required],
    });
    
    
    if (this.mob == null || this.mob?.id == 0) this.mobForm.get('mobImg').setValidators(Validators.required);
   
  }

  handle() {
    this.formData.append("id", this.mob?.id.toString() || "0");
    this.formData.append("title", this.mobForm.get('title').value);
    this.formData.append("descr", this.mobForm.get('descr').value);
    this.formData.append("typeId", this.mobForm.get('typeId').value);
    this.formData.append("link", this.mobForm.get('link').value);
    this.formData.append("langCode", this.mobForm.get('langCode').value);

    

    this.mobService.handlemob(this.formData).subscribe(() => {
      this.mobForm.reset();
      this.router.navigateByUrl('/mobs');
    }, error => {
      this.validationErrors = error;
    });
  }

  setFiles(event) {
    let files = event.srcElement.files
    if (!files) {
      return
    }
    this.formData = new FormData();
    for (let i = 0; i < files.length; i++) {
      this.formData.append(i.toString(), files[i], files[i].name);
      const reader = new FileReader();
      reader.readAsDataURL( files[i]);
      reader.onload = (_event) => { 
        this.imgUrl = reader.result; 
      }
    }
  }
  getTypes() {
    this.mobService.getMobTypes().subscribe(_mobTypes => {
      this.mobTypes = _mobTypes;
    });
  }

}
