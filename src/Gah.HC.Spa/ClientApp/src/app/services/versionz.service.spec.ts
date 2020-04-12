import { TestBed } from '@angular/core/testing';

import { VersionzService } from './versionz.service';

describe('VersionzService', () => {
  let service: VersionzService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VersionzService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
