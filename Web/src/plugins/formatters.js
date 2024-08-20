import i18n from "@/plugins/i18n.js";

export const formatMultilanguage = (data) => {

    if (!data) return "";
    return data[i18n.global.locale] ?? "";
};

export const formatPrice = (amount) => {
    return (
        new Intl.NumberFormat('uz-UZ', {
            style: 'currency',
            currency: 'UZS',
            maximumFractionDigits: 20,
            notation: 'standard'
        })
            .formatToParts(amount)
            .map((x) => {
                if (x.type === 'group') return ' '
                else if (x.type == 'currency') return ''
                else return x.value
            })
            .join('') + ' UZS'
    )
}


export default {
    install: (app, options) => {
        app.config.globalProperties.$format = formatMultilanguage;
        app.config.globalProperties.$formatMoney = formatPrice;
    }
}