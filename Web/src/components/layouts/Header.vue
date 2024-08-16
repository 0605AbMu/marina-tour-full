<!-- eslint-disable vue/multi-word-component-names -->
<script setup>
import {onMounted, ref, computed} from 'vue'
import i18n from '@/plugins/i18n'
import SelectLang from "../ui/SelectLang.vue"
import useAuthStore from '@/stores/authStore.js'
import SignInModal from "@/components/ui/SignInModal.vue";
import authService from "@/services/AuthService.ts";
import Get from "lodash-es/get.js";

const t = i18n.global.t

const authStore = useAuthStore()

const data = [
  {name: t('menu.home'), url: '/#home'},
  {name: t('menu.trips'), url: '/#trips'},
  // { name: 'Service', url: '#' },
  {name: t('menu.gallery'), url: '/#gallery'},
  {name: t('menu.blog'), url: '/#blog'}
]

const signModalOpen = ref(false);

const signOutBtnClickHandle = async () => {
  try {
    await authService.SignOut();
  } catch {
  }
  window.location.reload();
}

const GetLabel = computed(() => {
  if (authStore.userSignedIn && authStore.me)
    if (authStore.me?.name && authStore.me.name.length > 0)
      return authStore.me.name
    else return authStore.me.phoneNumber;
  else null;
})

onMounted(() => {
})
</script>
<template>
  <div class="border">
    <div class="container mx-auto py-4 flex justify-between items-center">
      <router-link to="/">
        <img class="w-20 h-20" src="/images/brand.jpg" alt=""/>
      </router-link>
      <div class="flex gap-12 items-center">
        <a
            v-for="item in data"
            :key="item.name"
            :href="item.url"
            class="text-xl font-sans transition duration-300 ease-out text-[#4D4949] hover:text-red-500"
        >{{ item.name }}</a
        >
      </div>
      <div class="flex flex-row gap-x-4 justify-center items-center">
        <SelectLang/>
        <button v-if="!authStore.userSignedIn" @click="() => signModalOpen = !signModalOpen"
                class="py-1 px-6 rounded bg-[#FF4D64] text-white">{{ $t('login') }}
        </button>
        <template v-else>
          <el-button round icon="SwitchButton" size="large" class="!bg-red-500 !text-white"
                     @click="signOutBtnClickHandle">
            <template v-if="GetLabel">{{ GetLabel }}</template>
          </el-button>
        </template>
        <sign-in-modal v-model="signModalOpen"/>
      </div>

    </div>
  </div>
</template>
