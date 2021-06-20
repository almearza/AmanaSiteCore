import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuillConfiguration } from 'src/app/_models/generalconfig';
import { News, NewsTypes } from 'src/app/_models/news';
import { NewsService } from 'src/app/_services/news.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-handle-news',
  templateUrl: './handle-news.component.html',
  styleUrls: ['./handle-news.component.css']
})
export class HandleNewsComponent implements OnInit {
  news: News;
  baseUrl = environment.baseUrl;
  newsTypes: NewsTypes[] = [];
  newsForm: FormGroup;
  validationErrors: string[] = [];
  formData: FormData = new FormData();
  quillConfiguration = QuillConfiguration;
  imgUrl:any;

  @ViewChild("newsForm") _newsForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._newsForm?.dirty) $event.returnValue = true;
  }

  constructor(
    private fb: FormBuilder,
    private newsService: NewsService,
    private router: Router,
    private activatedRouter: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.getTypes();
    this.activatedRouter.data.subscribe(data => {
      this.news = data.news;//user:key we used when we add resolver to route in approuting
      // console.log(this.user);
      this.iniailizeForm();
      this.imgUrl=this.baseUrl.replace('api/','')+'images/news/'+this.news?.imgUrl;
    });

  }
  iniailizeForm() {
    this.newsForm = this.fb.group({
      title: [this.news?.title || '', Validators.required],
      descr: [this.news?.descr || '', Validators.required],
      newsImg: [''],
      newsResource: [this.news?.newsResource || '', Validators.required],
      typeId: [this.news?.typeId || '1', Validators.required],
      langCode: [this.news?.langCode.toString() || '1'],
    });
    if (this.news==null ||this.news?.id == 0) this.newsForm.get('newsImg').setValidators(Validators.required);
  }

  handle() {
    this.formData.append("id", this.news?.id.toString()||"0");
    this.formData.append("title", this.newsForm.get('title').value);
    this.formData.append("descr", this.newsForm.get('descr').value);
    this.formData.append("newsResource", this.newsForm.get('newsResource').value);
    this.formData.append("typeId", this.newsForm.get('typeId').value);
    this.formData.append("langCode", this.newsForm.get('langCode').value);

    this.newsService.handleNews(this.formData).subscribe(() => {
      this.newsForm.reset();
      this.router.navigateByUrl('/news');
    }, error => {
      this.validationErrors = error;
    });
  }

  getTypes() {
    this.newsService.getNewsTypes().subscribe(newsTypes => {
      this.newsTypes = newsTypes;
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
}


