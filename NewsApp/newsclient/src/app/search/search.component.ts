import { Component, OnInit } from '@angular/core';
import { StoryService } from '../_services/story.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  model: any = {}
  searchResults: any
  constructor(public storyService: StoryService) { }

  ngOnInit(): void {
  }

  search() {
    this.storyService.search(this.model.search).subscribe(response => {
      console.log(response);
      this.searchResults = response;
    }, error => {
      console.log(error);
    })
  }
}
