import { AppUserType } from "./app-user-type.enum";

export class UserDto {
  constructor(
    public userType: AppUserType,
    public RegionId?: string,
    public HospitalId?: string) { }
}
