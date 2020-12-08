import { Component, OnInit } from '@angular/core';
import { StoryService } from '../_services/story.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  stories: any;

  constructor(private storyService: StoryService) { }

  ngOnInit() {
    this.getPage(1);
  }

  getPage(page: any) {
    this.storyService.getPage(page).subscribe(response => {
      this.stories = response;
    }, error => {
      console.log(error);
    })
  }
}
