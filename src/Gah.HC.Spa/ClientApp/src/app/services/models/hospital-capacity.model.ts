export class HospitalCapacity {
  constructor(
    public hospitalId: string,
    public bedCapacity: number,
    public bedsInUse: number,
    public percentOfUsage: number,
    public createdOn: Date
  ) {}
}
