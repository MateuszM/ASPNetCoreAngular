import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { BaseService } from '../BaseService';
import { ConfigService } from '../config.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  private user: User | null;
  private authNavStatusSource = new BehaviorSubject<boolean>(false);
  authNavStatus$ = this.authNavStatusSource.asObservable();

  constructor(private configService: ConfigService) {
    super();

    this.manager.getUser().then(user => {
      this.authNavStatusSource.next(this.isAuthenticated());
      this.user = user;
    });
  }
  private manager = new UserManager({
    authority: this.configService.authAppURI,
    client_id: 'angular_spa',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: "code",
    scope: "openid profile email api.read",
    filterProtocolClaims: true,
    loadUserInfo: true
  });
  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  login(newAccount?: boolean, userName?: string) {

    let extraQueryParams = newAccount && userName ? {
      newAccount: newAccount,
      userName: userName
    } : {};

    // https://github.com/IdentityModel/oidc-client-js/issues/315
    return this.manager.signinRedirect({
      extraQueryParams
    });
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this.authNavStatusSource.next(this.isAuthenticated());
  }

}
