import { StatisticService } from './../../../services/statistic.service';
import { IStatistic } from 'src/app/models/statistic';
import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StatisticsResolver implements Resolve<IStatistic[]> {
  constructor(private statisticService: StatisticService) {}

  resolve(): Observable<IStatistic[]> {
    return this.statisticService.GetAll();
  }
}
