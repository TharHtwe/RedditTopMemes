import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { TopMemeDto, TopMemeItemDetailDto, TopMemeServiceProxy } from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  templateUrl: './histories.component.html',
  animations: [appModuleAnimation()]
})

export class HistoriesComponent extends PagedListingComponentBase<TopMemeDto> {

  topMemes: TopMemeDto[] = [];

  constructor(
    injector: Injector,
    private router: Router,
    private _topMemeService: TopMemeServiceProxy) {
    super(injector);
  }

  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {

    this._topMemeService
      .getAll(
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: PagedResultDto) => {
        this.topMemes = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  createTopMeme(): void {
    this._topMemeService.create().subscribe(result => {
      abp.notify.success(this.l('SuccessfullyUpdated'));
      this.refresh();
    })
  }

  viewTopMeme(id: number): void {
    this.router.navigate(['app/top-memes/' + id])
  }

  toLocalTime(date: Date): string {
    return (new Date(date)).toLocaleString();
  }

  protected delete(topMeme: TopMemeDto): void {
    abp.message.confirm(
      this.l('DeleteConfirmation'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._topMemeService.delete(topMeme.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }
}
