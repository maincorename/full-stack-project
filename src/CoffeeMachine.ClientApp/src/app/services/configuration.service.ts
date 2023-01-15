import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConfigurationService {
  private configuration = {};

  constructor(private http: HttpClient) {}

  load(): Observable<void> {
    return this.http.get('../../assets/servicesconfig.json').pipe(
      tap((configuration: any) => (this.configuration = configuration)),
      map(() => undefined),
    );
  }

  getValue(key: string, defaultValue?: any): any {
    return this.configuration[key] || defaultValue;
  }
}
