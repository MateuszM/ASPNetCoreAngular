import { Component } from '@angular/core';
import { BaseService } from '../../core/BaseService';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
/** register component*/
export class RegisterComponent extends BaseService {
  /** register ctor */
  constructor() { super()}
}
