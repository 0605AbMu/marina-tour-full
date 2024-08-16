<script setup>

import {onMounted} from 'vue'
import useAuthStore from "@/stores/authStore.js";

const authStore = useAuthStore();

onMounted(async () => {
  await authStore.LoadMe();
})

</script>
<template>
  <div>
    <!-- <Header /> -->
    <RouterView/>
    <!-- <Footer /> -->
  </div>
</template>
