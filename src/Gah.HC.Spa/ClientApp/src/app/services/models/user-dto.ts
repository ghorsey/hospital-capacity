import { AppUserType } from "./app-user-type.enum";

export class UserDto {
  constructor(
    public userType: AppUserType,
    public id: string,
    public isApproved: boolean,
    public userName: string,
    public regionId?: string,
    public hospitalId?: string) { }
}
