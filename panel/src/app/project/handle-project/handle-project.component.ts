import { DatePipe } from '@angular/common';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'src/app/_models/project';
import { QuillConfiguration } from 'src/app/_models/generalconfig';
import { ProjectService} from 'src/app/_services/project.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-handle-project',
  templateUrl: './handle-project.component.html',
  styleUrls: ['./handle-project.component.css']
})
export class HandleProjectComponent implements OnInit {

  project:Project;
  baseUrl = environment.baseUrl;
  projectForm: FormGroup;
  validationErrors: string[] = [];
  formData: FormData = new FormData();
  quillConfiguration = QuillConfiguration;
  minDate: Date;
  hasExpireDate = false;
  imgUrl:any;

  @ViewChild("projectForm") _projectForm: FormGroup;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this._projectForm?.dirty) $event.returnValue = true;
  }

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private datePipe: DatePipe
  ) {
  }

  ngOnInit(): void {
    this.activatedRouter.data.subscribe(data => {
      this.project = data.project;
      this.iniailizeForm();
      this.imgUrl=this.baseUrl.replace('api/','')+'images/projects/'+this.project?.imgUrl;
    });
  }
  iniailizeForm() {
    this.projectForm = this.fb.group({
      title: [this.project?.title || '', Validators.required],
      descr: [this.project?.descr || '', Validators.required],
      intro: [this.project?.intro || '', Validators.required],
      projectImg: [''],
      langCode: [this.project?.langCode.toString() || '1'],
    });
    
    if (this.project == null || this.project?.id == 0) this.projectForm.get('projectImg').setValidators(Validators.required);
  }

  handle() {
    this.formData.append("id", this.project?.id.toString() || "0");
    this.formData.append("title", this.projectForm.get('title').value);
    this.formData.append("descr", this.projectForm.get('descr').value);
    this.formData.append("intro", this.projectForm.get('intro').value);
    this.formData.append("langCode", this.projectForm.get('langCode').value);

    this.projectService.handleproject(this.formData).subscribe(() => {
      this.projectForm.reset();
      this.router.navigateByUrl('/projects');
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
