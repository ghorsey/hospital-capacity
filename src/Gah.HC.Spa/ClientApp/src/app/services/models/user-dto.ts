import { AppUserType } from "./app-user-type.enum";

export class UserDto {
  constructor(
    public userType: AppUserType,
    public regionId?: string,
    public hospitalId?: string) { }
}
