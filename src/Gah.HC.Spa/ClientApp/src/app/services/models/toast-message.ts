export class ToastMessage {
  constructor(
    public body: string,
    public className = 'bg-success text-light',
    public autoHide = true,
    public delay = 5000
  ) { }
}
