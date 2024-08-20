import axios from "axios";

export const ApiBaseUrl = import.meta.env.VITE_API_BASE_URL;
export const FileApiBaseUrl = import.meta.env.VITE_API_BASE_URL.replace("/api", "");

export const axiosInstance = axios.create({
    baseURL: ApiBaseUrl,
    headers: {
        "Content-Type": "application/json",
        'Access-Control-Allow-Origin': '*',
    },
    // transformRequest: (data, headers) => JSON.stringify(data),
    // transformResponse: (data, headers) => JSON.parse(data),
    //
    withCredentials: true,
});

// axiosInstance.interceptors.response.use((response) => {
//     if (response.status >= 400) {
//         if (!response.data) NotificationHelper.Error("Unknown error has raised"); else NotificationHelper.Error(response.data.error);
//     }
//
//     return response;
// }, (error) => NotificationHelper.Error(error));

export default axiosInstance;