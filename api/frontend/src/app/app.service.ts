import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {AiImages} from "./app.model";

@Injectable({
  providedIn: 'root'
})

export class AppService {
  private apiUrl = 'http://localhost:5214/api/';

  constructor(private http : HttpClient) {}

  getAllImages() : Observable<AiImages[]>{
    return this.http.get<AiImages[]>(`${this.apiUrl}Image`);
  }
  getImageById(id : number) : Observable<AiImages>{
    return this.http.get<AiImages>(`${this.apiUrl}Image/GetImageById?id=${id}`);
  }

  getImagesByCategory(category : string) : Observable<AiImages[]> {
    return this.http.get<AiImages[]>(`${this.apiUrl}Image/GetByCategory?category=${category}`);
  }

  addImage(aiImages : AiImages) : Observable<AiImages>{
    return this.http.post<AiImages>(`${this.apiUrl}Image/AddImage`, aiImages)
  }

  updateImage(id : number, aiImages : AiImages){
    return this.http.put<void>(`${this.apiUrl}Image/${id}`, aiImages)
  }

  deleteImage(id : number) {
    return this.http.delete<void>(`${this.apiUrl}Image/${id}`)
  }

}
