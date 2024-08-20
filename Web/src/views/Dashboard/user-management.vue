<script setup>
import {onMounted, ref} from "vue";
import authService from "@/services/AuthService.ts";

const users = ref([]);
const total = ref(0);

onMounted(async () => {
  const res = await authService.GetAllUsers();
  users.value = res.content;
  total.value = res.total;
})

</script>

<template>
  <div class="flex flex-col gap-y-2 w-full mt-4 border-t-2 pt-4">
    <p>Barcha foydalanuvchilar soni: {{ total }}</p>
    <el-table :data="users" stripe>
      <el-table-column prop="id" label="ID"></el-table-column>
      <el-table-column prop="name" label="F.I.O"></el-table-column>
      <el-table-column prop="phoneNumber" label="Telefon"></el-table-column>
    </el-table>
  </div>
</template>

<style scoped>

</style>