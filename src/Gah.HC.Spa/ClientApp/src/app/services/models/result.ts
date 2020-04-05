export class Result<T> {

  constructor(
    public readonly value: T,
    public readonly message = '',
    public readonly success = true) { }
}
