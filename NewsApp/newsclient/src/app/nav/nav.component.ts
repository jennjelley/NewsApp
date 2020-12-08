import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { StoryService } from '../_services/story.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  constructor() { }

  ngOnInit(): void {
  }

}
