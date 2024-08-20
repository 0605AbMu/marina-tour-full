<script setup>
import {useRoute} from "vue-router";
import {Roles} from "@/stores/authStore.js";
import Authorize from "@/components/ui/Authorize.vue";

const route = useRoute();

</script>

<template>

  <div class="flex justify-center w-full bg-transparent">
    <div
        class="w-[70%] flex flex-row gap-x-4 bg-white p-10 px-5 m-10 rounded-xl border-gray-400  border-[.5px] border-opacity-35">
      <div class="h-full w-[20%]">
        <el-menu router unique-opened :default-active="route.path" ellipsis>
          <authorize :role="[Roles.Client, Roles.Admin]">
            <el-menu-item index="/dashboard">Profil</el-menu-item>
          </authorize>
          <authorize :role="[Roles.Client, Roles.Admin]">
            <el-menu-item index="/dashboard/my-orders">Mening buyurtmalarim</el-menu-item>
          </authorize>
          <authorize :role="Roles.Admin">
            <el-menu-item index="/dashboard/users">Foydalanuvchilar</el-menu-item>
            <el-menu-item index="/dashboard/trip-management">Sayohatlar boshqaruvi</el-menu-item>
          </authorize>
        </el-menu>
      </div>
      <div class="w-[80%] flex flex-col">
        <p class="text-2xl font-medium">{{ route.name }}</p>
        <div class="flex justify-center">
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>

</template>

<style scoped>

</style>