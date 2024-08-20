<script setup>
import {onMounted, reactive} from "vue";
import {vMaska} from "maska/vue";
import {orderService, OrderService} from "@/services/OrderService.ts";
import NotificationHelper from "@/tools/NotificationHelper.js";

const model = defineModel({
  type: Boolean,
  default: false
})

const props = defineProps({
  trip: {
    type: Object,
    required: true
  }
})

const bookModelInitial = {
  quantity: null,
  tripId: null,
};

const bookModel = reactive(bookModelInitial);


onMounted(() => {
})

const handleBookButtonClick = async () => {
  if (!bookModel.quantity || bookModel.quantity <= 0)
    return NotificationHelper.Warning("Yo'lovchilar soni 1 ta yoki undan ko'p bo'lishi kerak");

  await orderService.Add({...bookModel, quantity: Number.parseInt(bookModel.quantity)});

  await NotificationHelper.Success("Bron qilindi. Uning holatini kabinetingizdan ko'rishingiz mumkin.");

  model.value = false;

}

</script>

<template>
  <el-dialog v-model="model" width="500" @closed="() => bookModel.quantity = bookModelInitial"
             @open="() => bookModel.tripId = props.trip.id" class="!py-10">
    <el-alert type="success" :closable="false">
      Bu yerda siz sayohatingiz uchun so'rov qoldirishingiz mumkin. Va <b> Operatorlarimiz </b> siz bilan tez fursatda
      bog'lanadi.
    </el-alert>
    <div class="flex flex-col w-full gap-y-5 justify-center items-center mt-5">
      <div class="flex flex-col w-full">
        <div class="text-lg">Yo'lovchilar soni:</div>
        <el-input class="!w-full" size="large" :controls="false" v-model="bookModel.quantity"
                  v-maska="'##'" placeholder="00"></el-input>
      </div>
      <el-button size="large" type="primary" class="w-[50%]" @click="handleBookButtonClick">Bron qilish</el-button>
    </div>
  </el-dialog>
</template>

<style scoped>

</style>