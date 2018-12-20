import { Component } from '@angular/core';
import { OAuthService } from "angular-oauth2-oidc";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  constructor(private oauthService: OAuthService) { }

  logoutHandler() {
    this.oauthService.logOut();
  }
}
