import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
  @Input() control:FormControl;
  quillConfiguration=EditorConfig;
  constructor(
    private fb: FormBuilder,
    private newsService: NewsService
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
      typeId: [0, Validators.required],
      langCode: ['Ar'],
    });
  }

  create() {
    console.log(this.newsForm.value);
    console.log(this.newsForm.get('newsImg').value);
    const formData = new FormData();
    formData.append('file', this.newsForm.get('newsImg').value);
    //  this.accountService.register(this.newsForm.value).subscribe(user=>{
    //    this.newsForm.reset();
    //    this.router.navigateByUrl('/news');
    //  },error=>{
    //    this.validationErrors=error;
    //  });
  }
  getTypes() {
    this.newsService.getNewsTypes().subscribe(newsTypes => {
      this.newsTypes = newsTypes;
    });
  }
}


