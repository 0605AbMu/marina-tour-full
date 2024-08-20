import BaseApiService from "./Common/BaseApiService";

export class TripService extends BaseApiService {

    constructor() {
        super();
    }

    async GetAll() {
        return (await this.SendAsync("trips", "GET", null, null));
    }

    async GetById(id: number) {
        return (await this.SendAsync("trips/" + id, "GET", null, null));
    }

    async Add(data) {
        return (await this.SendAsync("trips", "POST", null, data));
    }

    async Update(data) {
        return (await this.SendAsync("trips", "PUT", null, data));
    }

}

export const tripService = new TripService();
export default tripService;
