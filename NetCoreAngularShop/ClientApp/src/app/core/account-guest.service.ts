import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './BaseService';
import { ConfigService } from './config.service';
import { AccountSignUp } from './Models/account-sign-up-model/AccountSignUp';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AccountGuestService extends BaseService {

  constructor(private http: HttpClient, private configService: ConfigService) {
    super();
  }

  signup(accountSignup: AccountSignUp) {
    return this.http.post(this.configService.authApiURI + '/accounts', accountSignup).pipe(catchError(this.handleError));
  }
}
