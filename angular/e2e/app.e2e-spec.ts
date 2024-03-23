import { RedditTopMemesTemplatePage } from './app.po';

describe('RedditTopMemes App', function() {
  let page: RedditTopMemesTemplatePage;

  beforeEach(() => {
    page = new RedditTopMemesTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
