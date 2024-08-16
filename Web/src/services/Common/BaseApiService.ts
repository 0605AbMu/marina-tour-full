import {Axios, AxiosError, AxiosResponse, ResponseType} from 'axios';
import {axiosInstance} from '../../plugins/Axios';
import NotificationHelper from '../../tools/NotificationHelper';
import urlJoin from 'url-join';

export type HttpMethods = 'GET' | 'POST' | 'PUT' | 'DELETE';
export type ApiResponse<T = object | []> = {
    code: number;
    content?: T;
    error: string;
    id: string;
    modelStateError: string[];
    total: number;
};

export interface HttpConfig {
    ContentType: string;
    Serialize: (d: any) => string;
    Deserialize: <T>(s: string) => T;
    ResponseType: ResponseType;
}

// console.log(instance);

abstract class BaseApiService {
    private axios: Axios;
    private baseUrl: string | undefined;
    protected httpConfig: HttpConfig;

    /**
     *
     */
    constructor(
        httpConfig: HttpConfig = {
            ContentType: 'application/JSON',
            Deserialize: JSON.parse,
            Serialize: JSON.stringify,
            ResponseType: 'json'
        }
    ) {
        this.axios = axiosInstance;
        this.httpConfig = httpConfig;

        if (this.httpConfig.Serialize === undefined)
            this.httpConfig.Serialize = JSON.stringify;

        if (this.httpConfig.Deserialize === undefined)
            this.httpConfig.Deserialize = JSON.parse;
    }

    // async GetAsync(
    //   url: string,
    //   searchParams: URLSearchParams
    // ): Promise<AxiosResponse> {
    //   return await this.SendAsync(url, 'GET', searchParams, undefined);
    // }

    // abstract GetAsync(searchParams: URLSearchParams): Promise<ApiResponse>;
    //
    // abstract PostAsync(data: any): Promise<ApiResponse>;
    //
    // abstract PutAsync(data: any): Promise<ApiResponse>;
    //
    // abstract DeleteAsync(data: any): Promise<ApiResponse>;

    async SendAsync(
        url: string,
        method: HttpMethods,
        searchParams: URLSearchParams | undefined,
        data: any
    ): Promise<AxiosResponse & ApiResponse> {
        return new Promise<AxiosResponse & ApiResponse>((res, rej) => {
            this.axios
                .request({
                    method: method.toLowerCase(),
                    data: data ? this.httpConfig.Serialize(data) : data,
                    headers: {
                        'Content-Type': this.httpConfig.ContentType ?? 'application/json'
                    },
                    url: urlJoin(
                        this.baseUrl ?? '',
                        url,
                        searchParams ? '?' + searchParams.toString() : ''
                    ),
                    responseType: this.httpConfig.ResponseType
                })
                .then(response => res({...response, ...(response.data && response.data.id ? response.data : {})}))
                .catch((error) => {
                    if (error instanceof AxiosError) NotificationHelper.AxiosError(error);
                    else NotificationHelper.Error(error);
                    rej(error);
                });
        });
    }

    async DownloadAsync(
        url: string,
        method: HttpMethods,
        searchParams: URLSearchParams | undefined,
        data: any,
        withoutBaseUrl: boolean = false
    ): Promise<void> {
        return new Promise<void>(async (res, rej) => {
            return this.axios
                .request({
                    method: method.toLowerCase(),
                    data: data ? this.httpConfig.Serialize(data) : data,
                    headers: {
                        'Content-Type': this.httpConfig.ContentType ?? 'application/json',
                        Accept: 'application/octet-stream'
                    },
                    url: urlJoin(
                        this.baseUrl && !withoutBaseUrl ? this.baseUrl : '',
                        url,
                        searchParams ? '?' + searchParams.toString() : ''
                    ),
                    responseType: 'blob'
                })
                .then((response) => {
                    return new Promise<{ href: string; name: string }>((res, rej) => {
                        const blob = response.data;
                        var filename = '';
                        var disposition = response.headers['content-disposition'];
                        if (disposition && disposition.indexOf('attachment') !== -1) {
                            var filenameRegex = /filename\*=UTF-8''([\w%\-\.]+)(?:; ?|$)/i;
                            var matches = filenameRegex.exec(disposition);
                            if (matches != null && matches[1]) {
                                filename = decodeURIComponent(matches[1].replace(/['"]/g, ''));
                            }
                        }
                        var href = URL.createObjectURL(blob as any);
                        res({href: href, name: filename});
                    });
                })
                .then((data) => {
                    this.DownloadHrefWithLink(data.href, data.name as any);
                })
                .catch((error) => {
                    if (error instanceof AxiosError) NotificationHelper.AxiosError(error);
                    else NotificationHelper.Error(error);
                    rej(error);
                });

            // .then((res) => res.data)
            // // .then((res) => {
            // //   const disp: string = res.headers['content-disposition'] ?? '';
            // //   const fileName = disp.split('; ')[1]?.split('=')[1] ?? 'unknown-file';
            // //   return ({res.});
            // // })
            // // .then((val) => {
            // //   var href = URL.createObjectURL(val.da as any);
            // //   this.DownloadHrefWithLink(href, fileName as any);
            // // })
        });
    }

    async DownloadHrefWithLink(href, downloadName = undefined) {
        const link = document.createElement('a');
        link.href = href;
        if (!!downloadName) link.setAttribute('download', downloadName); //or any other extension
        document.body.appendChild(link);
        link.click();

        document.body.removeChild(link);
        URL.revokeObjectURL(href);
    }
}

export default BaseApiService;
