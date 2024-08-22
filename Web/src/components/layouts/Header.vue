<!-- eslint-disable vue/multi-word-component-names -->
<script setup>
import {onMounted, ref, computed} from 'vue'
import i18n from '@/plugins/i18n'
import SelectLang from "../ui/SelectLang.vue"
import useAuthStore, {Roles} from '@/stores/authStore.js'
import SignInModal from "@/components/ui/SignInModal.vue";
import authService from "@/services/AuthService.ts";
import Get from "lodash-es/get.js";
import {useRoute} from "vue-router";
import {CloseBold, RemoveFilled} from "@element-plus/icons-vue";
import Authorize from "@/components/ui/Authorize.vue";

const t = i18n.global.t

const authStore = useAuthStore();
const route = useRoute();


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
  if (!authStore.userSignedIn && route.hash.indexOf("#login") !== -1)
    signModalOpen.value = true;

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
                class="py-1 px-6 rounded bg-[#FF4D64] text-white">{{ $t('signIn') }}
        </button>
        <template v-else>
          <el-dropdown>
            <el-button size="large" class="!rounded-xl !bg-[#FF4D64] !text-white">
              {{ GetLabel }}
              <el-icon class="el-icon--right">
                <arrow-down/>
              </el-icon>
            </el-button>
            <template #dropdown>
              <el-dropdown-menu>

                <authorize :role="[Roles.Client, Roles.Admin]">
                  <router-link to="/dashboard">
                    <el-dropdown-item>
                      {{ $t('profile')}}
                    </el-dropdown-item>
                  </router-link>
                </authorize>

                <authorize :role="[Roles.Client, Roles.Admin]">
                  <router-link to="/dashboard/my-orders">
                    <el-dropdown-item>{{ $t('myOrders') }}</el-dropdown-item>
                  </router-link>
                </authorize>

                <authorize :role="Roles.Admin">
                  <router-link to="/dashboard/users">
                    <el-dropdown-item>{{ $t('users') }}</el-dropdown-item>
                  </router-link>
                </authorize>

                <authorize :role="Roles.Admin">
                  <router-link to="/dashboard/trip-management">
                    <el-dropdown-item>{{ $t('tripManagement')}}</el-dropdown-item>
                  </router-link>
                </authorize>

                <el-dropdown-item @click="signOutBtnClickHandle" divided>
                  <el-icon color="rgb(185 28 28)">
                    <RemoveFilled/>
                  </el-icon>
                  <span class="text-red-700 font-bold">{{ $t('signOut')}}</span>
                </el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </template>
        <sign-in-modal v-model="signModalOpen"/>
      </div>

    </div>
  </div>
</template>
