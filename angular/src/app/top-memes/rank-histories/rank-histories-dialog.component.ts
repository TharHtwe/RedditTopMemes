import {
  Component,
  Injector,
  OnInit
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  MemeRankHistoryDto,
  MemeServiceProxy
} from '@shared/service-proxies/service-proxies';
import Chart from 'chart.js/auto';
import { AbpModalFooterComponent } from '@shared/components/modal/abp-modal-footer.component';

@Component({
  templateUrl: './rank-histories-dialog.component.html'
})
export class RankHistoriesDialogComponent extends AppComponentBase
  implements OnInit {
  rankHistories: MemeRankHistoryDto[] = [];
  id: string;
  chart: any;
  postedDate: string;

  constructor(
    injector: Injector,
    public _memeService: MemeServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._memeService.getRankHistories(this.id).subscribe((result) => {
      this.rankHistories = result.histories;
      this.postedDate = (new Date(result.memePostedDate.toString())).toLocaleString()
      this.createChart();
    });
  }

  createChart(){
  
    this.chart = new Chart("MyChart", {
      type: 'line', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: this.rankHistories.map(x => (new Date(x.date.toString())).toLocaleString()),
	       datasets: [
          {
            label: 'Rank',
            data: this.rankHistories.map(x => x.rank),
            backgroundColor: 'blue',
          }
        ]
      },
      options: {
        aspectRatio:2.5,
        scales: {
          y: {
            reverse: true,
            beginAtZero: false,
            ticks: {
              stepSize: 1,
              callback: function(value, index, ticks) {
                if (value == 0) return NaN;
                else return value;
              }
            },
            grid: {
              display: false
            }
          },
          x: {
            grid: {
              display: false
            }
          }
        }
      },
      
      
    });
  }
}
