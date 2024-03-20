import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom, map, Observable, Subject} from "rxjs";
import {AiImages} from "./app.model";

@Injectable({
  providedIn: 'root'
})

export class AppService {
  private apiUrl = 'http://localhost:5214/api/';

  allImages: AiImages[] = [];
  tempImages: AiImages[] = [];
  private displayedImages = new Subject<AiImages[]>();

  constructor(private http : HttpClient) {
  }

  getDisplyedImagesListener(){
    return this.displayedImages;
  }

  async getAllImages() {
    const call = this.http.get<AiImages[]>(`${this.apiUrl}Image`);
    this.allImages = await firstValueFrom<AiImages[]>(call);
  }
  getImageById(id : number) : Observable<AiImages>{
    return this.http.get<AiImages>(`${this.apiUrl}Image/GetImageById?id=${id}`);
  }

  getImagesByCategory(category : string)  {
    this.tempImages = []
    this.allImages.forEach((image =>{
      if(image.category == category){
        this.tempImages.push(image);
      }
    }));
    this.displayedImages.next(this.tempImages);
  }

  async addImage(aiImages : AiImages){
    const call = this.http.post<AiImages>(`${this.apiUrl}Image/AddImage`, aiImages);
    return await firstValueFrom(call);
  }

  updateImage(id : number, aiImages : AiImages){
    return this.http.put<void>(`${this.apiUrl}Image/${id}`, aiImages)
  }

  deleteImage(id : number) {
    return this.http.delete<void>(`${this.apiUrl}Image/${id}`)
  }

  async uploadBlob(formData: FormData) {
    const call = this.http.post(`http://localhost:5214/uploadfile`, formData, {responseType: 'text'});
    return await firstValueFrom(call);
  }

  async getBlob(imageName: string) {
    const call = this.http.get(`BlobStorageController/GetImage?imageName=${imageName}`, {responseType: 'blob'});
    return await firstValueFrom(call);
  }

  async getPrediction(blobUrl: string) {
    const call = this.http.post(`${this.apiUrl}VisionPrediction?imageUrl=${blobUrl}`, {}, {responseType: 'text'}).pipe(map(response => response as string));
    return await firstValueFrom<string>(call);
  }

}
