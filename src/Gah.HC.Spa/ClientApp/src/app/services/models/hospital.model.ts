export class Hospital {
  constructor(
    public slug: string = '',
    public name: string = '',
    public address1: string = '',
    public address2: string = '',
    public city: string = '',
    public state: string = '',
    public postalCode: string = '',
    public phone: string = '',
    public isCovid: boolean = true,
    public bedCapacity: number = 0,
    public bedsInUse: number = 0,
    public percentOfUsage: number = 0,
    public createdOn: string = '',
    public updatedOn: string = '',
    public regionId: string = '',
    public regionName: string = '',
    public region?: any,
    public id?: string,
  ) {}
}
