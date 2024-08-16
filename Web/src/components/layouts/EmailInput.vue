<!-- eslint-disable vue/no-mutating-props -->
<template>
  <div
    class="flex flex-row bg-white py-2 px-2 pl-4 m-5 rounded-lg w-[500px] gap-x-4 justify-between items-center"
    style="border: 1px solid #00000033"
  >
    <i class="bi bi-envelope text-3xl text-gray-600"></i>
    <input
      type="email"
      class="border-none outline-none w-[70%]"
      :placeholder="props.placeHolder"
      v-model="props.modelValue"
    />
    <button
      class="text-white rounded-lg bg-[#ff4d64] w-[129px] h-[50px] flex justify-center gap-x-2 items-center"
    >
      <span> {{ $t('subscribe') }} </span> <i class="bi bi-send-fill"></i>
    </button>
  </div>
</template>

<script setup>
import i18n from '@/plugins/i18n'
import { defineProps } from 'vue'

const props = defineProps({
  placeHolder: {
    type: String,
    default: i18n.global.t('typeAnEmail')
  },
  modelValue: {
    type: String,
    default: ''
  }
})
</script>

<style scoped></style>
