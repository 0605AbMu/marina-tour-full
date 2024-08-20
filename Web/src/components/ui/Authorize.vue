<script setup>
import {Roles, useAuthStore} from "@/stores/authStore.js";

const authStore = useAuthStore();

const props = defineProps({
  role: {
    type: String,
    default: Roles.Client
  }
});

const checkForRole = (roles) => {
  if (!authStore.userSignedIn)
    return false;

  if (!authStore.me)
    return false;


  if (Array.isArray(roles))
    return roles.indexOf(authStore.GetRole) !== -1;
  else return roles === authStore.GetRole;

}

</script>

<template>
  <div v-if="checkForRole(props.role)">
    <slot></slot>
  </div>
</template>