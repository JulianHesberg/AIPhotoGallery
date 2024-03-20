import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {AppService} from "../../app.service";
import {AiImages} from "../../app.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-upload-image',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upload-image.component.html',
  styleUrl: './upload-image.component.css'
})
export class UploadImageComponent {
  selectedImage: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;


  constructor(private service: AppService, private router: Router) {
  }

  onFileSelected(event: any) {
    this.selectedImage = <File>event.target.files[0];

    // Create a FileReader to read the contents of the selected image
    let reader = new FileReader();

    // Set the imagePreview to the result of reading the selected image
    reader.onload = (e) => this.imagePreview = reader.result;

    // Read the contents of the selected image
    reader.readAsDataURL(this.selectedImage);
  }

  async onUpload() {
    let formData = new FormData();
    formData.append('file', this.selectedImage!);
    var blobUrl = await this.service.uploadBlob(formData)
    var encodedUri = encodeURIComponent(blobUrl);
    var category = await this.service.getPrediction(encodedUri);

    let aiImage: AiImages = {
      imageId: 0,
      category: category,
      imageUrl: blobUrl
    }
    console.log(aiImage);
    await this.service.addImage(aiImage);
    await this.service.getAllImages();
    window.location.reload();
  }

  onChooseDifferentImage() {
    this.selectedImage = null;
    this.imagePreview = null;
  }
}
