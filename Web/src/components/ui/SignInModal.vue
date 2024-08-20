<script setup>

import {reactive, ref} from "vue";
import authService from "@/services/AuthService.ts";
import {vMaska} from "maska/vue";

const signInModelInit = {
  phoneNumber: "",
  otp: "",
  key: null
};
const signModel = reactive(signInModelInit);

const otpSent = ref(false);

const onSignClick = async () => {
  const res = await authService.SignIn(null, signModel.phoneNumber.replaceAll(" ", ""));
  signModel.key = res.content;
  otpSent.value = true;
}

const onVerifyClick = async () => {
  await authService.Verify(signModel.key, signModel.otp.replaceAll(" ", ""));
  window.location.reload();
}


const model = defineModel({
  type: Boolean,
  required: true
})

</script>

<template>
  <el-dialog class="cursor-pointer" lock-scroll show-close width="30%" v-model="model">
    <div class="w-full flex flex-col gap-y-3 p-10 justify-center">
      <p class="text-2xl font-semibold text-center my-2 mb-4">Tizimga kirish</p>
      <el-input v-model="signModel.phoneNumber" size="large" placeholder="Telefon raqam"
                :disabled="otpSent" v-maska="'## ### ## ##'">
        <template #prefix>
          <span>+998</span>
        </template>
      </el-input>
      <el-input v-if="otpSent" v-model="signModel.otp" size="large" placeholder="OTP"
                v-maska="'# # # # # #'"></el-input>
      <div class="flex justify-center flex-row">
        <el-button size="large" v-if="!otpSent">Sign up</el-button>
        <el-button size="large" @click="onVerifyClick" v-if="otpSent" type="primary">Verify</el-button>
        <el-button size="large" type="primary" @click="onSignClick" v-if="!otpSent">Sign in</el-button>
      </div>
    </div>

  </el-dialog>
</template>

<style scoped>

</style>