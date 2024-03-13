import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { UploadImageComponent } from '../upload-image/upload-image.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule, MatDialogModule, UploadImageComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  categories: string[] = ['Human', 'Dog', 'Cat', 'Chicken', 'Cow', 'Sheep',
    'Horse', 'Squirrel', 'Elephant', 'Butterfly', 'Spider', 'Fruit', 'Plant', 'Vegetable'];

  constructor(public dialog: MatDialog) { }

  addImage() {
    this.dialog.open(UploadImageComponent);
  }

  categoryClicked(category: string) {
    console.log(category);
  }

}
