import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  baseUrl = 'http://localhost:5000/api/stories/';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl)
  }

  getPage(page: any) {
    let params = new HttpParams().set("page", page)
    return this.http.get(this.baseUrl + 'page', { params: params })
  }

  search(model: any) {
    let params = new HttpParams().set("searchTerm", model)
    return this.http.get(this.baseUrl + 'search', {params: params})
  }
}
