import { DatePipe } from '@angular/common';
import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Video } from 'src/app/_models/video';
import { QuillConfiguration } from 'src/app/_models/generalconfig';
import { VideoService} from 'src/app/_services/video.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-handle-video',
  templateUrl: './handle-video.component.html',
  styleUrls: ['./handle-video.component.css']
})
export class HandleVideoComponent implements OnInit {

  video: Video;
  baseUrl = environment.baseUrl;
  videoForm: FormGroup;
  validationErrors: string[] = [];
  formData: FormData = new FormData();
  quillConfiguration = QuillConfiguration;
  minDate: Date;
  hasExpireDate = false;
  imgUrl:any;

  @ViewChild("videoForm") _videoForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._videoForm?.dirty) $event.returnValue = true;
  }

  constructor(
    private fb: FormBuilder,
    private videoService: VideoService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private datePipe: DatePipe
  ) {
  }

  ngOnInit(): void {
    this.activatedRouter.data.subscribe(data => {
      this.video = data.video;
      this.iniailizeForm();
      this.imgUrl=this.baseUrl.replace('api/','')+'images/videos/'+this.video?.imgUrl;
    });
  }
  iniailizeForm() {
    this.videoForm = this.fb.group({
      title: [this.video?.title || '', Validators.required],
      descr: [this.video?.descr || '', Validators.required],
      link: [this.video?.link || '', Validators.required],
      videoImg: [''],
      langCode: [this.video?.langCode.toString() || '1'],
    });
    
    if (this.video == null || this.video?.id == 0) this.videoForm.get('videoImg').setValidators(Validators.required);
  }

  handle() {
    this.formData.append("id", this.video?.id.toString() || "0");
    this.formData.append("title", this.videoForm.get('title').value);
    this.formData.append("descr", this.videoForm.get('descr').value);
    this.formData.append("link", this.videoForm.get('link').value);
    this.formData.append("langCode", this.videoForm.get('langCode').value);

    this.videoService.handlevideo(this.formData).subscribe(() => {
      this.videoForm.reset();
      this.router.navigateByUrl('/video');
    }, error => {
      console.log(error);
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

}
