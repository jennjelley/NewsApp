import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { StoryService } from './_services/story.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor() {}

  ngOnInit() {
  }
}
