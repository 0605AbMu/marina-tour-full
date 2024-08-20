<script setup>
import useAuthStore from "@/stores/authStore.js";
import {useRouter} from "vue-router";
import {onBeforeMount} from "vue";
import notificationHelper from "@/tools/NotificationHelper.js";

const router = useRouter();
const authStore = useAuthStore();

onBeforeMount(async () => {
  if (!authStore.userSignedIn) {
    await router.push({path: "/", replace: true, force: true});
    notificationHelper.Error("Access denied!")
  }

  const route = router.currentRoute.value;

  if (Array.isArray(route.meta["requiredRoles"]) && route.meta["requiredRoles"].indexOf(authStore.GetRole) === -1) {
    await router.push({path: "/", replace: true, force: true});
    notificationHelper.Error("Access denied!")
  }
})

</script>

<template>
  <router-view/>
</template>

<style scoped>

</style>