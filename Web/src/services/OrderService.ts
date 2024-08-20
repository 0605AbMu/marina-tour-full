import BaseApiService from "./Common/BaseApiService";

export class OrderService extends BaseApiService {

    constructor() {
        super();
    }

    async GetAll() {
        return (await this.SendAsync("orders", "GET", null, null));
    }

    async GetAllForAdmin() {
        return (await this.SendAsync("orders/all", "GET", null, null));
    }

    async Add(data) {
        return (await this.SendAsync("orders", "POST", null, data));
    }


}

export const orderService = new OrderService();
export default orderService;
