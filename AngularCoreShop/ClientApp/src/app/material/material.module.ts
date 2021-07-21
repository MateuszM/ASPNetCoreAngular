import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NgModule } from '@angular/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [ReactiveFormsModule,MatMenuModule, MatSelectModule, /*MatOptionModule,*/ MatFormFieldModule, MatChipsModule, MatExpansionModule, DragDropModule, MatToolbarModule, MatButtonModule, MatInputModule, MatIconModule, MatCardModule, MatProgressSpinnerModule, MatListModule, MatSidenavModule],
  exports: [ReactiveFormsModule,MatMenuModule, MatSelectModule, /*MatOptionModule,*/MatFormFieldModule, MatChipsModule, MatExpansionModule, DragDropModule, MatToolbarModule, MatButtonModule, MatInputModule, MatIconModule, MatCardModule, MatProgressSpinnerModule, MatListModule, MatSidenavModule]
})

export class MaterialModule { }
