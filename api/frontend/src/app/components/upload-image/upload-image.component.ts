import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-upload-image',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upload-image.component.html',
  styleUrl: './upload-image.component.css'
})
export class UploadImageComponent {
  selectedImage: any = null;
  imagePreview: string | ArrayBuffer | null = null;

  onFileSelected(event: any) {
    this.selectedImage = event.target.files[0];

    // Create a FileReader to generate a preview of the selected image
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreview = reader.result;
    };
    reader.readAsDataURL(this.selectedImage);
  }

  onUpload() {
    // Add your upload logic here
    console.log('Image uploaded');
  }

  onChooseDifferentImage() {
    this.selectedImage = null;
    this.imagePreview = null;
  }
}
