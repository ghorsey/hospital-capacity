import { LoaderService } from './loader.service';

describe('LoaderService', () => {
  let service: LoaderService;
  beforeEach(() => {
    service = new LoaderService();
  });

  it('#show should set show to true on the subject', (done: DoneFn) => {
    service.loaderState.subscribe((state) => {
      expect(state.show).toBeTruthy();
      done();
    });
    service.show();
  });

  it('#hide should set show to false on the subject', (done: DoneFn) => {
    service.loaderState.subscribe((state) => {
      expect(state.show).toBeFalsy();
      done();
    });
    service.hide();
  });
});
