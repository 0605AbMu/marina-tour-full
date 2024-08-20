<script setup>

import {onMounted, reactive, ref, defineEmits} from "vue";
import UploadFile from "@/components/ui/UploadFile.vue";
import tripService from "@/services/TripService.ts";


const model = defineModel({
  type: Boolean,
  default: false,
});

const props = defineProps({
  tripId: {
    type: Number,
    required: true,
  }
})

let tripModel = reactive({
  name: {
    uz: '',
    ru: '',
    en: ''
  },
  country: {
    uz: '',
    ru: '',
    en: ''
  },
  description: {
    uz: '',
    ru: '',
    en: ''
  },
  price: null,
  discount: null,
  images: '',
  rank: 0
})

const emits = defineEmits(["onPersisted"])

const handleUpdate = async () => {
  await tripService.Update(tripModel);
  await emits("onPersisted");
  model.value = false;
}

onMounted(async () => {
  const res = await tripService.GetById(props.tripId);
  Object.assign(tripModel, {
    ...res.content,
    country: res.content.location, ...({description: res.content.description ? res.content.description : {}})
  });
})


</script>

<template>
  <el-dialog v-model="model" width="70%" class="!rounded-lg" lock-scroll>
    <div class="flex flex-col gap-y-4">
      <div class="flex flex-col gap-y-2">
        <p class="text-lg font-medium">Name</p>
        <div class="!flex !flex-row items-center gap-x-4">
          <el-input v-model="tripModel.name.uz" placeholder="name uz" size="large"></el-input>
          <el-input v-model="tripModel.name.en" placeholder="name en" size="large"></el-input>
          <el-input v-model="tripModel.name.ru" placeholder="name ru" size="large"></el-input>
        </div>
      </div>

      <div class="flex flex-col gap-y-2">
        <p class="text-lg font-medium">Country</p>
        <div class="!flex !flex-row items-center gap-x-4">
          <el-input v-model="tripModel.country.uz" placeholder="country uz" size="large"></el-input>
          <el-input v-model="tripModel.country.en" placeholder="country en" size="large"></el-input>
          <el-input v-model="tripModel.country.ru" placeholder="country ru" size="large"></el-input>
        </div>
      </div>

      <div class="flex flex-col gap-y-2">
        <p class="text-lg font-medium">Description</p>
        <div class="!flex !flex-row items-center gap-x-4">
          <el-input v-model="tripModel.description.uz" placeholder="description uz" type="textarea" rows="4"
                    size="large"></el-input>
          <el-input v-model="tripModel.description.en" placeholder="description en" type="textarea" rows="4"
                    size="large"></el-input>
          <el-input v-model="tripModel.description.ru" placeholder="description ru" type="textarea" rows="4"
                    size="large"></el-input>
        </div>
      </div>


      <div class="flex flex-col gap-y-2">
        <p class="text-lg font-medium">Prices</p>
        <div class="!flex !flex-row items-center gap-x-4">
          <div class="flex flex-col gap-y-1">
            <p>Price UZS</p>
            <el-input-number v-model="tripModel.price" :controls="false" size="large" min="1">
            </el-input-number>
          </div>
          <div class="flex flex-col gap-y-1">
            <p>Discount UZS</p>
            <el-input-number v-model="tripModel.discount" :controls="false" size="large" min="0">
              <template #prefix><span>Discount UZS</span></template>
            </el-input-number>
          </div>

          <div class="flex flex-col gap-y-1">
            <p>Rank</p>
            <el-rate v-model="tripModel.rank" show-text show-score size="large" allow-half></el-rate>
          </div>

        </div>
      </div>

      <div class="flex flex-col gap-y-2">
        <p class="text-lg font-medium">Images</p>
        <upload-file v-model="tripModel.images" accept="image/*"/>
      </div>

    </div>
    <div class="flex justify-center mt-5">
      <el-button type="primary" size="large" @click="handleUpdate">Update</el-button>
    </div>
  </el-dialog>
</template>

<style scoped>

</style>