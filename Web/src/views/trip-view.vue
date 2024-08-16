<template>
  <div class="w-full flex justify-center items-center">
    <div
      class="w-[1000px] flex flex-row my-[100px] border-gray-400 border-opacity-50 border p-10 rounded-xl pl-0 pr-0 py-0 h-[600px] overflow-hidden"
    >
      <div class="w-[50%] overflow-hidden cursor-pointer">
        <img
          :src="urlImage(trip.img)"
          alt=""
          class="w-full h-full object-cover transition duration-500 ease-in-out hover:scale-125 hover:bg-gray-700 hover:bg-opacity-30"
        />
      </div>
      <div class="w-[50%] flex flex-col">
        <div class="flex flex-col gap-y-3 h-[90%]">
          <div class="text-center text-2xl my-10 font-semibold">
            <p>{{ trip.title }}</p>
          </div>
          <div class="flex flex-row justify-between px-5 items-center font-semibold">
            <div>
              <i class="bi bi-geo-alt-fill text-red-800 text-xl"></i>
              <p class="inline-block text-xl">{{ trip.region }}</p>
            </div>
            <el-rate
              v-model="trip.star"
              disabled
              show-score
              text-color="#ff9900"
              score-template="{value} points"
              size="large"
            />
          </div>
          <div class="flex flex-row justify-between px-5">
            <p>{{ $t('amount') }}:</p>
            <p class="font-semibold">{{ tripStore.formatPrice(trip.price) }}</p>
          </div>
          <div class="flex flex-row justify-between px-5">
            <p>{{ $t('quantity') }}:</p>
            <el-input-number v-model="model.count" :min="0" :max="10" @change="handleChange" />
          </div>

          <div class="flex flex-row justify-between px-5">
            <p>{{ $t('total') }}:</p>
            <p class="font-semibold">{{ tripStore.formatPrice(trip.price * model.count) }}</p>
          </div>
        </div>
        <div class="flex flex-row justify-center">
          <button
            :disabled="model.count == 0"
            :class="{ 'click_logo-disabled': model.count == 0 }"
            value="click"
            size="large"
            border
            class="click_logo !text-white rounded-lg w-[70%] h-[50px] flex justify-center gap-x-2 items-center disabled:!bg-opacity-50 cursor-pointer !disabled:cursor-not-allowed"
            @click="pay(model.count * trip.price)"
          >
            <i></i>{{ $t('payViaClick') }}
          </button>
          <!-- <button
            class="text-white rounded-lg bg-[#ff4d64] w-[70%] h-[50px] flex justify-center gap-x-2 items-center disabled:bg-opacity-50 cursor-pointer disabled:cursor-not-allowed"
            :disabled="model.count == 0"
            @click="pay(model.count * trip.price)"
          >
            <span>Checkout</span>
            <i class="bi bi-bag-check-fill"></i>
          </button> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router'
import useTripStore from '../stores/tripStore'
import { computed, reactive } from 'vue'
import { v4 as uuidv4 } from 'uuid'

const route = useRoute()
const router = useRouter()
const tripStore = useTripStore()

const slug = computed(() => route.params.slug)

if (slug.value === undefined) router.back()

const trip = computed(() => tripStore.trips.find((x) => x.title == slug.value))

if (trip.value === undefined) router.back()

function urlImage(url) {
  return new URL(url, import.meta.url).href
}

const model = reactive({
  count: 0,
  method: 'click'
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
  window.location.href = payUrl
}
</script>

<style scoped>
.click_logo {
  padding: 4px 10px;
  cursor: pointer;
  color: #fff;
  line-height: 190%;
  font-size: 13px;
  font-family: Arial;
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
  filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#27a8e0', endColorstr='#1c8ed7',GradientType=0 );
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
