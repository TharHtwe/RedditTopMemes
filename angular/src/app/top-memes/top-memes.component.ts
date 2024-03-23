import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { TopMemeDto, TopMemeItemDetailDto, TopMemeServiceProxy } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RankHistoriesDialogComponent } from './rank-histories/rank-histories-dialog.component';

class TopMemeDetailsInColumn {
  column1: TopMemeItemDetailDto;
  column2: TopMemeItemDetailDto;
}

@Component({
  templateUrl: './top-memes.component.html',
  animations: [appModuleAnimation()]
})

export class TopMemesComponent extends AppComponentBase implements OnInit {

  topMeme: TopMemeDto = new TopMemeDto();
  topMemeDetails: TopMemeDetailsInColumn[];
  initializing: boolean = true;

  constructor(
    injector: Injector,
    private route: ActivatedRoute,
    private _modalService: BsModalService,
    private _topMemeService: TopMemeServiceProxy) {
    super(injector);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.getTopMeme(parseInt(id));
        } else {
          this.getLatestTopMeme();
        }
      }
    })
  }

  getLatestTopMeme(): void {
      this._topMemeService.getLatest().subscribe((result) => {
        this.topMeme = result;
        this.initializing = false;
      })
  }

  getTopMeme(id: number): void {
    this._topMemeService.get(id).subscribe((result) => {
      this.topMeme = result;
    })
  }

  createTopMeme(): void {
    this._topMemeService.create().subscribe(result => {
      this.topMeme = result;
      abp.notify.success(this.l('SuccessfullyGotLatest'));
    })
  }

  sendToTelegram(): void {
    this._topMemeService.sendToTelegramBot().subscribe(() => {
      abp.notify.success(this.l('SentToTelegramSuccessfully'));
    })
  }

  getUrl(meme: TopMemeItemDetailDto): string {
    return 'https://www.reddit.com/r/memes/comments/' + meme.memeId;
  }

  separateTopMemesTo2Columns(): void {
    this.topMemeDetails = [];

    for (var i = 0; i < this.topMeme.topMemeItems.length; i = i + 2) {
      if (i % 2 == 0) {
        this.topMemeDetails.push({
          column1: this.topMeme.topMemeItems[i], 
          column2: this.topMeme.topMemeItems[i + 1]
        });
      }
    }
  }

  showRankHistoryDialog(id: string): void {
    this._modalService.show(
        RankHistoriesDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
          
        }
      );
  }
}
