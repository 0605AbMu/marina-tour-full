<script setup>
import { Swiper, SwiperSlide } from 'swiper/vue'
import { Pagination, Navigation, Autoplay } from 'swiper/modules'
import useTripStore from '../../stores/tripStore'
const tripStore = useTripStore()

const modules = [Pagination, Navigation, Autoplay]

const data = tripStore.trips

function urlImage(url) {
  return new URL(url, import.meta.url).href
}
</script>
<template>
  <div class="select-none scroll-smooth" id="trips">
    <swiper
      :slidesPerView="3"
      :spaceBetween="30"
      :loop="true"
      :pagination="{
        clickable: true
      }"
      :autoplay="{
        delay: 1500,
        disableOnInteraction: true
      }"
      :navigation="true"
      :modules="modules"
      class="mySwiper"
    >
      <swiper-slide
        class="border-2 rounded-xl transition-all duration-500 ease-in-out group hover:border-[#FF4D64] !h-[500px]"
        v-for="item in data"
        :key="item.url"
      >
        <div class="overflow-hidden relative rounded-t-xl cursor-pointer">
          <img
            :src="urlImage(item.img)"
            alt="not image"
            class="w-full object-cover transition duration-500 ease-in-out scale-110 group-hover:scale-125"
          />
          <div
            class="absolute top-0 right-0 left-0 h-full w-full bg-[#00000012] flex justify-end items-end p-4"
          >
            <span class="text-white border px-2 py-1 rounded">{{
              tripStore.formatPrice(item.price)
            }}</span>
          </div>
        </div>
        <div class="flex flex-col gap-4 px-4 py-8">
          <h3 class="text-[#1A080A] text-2xl font-semibold">{{ item.title }}</h3>
          <div class="flex items-center gap-4 text-[#4D4949]">
            <i class="bi bi-geo-alt-fill text-red-800 text-2xl"></i>
            <p class="text-xl">{{ item.region }}</p>
          </div>
          <div class="flex justify-between">
            <div class="flex gap-0.5 items-center">
              <el-rate
                v-model="item.star"
                disabled
                show-score
                text-color="#ff9900"
                :score-template="`{value} ${$t('point')}`"
              />
            </div>
            <router-link
              class="px-4 py-2 bg-[#FF4D64] rounded text-white"
              :to="`trip/${encodeURIComponent(item.title)}`"
            >
              {{ $t('viewDetails') }}
            </router-link>
          </div>
        </div>
      </swiper-slide>
    </swiper>
  </div>
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
