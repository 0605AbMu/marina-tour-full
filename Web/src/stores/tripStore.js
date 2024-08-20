import {defineStore} from 'pinia'
import {ref} from 'vue'
import tripService from "@/services/TripService.ts";
import {formatPrice} from "@/plugins/formatters.js";

export const useTripStore = defineStore('trip-store', () => {
    const trips = ref([
        {
            img: '/images/slides/1.jpg',
            price: 10080000,
            title: 'Bali',
            region: 'Indonesia',
            description: "lorem dsfsd fsd fsdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf dsf sd fsd sd sfdd fsd fdsf dsf sdf sdf dsf sd fsdf sdf df sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sdf sd  sdf ds dsf sdf dsf sdf sdf sdf sdfsd fdsf sdf sdf sdfsd fdsf dsf sdfsdf f",
            star: 4
        },
        {
            img: '/images/slides/2.jpg',
            price: 1134000,
            title: 'Antalya',
            region: 'Turkey',
            star: 4.5
        },
        {
            img: '/images/slides/3.jpg',
            price: 8190000,
            title: 'Istanbul',
            region: 'Turkey',
            star: 4
        },
        {
            img: '/images/slides/4.jpg',
            price: 9450000,
            title: 'Thailand',
            region: 'Thailand',
            star: 4
        }
    ])
    
    const GetTrips = async () => {
        // if (trips.value)
        //     return trips;

        const {content} = await tripService.GetAll();

        trips.value = content.map(x => ({
            id: x.id,
            img: x.images,
            price: x.price,
            star: x.rank,
            title: x.name,
            region: x.location,
            description: x.description
        }));

        return trips;

    }

    return {
        GetTrips,
        formatPrice
    }
})

export default useTripStore
