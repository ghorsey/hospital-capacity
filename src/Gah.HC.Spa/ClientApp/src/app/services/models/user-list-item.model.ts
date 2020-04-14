export class UserListItem {
  constructor(
    public id: string = '',
    public userType: number = 0,
    public userName: string = '',
    public regionName: string = '',
    public hospitalName: string = '',
    public isApproved: boolean = false,
  ) {}
}
