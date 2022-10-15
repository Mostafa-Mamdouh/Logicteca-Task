import { HttpClient, HttpEvent, HttpEventType, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})
export class UploadFileComponent implements OnInit {
  baseUrl = environment.apiUrl;
  @Output() onFileUploaded = new EventEmitter<any>();

  currentFile?: File;
  progress = 0;
  message = '';

  fileName = 'Select File';
  fileInfos?: Observable<any>;

  constructor(private http: HttpClient,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  uploadService(file: File): Observable<any> {
    debugger
    const formData: FormData = new FormData();

    formData.append('file', file);
   

    const req = new HttpRequest('POST', `${this.baseUrl}Product/SaveBulkData`, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(req);
  }


  selectFile(event: any): void {
    debugger
    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      this.currentFile = file;
      this.fileName = this.currentFile.name;
    } else {
      this.fileName = 'Upload';
    }
  }

  
  upload(): void {
    debugger
    this.progress = 0;
    this.message = "";
    if (this.currentFile) {
      this.uploadService(this.currentFile).subscribe(
        (event: any) => {
          debugger
          if (event.ok) {
            this.progress = Math.round(100 * event.loaded / event.total);
           this.docUpload(event.ok);
          } 
        },
        (err: any) => {
          console.log(err);
          this.progress = 0;

          if (err.error && err.error.message) {
            this.message = err.error.message;
          } else {
            this.message = 'Could not upload the file!';
          }

          this.currentFile = undefined;
        });
    }

  
  }
  docUpload(event:any) {
    this.onFileUploaded.emit(event);
    console.log('ApiResponse -> docUpload -> Event: ',event);
  }



}
