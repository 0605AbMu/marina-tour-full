import {ElNotification} from 'element-plus';
import i18n from '../plugins/i18n';

const t = i18n.global.t;

class NotificationHelper {
    /**
     *
     * @param {String} message
     * @param {"error" | "success" | "warning" | "info"} type
     */
    static Notify(message, type = 'info') {
        if (ElNotification[type]) {
            ElNotification[type]({
                title: type.toLocaleUpperCase(), message: message, duration: 2000, dangerouslyUseHTMLString: true
            });
        }
        let logger = console.log;
        switch (type) {
            case 'error':
                logger = console.error;
                break;
            case 'info':
                logger = console.info;
                break;
            case 'warning':
                logger = console.warn;
                break;
            case 'success':
                logger = console.log;
                break;

            default:
                break;
        }

        logger(message);
    }

    static Info(message = t('info')) {
        this.Notify(message, 'info');
    }

    static Error(message = t('error')) {
        this.Notify(message, 'error');
    }

    static Warning(message = t('warning')) {
        this.Notify(message, 'warning');
    }

    static Success(message = t('success')) {
        this.Notify(message, 'success');
    }

    static AxiosError(error) {
        if (!error.response) {
            this.Error(t('networkError'));
        } else if (error.response.data instanceof Blob) {
            const reader = new FileReader();
            reader.onloadend = (ev) => {
                if (ev?.target?.result) {
                    var res = JSON.parse(ev.target.result);
                    NotificationHelper.Error(res.error);
                } else NotificationHelper.Error();
            };
            reader.readAsText(error.response.data);
        } else if (error.response.data) {
            if (error.response.status === 400 && error.response.data?.modelStateError && Array.isArray(error.response.data?.modelStateError)) {
                this.Error(`${error.response.statusText} - ${error.response.status}.<br>${error.response.data.modelStateError.at(-1)?.errorMessage}`);
            } else if (error.response.status !== 401) this.Error(`${error.response.statusText} - ${error.response.status}.\n${error.response.data.error}`);
        } else if (error.response.status !== 401) this.Error(`${error.status} - ${error.code}.\n${error.message}`);
    }

    static ValidationError(message = t('validationError')) {
        this.Notify(message, 'warning');
    }
}

export default NotificationHelper;
export {NotificationHelper};
