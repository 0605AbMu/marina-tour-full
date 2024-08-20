import {defineStore} from 'pinia'
import {ref, computed} from 'vue'
import authService from "@/services/AuthService.ts";

export const Roles = {
    Admin: "ADMIN",
    Client: "CLIENT",
}

export const useAuthStore = defineStore('auth-store', () => {

    const userSignedIn = ref(false)
    const me = ref();

    const LoadMe = async () => {
        try {
            const res = await authService.GetMe();
            me.value = res.content;
            userSignedIn.value = true;
        } catch (e) {
            me.value = {};
            userSignedIn.value = false;
        }
    }

    const GetRole = computed(() => me && me.value ? me.value.role : null);


    return {
        me, userSignedIn, LoadMe, GetRole
    }
})

export default useAuthStore
