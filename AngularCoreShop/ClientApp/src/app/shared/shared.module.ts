import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmPasswordValidatorDirective } from './confirm-password-validator.directive';
import { CustomValidatorsDirective } from './custom-validators.directive';



@NgModule({
  imports: [CommonModule, MaterialModule, MatDialogModule],
  exports: [MaterialModule],
  providers: [],
  entryComponents: [
  ],
  declarations: [ConfirmPasswordValidatorDirective, CustomValidatorsDirective],
})
export class SharedModule { }
