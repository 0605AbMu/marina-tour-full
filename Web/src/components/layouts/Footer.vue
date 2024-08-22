<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <div class="w-full p-10 bg-[#1A080A] flex flex-col justify-center items-center gap-y-5" id="blog">
    <div class="flex flex-row w-full gap-y-10 m-10 mb-2 justify-center flex-wrap gap-x-56">
      <div>
        <p class="text-2xl text-white">{{ $t('company') }}</p>
        <p class="text-gray-400 my-5">
          <i class="bi bi-geo-alt-fill"></i>&nbsp;TOSHKENT SHAHAR, UCHTEPA TUMAN,11-MAVZE,<br>ZARGARLIK 48-UY 50-XONADON
        </p>
        <p class="text-gray-400 my-5">
          <i class="bi bi-telephone-fill"></i>&nbsp;<a href="tel:+998970111482">+998970111482</a>
        </p>
        <p class="text-gray-400 my-5">
          <i class="bi bi-envelope-fill"></i>&nbsp;<a href="mailto:marinatraveluz@gmail.com">
          marinatraveluz@gmail.com</a
        >
        </p>
        <p class="text-gray-400 my-5">
          <i class="bi bi-calendar-date-fill"></i>&nbsp;9.00 dan - 20.00 gacha 24/7
        </p>
        <p class="text-gray-400 my-5">
          Rahbar: <i>Y.X.Yangiboyev</i>&nbsp;
        </p>
        <!-- <div class="bg-white">
          <a
            class="inline-block mr-0"
            href="https://www.facebook.com/share/ywaArfSx3oQaco3h/?mibextid=LQQJ4d"
          >
            <img class="w-[50px]" src="/images/facebook.png" alt="Facebook" />
          </a>
          <a
            class="inline-block"
            href="https://www.facebook.com/share/ywaArfSx3oQaco3h/?mibextid=LQQJ4d"
          >
            <img class="w-[50px]" src="/images/facebook.png" alt="Facebook" />
          </a>
        </div> -->
      </div>

      <div>
        <p class="text-white text-2xl mb-8">{{ $t('services') }}</p>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('popularPlaces') }}</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('pricing') }}</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('booking') }}</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('termsOfServices') }}</a>
      </div>

      <div>
        <p class="text-white text-2xl mb-8">{{ $t('support') }}</p>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('faq') }}</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('helpAndSupport') }}</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('blog') }}</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">{{ $t('news') }}</a>
      </div>

      <!-- <div>
        <p class="text-white text-2xl mb-8">Top Destination</p>
        <a class="text-gray-400 my-2 text-lg block" href="#">Indonesia</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">Switzerland</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">Singapore</a>
        <a class="text-gray-400 my-2 text-lg block" href="#">Bangkok</a>
      </div> -->
    </div>

    <div class="divider"></div>
    <p class="text-gray-400">
      Copyright © {{ new Date().getFullYear() }} Marina Travel, {{ $t('allRightsReserved') }}.
    </p>
  </div>
</template>

<script setup>
import EmailInput from './EmailInput.vue'
</script>

<style scoped>
.divider {
  width: 1146px;
  height: 0px;
  gap: 0px;
  border: 1px 0px 0px 0px;
  opacity: 0.4;
  border: 1px solid #ffffff;
}
</style>
