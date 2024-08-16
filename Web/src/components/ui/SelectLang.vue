<script setup>
import { computed, onBeforeUnmount, onMounted, ref } from 'vue'
import { useLangStore } from '@/stores/appStore'
//
const store = useLangStore()
//
console.log(store.languages)
//
const isHidden = ref(false)
const handleClick = (event) => {
  event.stopPropagation()
  isHidden.value = !isHidden.value
}
const windowClick = () => {
  if (isHidden.value) {
    isHidden.value = false
  }
}

const lngs = store.languages
const currentLangCode = localStorage.getItem('lang') ?? 'uz'

const current = computed(() => lngs.find((x) => x.code === currentLangCode)) ?? null
const others = computed(() => lngs.filter((x) => x.code !== currentLangCode))

onMounted(() => {
  window.addEventListener('click', windowClick)
})

onBeforeUnmount(() => {
  window.removeEventListener('click', windowClick)
})

const handleChangeLang = (code) => {
  localStorage.setItem('lang', code)
  window.location.reload()
}
</script>

<template>
  <div class="select-none">
    <button class="relative inline-flex text-left h-[50px] flex-row items-center">
      <div>
        <button
          @click="handleClick"
          type="button"
          class="inline-flex w-full justify-center gap-x-1.5 bg-white text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50 rounded-full p-0.5 border-2 border-red-500"
          id="menu-button"
          aria-expanded="true"
          aria-haspopup="true"
        >

          <img class="w-10" :src="`/images/flag-${current.code}.svg`" alt="Flag">

        </button>
      </div>

      <div
        :class="isHidden ? 'block' : 'hidden'"
        class="absolute top-14 w-[200px] right-0 z-10 mt-2 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none px-1"
        role="menu"
        aria-orientation="vertical"
        aria-labelledby="menu-button"
        tabindex="-1"
      >
        <div class="py-1 flex flex-col gap-1" role="none">
          <button
            v-for="lang in others"
            :key="lang.code"
            @click="handleChangeLang(lang.code)"
            class="text-gray-700 px-4 py-1 text-sm w-full rounded-md flex items-center gap-4 transition duration-300 hover:bg-[#eaeff3]"
          >
            <img class="w-10" :src="`/images/flag-${lang.code}.svg`" alt="Flag">
            <span class="text-lg font-semibold">{{ $t(lang.code) }}</span>
          </button>
        </div>
      </div>
    </button>
  </div>
</template>
