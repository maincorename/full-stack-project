import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { StatisticService } from './../../../../services/statistic.service';
import { Component, OnInit } from '@angular/core';
import { IStatistic } from 'src/app/models/statistic';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
})
export class StatisticsComponent implements OnInit{
  statistics!: Observable<IStatistic[]>;

  constructor(private statisticService: StatisticService, private router: Router){}
  
  ngOnInit(): void {
    this.statistics = this.statisticService.GetAll();
  }
}
