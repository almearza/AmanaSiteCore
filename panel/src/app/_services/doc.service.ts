import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Doc } from '../_models/doc';

@Injectable({
  providedIn: 'root'
})
export class DocService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  addDoc(doc: any) {
    return this.http.post<Doc>(this.baseUrl + 'docs/add-doc', doc)
  }
  deletedoc(id: number) {
    return this.http.delete(this.baseUrl + 'docs/delete-doc/' + id, {});
  }
}