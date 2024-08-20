import BaseApiService from "./Common/BaseApiService";
import axiosInstance from "../plugins/Axios";

export class AuthService extends BaseApiService {

    constructor() {
        super();
    }

    async SignIn(name, phoneNumber) {
        return (await this.SendAsync("auth/sign", "POST", null, {
            name,
            phoneNumber: `+998${phoneNumber}`
        }))
    }

    async Verify(key, code) {
        return (await this.SendAsync("auth/verify", "POST", null, {
            key, code
        }));
    }

    async SignOut() {
        return (await this.SendAsync("auth/sign-out", "POST", null, {}));
    }

    async GetMe() {
        return (await this.SendAsync("auth/me", "GET", null, null));
    }

    async GetAllUsers() {
        return (await this.SendAsync("auth/users", "GET", null, null));
    }

}

export const authService = new AuthService();
export default authService;
