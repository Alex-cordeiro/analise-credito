export default interface ICommonResponse<T> {
  data?: T;
  success: boolean;
  message: string;
  errors: string[];
}