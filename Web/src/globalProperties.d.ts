import axios from "axios";

declare module 'vue' {
    interface ComponentCustomProperties {
        $format: (data: { uz: string; ru: string; en: string; }) => string;
    }
}