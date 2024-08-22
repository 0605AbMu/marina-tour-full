<script setup>

import {onMounted, ref} from "vue";
import orderService from "@/services/OrderService.ts";
import tripStore from "../../stores/tripStore.js";
import {v4 as uuidv4} from "uuid";

const orders = ref([]);
const totalOrders = ref(0);

onMounted(async () => {
  const {content, total} = await orderService.GetAll()
  orders.value = content;
  totalOrders.value = total;
})

const pay = (amount) => {
  const params = new URLSearchParams()
  params.append('mearchant_id', 26277)
  params.append('merchant_user_id', 41787)
  params.append('service_id', 34302)
  params.append('transaction_param', uuidv4())
  params.append('amount', amount)
  params.append('return_url', window.location.href)

  const payUrl = `https://my.click.uz/services/pay?${params.toString()}`
  window.open(payUrl);
}

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
            <button
                border
                class="click_logo !text-white rounded-lg  h-[50px] flex justify-center gap-x-2 items-center disabled:!bg-opacity-50 cursor-pointer !disabled:cursor-not-allowed"
                @click="pay(scope.row.fee)"
            >
              <i class=""></i>
              <span>{{ $formatMoney(scope.row.fee) }}</span>
              <!--              <i>{{ $t('payViaClick') }}</i>
              -->
            </button>
          </template>
        </el-table-column>
      </el-table>
    </div>

  </div>
</template>

<style scoped>
.click_logo {
  padding: 4px 10px;
  cursor: pointer;
  color: #fff;
  line-height: 190%;
  font-size: 13px;
  font-weight: bold;
  text-align: center;
  border: 1px solid #037bc8;
  text-shadow: 0px -1px 0px #037bc8;
  border-radius: 4px;
  background: #27a8e0;
  background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzI3YThlMCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMxYzhlZDciIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
  background: -webkit-gradient(linear, 0 0, 0 100%, from(#27a8e0), to(#1c8ed7));
  background: -webkit-linear-gradient(#27a8e0 0%, #1c8ed7 100%);
  background: -moz-linear-gradient(#27a8e0 0%, #1c8ed7 100%);
  background: -o-linear-gradient(#27a8e0 0%, #1c8ed7 100%);
  background: linear-gradient(#27a8e0 0%, #1c8ed7 100%);
  box-shadow: inset 0px 1px 0px #45c4fc;
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#27a8e0', endColorstr='#1c8ed7', GradientType=0);
  -webkit-box-shadow: inset 0px 1px 0px #45c4fc;
  -moz-box-shadow: inset 0px 1px 0px #45c4fc;
  -webkit-border-radius: 4px;
  -moz-border-radius: 4px;
}

.click_logo-disabled {
  filter: opacity(0.4);
  cursor: not-allowed;
}

.click_logo i {
  background: url(https://m.click.uz/static/img/logo.png) no-repeat top left;
  width: 30px;
  height: 25px;
  display: block;
  float: left;
}
</style>