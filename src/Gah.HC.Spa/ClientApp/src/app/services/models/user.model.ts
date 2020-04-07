export class User {
  constructor(
    public email: string = '',
    public regionName: string = '',
    public password: string = '',
    public confirmPassword: string = '',
  ) {}
}
