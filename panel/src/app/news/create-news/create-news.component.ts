import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NewsTypes } from 'src/app/_models/news';
import { EditorConfig } from 'src/app/_models/txtconfig';
import { NewsService } from 'src/app/_services/news.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-create-news',
  templateUrl: './create-news.component.html',
  styleUrls: ['./create-news.component.css']
})
export class CreateNewsComponent implements OnInit {
  baseUrl = environment.baseUrl;
  htmlContent = '';
  newsTypes: NewsTypes[] = [];
  newsForm: FormGroup;
  validationErrors: string[] = [];
  @Input() control: FormControl;
  formData: FormData = new FormData();
  quillConfiguration = EditorConfig;
  constructor(
    private fb: FormBuilder,
    private newsService: NewsService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.iniailizeForm();
    this.getTypes();
    this.control = this.control ?? new FormControl()
  }
  iniailizeForm() {
    this.newsForm = this.fb.group({
      title: ['', Validators.required],
      descr: ['', Validators.required],
      newsImg: ['', Validators.required],
      newsResource: ['', Validators.required],
      typeId: ['0', Validators.required],
      langCode: ['1'],
    });
  }

  create() {
    this.formData.append("title", this.newsForm.get('title').value);
    this.formData.append("descr", this.newsForm.get('descr').value);
    this.formData.append("newsResource", this.newsForm.get('newsResource').value);
    this.formData.append("typeId", this.newsForm.get('typeId').value);
    this.formData.append("langCode", this.newsForm.get('langCode').value);

    this.newsService.createNews(this.formData).subscribe(user => {
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
  }
}


