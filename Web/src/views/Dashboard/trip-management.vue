<script setup>

import {onMounted, ref} from "vue";
import tripService from "@/services/TripService.ts";
import AddTripModal from "@/components/ui/Trips/AddTripModal.vue";
import UpdateTripModal from "@/components/ui/Trips/UpdateTripModal.vue";


const trips = ref([]);
const total = ref(0);

const LoadTrips = async () => {
  const res = await tripService.GetAll();
  trips.value = res.content;
  total.value = res.total;
}

onMounted(async () => {
  await LoadTrips();
})

const updatableTripId = ref(null);

const openAddTripModal = ref(false);
const openEditModal = ref(false);

</script>

<template>
  <div class="flex flex-col gap-y-2 w-full mt-4 border-t-2 pt-4">
    <p>Barcha sayohatlar soni: {{ total }}</p>
    <div class="flex w-full flex-col">
      <div class="flex justify-end">
        <el-button icon="plus" type="primary" @click="openAddTripModal = !openAddTripModal"></el-button>
      </div>
      <el-table :data="trips" stripe>
        <el-table-column prop="id" label="ID"></el-table-column>
        <el-table-column prop="name" label="Name">
          <template #default="scope">
            {{ $format(scope.row.name) }}
          </template>
        </el-table-column>
        <el-table-column prop="location" label="Country">
          <template #default="scope">
            {{ $format(scope.row.location) }}
          </template>
        </el-table-column>
        <el-table-column prop="price" label="Price"></el-table-column>
        <el-table-column prop="discount" label="Discount"></el-table-column>
        <el-table-column label="Actions">
          <template #default="scope">
            <el-button-group>
              <el-button icon="Edit" @click="() => {updatableTripId = scope.row.id; openEditModal = true;}"></el-button>
            </el-button-group>
          </template>
        </el-table-column>
      </el-table>
    </div>

  </div>
  <add-trip-modal v-model="openAddTripModal" @on-persisted="LoadTrips"></add-trip-modal>
  <update-trip-modal :trip-id="updatableTripId" v-if="openEditModal" v-model="openEditModal" @on-persisted="LoadTrips"/>
</template>

<style scoped>

</style>