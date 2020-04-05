import { TestBed } from '@angular/core/testing';

import { AuthGeneralGuardService } from './auth-general-guard.service';

describe('AuthRegionGuardService', () => {
  let service: AuthGeneralGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthGeneralGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
