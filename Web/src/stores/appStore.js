import { defineStore } from 'pinia'
import { ref } from 'vue'


export const useLangStore = defineStore('app-store', () => {
  const languages = ref([
    {
      code: 'ru'
    },
    {
      code: 'uz'
    },
    {
      code: 'en'
    }
  ])

  return {
    languages
  }
})
