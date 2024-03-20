import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import {UploadImageComponent} from '../upload-image/upload-image.component';
import {AppService} from "../../app.service";
import {AiImages} from "../../app.model";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule, MatDialogModule, UploadImageComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
  categories: string[] = ['Human', 'Dog', 'Cat', 'Chicken', 'Cow', 'Sheep',
    'Horse', 'Squirrel', 'Elephant', 'Butterfly', 'Spider', 'Fruit', 'Plant', 'Vegetable'];

  images: AiImages[] = [];

  private imageSub: Subscription | undefined;

  async ngOnInit() {
    await this.service.getAllImages();
    this.images = this.service.allImages;
    this.imageSub = this.service.getDisplyedImagesListener().subscribe((images: AiImages[]) => {
      this.images = images
    });
  }


  constructor(public dialog: MatDialog, private service: AppService) {
  }

  addImage() {
    this.dialog.open(UploadImageComponent);
  }

  categoryClicked(category: string) {
    this.service.getImagesByCategory(category);
  }

}
