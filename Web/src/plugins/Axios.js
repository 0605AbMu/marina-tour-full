import axios from "axios";
import NotificationHelper from "@/tools/NotificationHelper.js";

export const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
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