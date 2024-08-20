<script setup>
import {Autoplay, Navigation, Pagination} from 'swiper/modules'
import useTripStore from '../../stores/tripStore'
import {onMounted, ref} from "vue";
import div from "element-plus/es/components/divider/src/divider2";
import {FileApiBaseUrl} from "@/plugins/Axios.js";
import BookingModal from "@/components/ui/BookingModal.vue";
import useAuthStore from "@/stores/authStore.js";
import {useRouter} from "vue-router";

const tripStore = useTripStore()

const modules = [Pagination, Navigation, Autoplay];
const authStore = useAuthStore();

const data = ref([]);

onMounted(async () => {
  data.value = (await tripStore.GetTrips()).value;
})

const bookDialogOpen = ref(false);
const selectedTrip = ref({});


const router = useRouter();

const bookATrip = (trip) => {
  if (authStore.userSignedIn) {
    selectedTrip.value = trip;
    bookDialogOpen.value = true;
  } else {
    window.location.hash = "#login";
    window.location.reload();
  }
}

</script>
<template>
  <div class="select-none scroll-smooth">
    <div class="flex flex-wrap flex-row justify-center gap-x-5 gap-y-5 ">
      <div
          v-if="data && data.length > 0"
          class="border-2 rounded-xl transition-all duration-500 ease-in-out group hover:border-[#FF4D64] !h-[500px] !w-[400px] !flex flex-col justify-between shadow-lg overflow-hidden"
          v-for="item in data"
          :key="item.id"
      >
        <div class="h-[90%]">
          <div class="h-[275px] overflow-hidden">
            <img class="w-full hover:scale-125 duration-500 transition-all h-full z-10"
                 :src="`${FileApiBaseUrl}/uploads/${item.img}`"
                 :alt="item.title">
            <div class="relative bottom-[70px] left-0 z-30 flex flex-row justify-end">
              <p class="bg-black bg-opacity-[70%] text-white p-3 m-4 py-2 mx-2 rounded-md">
                {{ tripStore.formatPrice(item.price) }}
              </p>
            </div>
          </div>
          <div class="mt-2">
            <div class="flex flex-row justify-between px-2">
              <div>
                <p class="text-2xl font-semibold ">{{ $format(item.title) }}</p>
                <p>{{ $format(item.region) }}</p>
              </div>
              <div class="pr-2 h-full">
                <el-rate v-model="item.star" size="large" allow-half show-score disabled></el-rate>
              </div>

            </div>
            <div class="px-4 py-2">
              <el-text size="default" line-clamp="3">{{ $format(item.description) }}</el-text>
            </div>
          </div>
        </div>
        <div class="h-[10%] flex flex-row justify-end items-center">
          <button class="py-3 p-6 rounded bg-[#FF4D64] text-white mr-2 mb-2" @click="bookATrip(item)">
            {{ $t('viewDetails') }}
          </button>
        </div>
      </div>
    </div>
  </div>
  <booking-modal v-model="bookDialogOpen" :trip="selectedTrip"/>
</template>
<style>
.zoom {
  transform: scale(1.2);
}

.autoplay-progress {
  position: absolute;
  right: 16px;
  bottom: 16px;
  z-index: 10;
  width: 48px;
  height: 48px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  color: var(--swiper-theme-color);
}

.autoplay-progress svg {
  --progress: 0;
  position: absolute;
  left: 0;
  top: 0px;
  z-index: 10;
  width: 100%;
  height: 100%;
  stroke-width: 4px;
  stroke: var(--swiper-theme-color);
  fill: none;
  stroke-dashoffset: calc(125.6px * (1 - var(--progress)));
  stroke-dasharray: 125.6;
  transform: rotate(-90deg);
}
</style>
