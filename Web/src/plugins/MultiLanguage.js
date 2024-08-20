import i18n from "@/plugins/i18n.js";

export default {
    install: (app, options) => {
        app.config.globalProperties.$format = (data) => {

            if (!data) return "";
            return data[i18n.global.locale] ?? "";
        }
    }
}