<script setup>
import SwiperCard from '../components/ui/SwiperCard.vue'
import GalleryList from '../components/ui/GalleryList.vue'
import EmailInput from "@/components/layouts/EmailInput.vue";
</script>
<template>
  <div class="bg-white py-4">
    <div class="container mx-auto relative">
      <img src="/images/bg.png" alt="" data-aos="fade-up-left"/>
      <div
          id="home"
          class="absolute w-1/2 h-full top-0 flex flex-col justify-center gap-16 scroll-smooth"
          data-aos="fade-right"
      >
        <h3 class="text-6xl font-semibold mt-20">
          {{ $t('headerText.1') }}<br/>{{ $t('headerText.2') }}<br/>{{ $t('headerText.3') }}
        </h3>
        <p class="text-2xl text-[#4D4949] w-2/3">
          {{ $t('headerDesc') }}
        </p>
        <div class="flex">
          <button class="py-1 px-6 rounded bg-[#FF4D64] text-white">{{ $t('getStarted') }}</button>
          <button class="py-1 px-6 rounded text-black flex items-center gap-2">
            <svg
                width="45"
                height="44"
                viewBox="0 0 45 44"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
            >
              <path
                  opacity="0.8"
                  d="M44.5 21.8231C44.5 33.5853 34.6648 43.1463 22.5 43.1463C10.3352 43.1463 0.5 33.5853 0.5 21.8231C0.5 10.061 10.3352 0.5 22.5 0.5C34.6648 0.5 44.5 10.061 44.5 21.8231Z"
                  stroke="#FF4D64"
              />
              <path
                  d="M33.2596 21.5912C33.5903 21.7842 33.5903 22.2621 33.2596 22.455L16.7519 32.0842C16.4186 32.2786 16 32.0382 16 31.6523L16 12.394C16 12.0081 16.4186 11.7676 16.7519 11.9621L33.2596 21.5912Z"
                  fill="#FF4D64"
              />
            </svg>
            <span>{{ $t('playVideo') }}</span>
          </button>
        </div>
      </div>
    </div>
    <div class="container mx-auto mt-20" data-aos="fade-up">
      <h3 class="text-4xl">{{ $t('trips.label') }}</h3>
      <p class="text-xl text-[#4D4949] mt-3">
        {{ $t('trips.description') }}
      </p>
      <SwiperCard class="mt-8"/>
    </div>
    <!-- Gallery -->
    <GalleryList class="mt-20" data-aos="fade-up"/>

    <!-- Location -->
    <div class="w-100% flex justify-center flex-col items-center my-28">
      <p class="text-4xl mb-10">{{ $t('ourLocation') }}</p>
      <iframe
          class="w-[80%]"
          src="https://www.google.com/maps/embed?pb=!1m13!1m8!1m3!1d5997.154381686351!2d69.190407!3d41.274541!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zNDHCsDE2JzI4LjQiTiA2OcKwMTEnMjUuNSJF!5e0!3m2!1suz!2s!4v1719936376916!5m2!1suz!2s"
          width="600"
          height="450"
          style="border: 0"
          allowfullscreen=""
          loading="lazy"
          referrerpolicy="no-referrer-when-downgrade"
      ></iframe>
    </div>
  </div>
  <div
      class="w-full bg-[#FF4D64] flex flex-row justify-center items-center bg-opacity-[10%] gap-x-56 h-[250px]"
  >
    <p class="text-4xl font-semibold">{{ $t('subsribeOurNewsLetter') }}</p>
    <EmailInput/>
  </div>
</template>
