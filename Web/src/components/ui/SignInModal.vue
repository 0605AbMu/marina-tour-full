<script setup>

import {reactive, ref} from "vue";
import authService from "@/services/AuthService.ts";
import {vMaska} from "maska/vue";
import NotificationHelper from "@/tools/NotificationHelper.js";

const signInModelInit = {
  phoneNumber: "",
  otp: "777777",
  name: "",
  key: null
};
const signModel = reactive(signInModelInit);

const otpSent = ref(false);

const hasSignUp = ref(false);

const onSignClick = async () => {
  const res = await authService.SignIn(signModel.name, signModel.phoneNumber.replaceAll(" ", ""));
  signModel.key = res.content;
  otpSent.value = true;
}

const onSignUpClick = async () => {
  if (!hasSignUp.value)
    hasSignUp.value = true;

  if (!signModel.name || signModel.name.length === 0) {
    return NotificationHelper.Warning("Username kiritish majburiy");
  }

  await onSignClick();
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
      <p class="text-2xl font-semibold text-center my-2 mb-4">{{ $t('login') }}</p>
      <el-input v-model="signModel.phoneNumber" size="large" :placeholder="$t('phoneNumber')"
                :disabled="otpSent" v-maska="'## ### ## ##'">
        <template #prefix>
          <span>+998</span>
        </template>
      </el-input>
      <el-input v-if="hasSignUp" v-model="signModel.name" size="large" :placeholder="$t('userName')"
                :disabled="otpSent"></el-input>
      <el-input v-if="otpSent" v-model="signModel.otp" size="large" :placeholder="$t('otp')"
                v-maska="'# # # # # #'" :disabled="true"></el-input>
      <div class="flex justify-center flex-row">
        <el-button size="large" v-if="!otpSent" @click="onSignUpClick">{{ $t('signUp') }}</el-button>
        <el-button size="large" @click="onVerifyClick" v-if="otpSent" type="primary">Verify</el-button>
        <el-button size="large" type="primary" @click="onSignClick" v-if="!otpSent && !hasSignUp">{{ $t('signIn') }}
        </el-button>
      </div>
    </div>

  </el-dialog>
</template>

<style scoped>

</style>