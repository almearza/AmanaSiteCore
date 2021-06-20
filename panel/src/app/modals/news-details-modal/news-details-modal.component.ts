import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-news-details-modal',
  templateUrl: './news-details-modal.component.html',
  styleUrls: ['./news-details-modal.component.css']
})
export class NewsDetailsModalComponent implements OnInit {
@Input() newsDetails:string;
@Input() imgUrl:string;
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
    console.log(this.newsDetails);
  }
}
