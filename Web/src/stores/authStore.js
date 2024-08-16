import {defineStore} from 'pinia'
import {ref} from 'vue'
import authService from "@/services/AuthService.ts";

export const useAuthStore = defineStore('auth-store', () => {

    const userSignedIn = ref(false)
    const me = ref();

    const LoadMe = async () => {
        try {
            const res = await authService.GetMe();
            console.log(res);
            me.value = res.content;
            userSignedIn.value = true;
        } catch (e) {
            me.value = {};
            userSignedIn.value = false;
        }
    }


    return {
        me, userSignedIn, LoadMe
    }
})

export default useAuthStore
