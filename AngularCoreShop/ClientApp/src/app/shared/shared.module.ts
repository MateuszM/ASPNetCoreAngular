import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material/dialog';



@NgModule({
  imports: [CommonModule, MaterialModule, MatDialogModule],
  exports: [MaterialModule],
  providers: [],
  entryComponents: [
  ],
})
export class SharedModule { }
