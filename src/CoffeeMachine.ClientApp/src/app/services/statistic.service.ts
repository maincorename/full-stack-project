import { ConfigurationService } from './configuration.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IStatistic } from '../models/statistic';

@Injectable({ providedIn: 'root' })
export class StatisticService {
  private statisticPath: string;

  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
    this.statisticPath = this.configurationService.getValue('requestPathStatistic', 'notfound');
  }

  GetAll() {
    return this.http.get<IStatistic[]>(this.statisticPath);
  }

  GetByCoffeeId(id: string) {
    return this.http.get<IStatistic>(this.statisticPath + `/coffee/${id}`);
  }

  IncreaseByCoffeeId(id: string) {
    return this.http.put(this.statisticPath + `/${id}`, null);
  }
}
