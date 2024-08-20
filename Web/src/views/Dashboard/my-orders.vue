<script setup>

import {onMounted, ref} from "vue";
import orderService from "@/services/OrderService.ts";
import tripStore from "../../stores/tripStore.js";

const orders = ref([]);
const totalOrders = ref(0);

onMounted(async () => {
  const {content, total} = await orderService.GetAll()
  orders.value = content;
  totalOrders.value = total;
})

</script>

<template>
  <div class="flex flex-col gap-y-2 w-full mt-4 border-t-2 pt-4">
    <p>Barcha buyurtmalar soni: {{ totalOrders }}</p>
    <div class="flex w-full flex-col">
      <el-table :data="orders" stripe>
        <el-table-column prop="id" label="ID"></el-table-column>
        <el-table-column prop="trip" label="Sayohat">
          <template #default="scope">
            {{ $format(scope.row.trip.name) }}
          </template>
        </el-table-column>

        <el-table-column prop="price" label="Price">
          <template #default="scope">
            {{ $formatMoney(scope.row.trip.price) }}
          </template>
        </el-table-column>

        <el-table-column label="Discount">
          <template #default="scope">
            {{ $formatMoney(scope.row.trip.discount) }}
          </template>
        </el-table-column>

        <el-table-column prop="quantity" label="Soni">
        </el-table-column>


        <el-table-column label="Jami">
          <template #default="scope">
            {{ $formatMoney(scope.row.fee) }}
          </template>
        </el-table-column>
      </el-table>
    </div>

  </div>
</template>

<style scoped>

</style>