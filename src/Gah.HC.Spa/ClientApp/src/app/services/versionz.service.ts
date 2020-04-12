import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Result } from './models/result';

@Injectable({
  providedIn: 'root'
})
export class VersionzService {

  constructor(private http: HttpClient) { }

  public getVersionz(): Observable<Result<string>> {
    return this.http.get<Result<string>> ("versionz");
  }
}
