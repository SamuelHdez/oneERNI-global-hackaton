import { Component } from '@angular/core';
import { AboutServiceApi } from '../api/about.service';
import { TeamMember } from '../models/TeamMember.model';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
})
export class AboutComponent {

  public teamMembers: TeamMember[] = [];
  
  public constructor(private readonly aboutService: AboutServiceApi) {
    aboutService.getAllTeamMembers().subscribe((data) => {
      this.teamMembers = data;
    });
  }
}
