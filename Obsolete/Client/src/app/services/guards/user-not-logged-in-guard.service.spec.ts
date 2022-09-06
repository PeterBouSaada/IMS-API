import { TestBed } from '@angular/core/testing';

import { UserNotLoggedInGuardService } from './user-not-logged-in-guard.service';

describe('UserNotLoggedInGuardService', () => {
  let service: UserNotLoggedInGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserNotLoggedInGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
