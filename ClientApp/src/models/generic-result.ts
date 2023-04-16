export interface GenericResult<T> {
    data?: T;
    success?: boolean;
    message?: string;
    error?: string;
    errorMessages?: string[];
}
