<template>
  <el-upload
      v-model:file-list="fileList"
      :action="axiosInstance.defaults.baseURL + '/file'"
      list-type="picture-card"
      :on-preview="handlePictureCardPreview"
      :on-remove="handleRemove"
      :on-success="onSuccess"
      :accept="props.accept"
  >
    <el-icon>
      <Plus/>
    </el-icon>
  </el-upload>

  <el-dialog v-model="dialogVisible">
    <img w-full :src="dialogImageUrl" alt="Preview Image"/>
  </el-dialog>
</template>

<script lang="ts" setup>
import {computed, onMounted, ref, reactive, watch} from 'vue'
import {Plus} from '@element-plus/icons-vue'
import axiosInstance from '@/plugins/Axios';

import type {UploadProps, UploadUserFile} from 'element-plus'

const model = defineModel({
  type: String,
  required: true,
});

const props = defineProps({
  accept: {
    type: String,
    default: "*"
  }
})

const fileList = computed(() => {
  if (!model.value || model.value.length === 0)
    return [];

  if (model.value.indexOf(";") !== -1) {
    const files = model.value.split(";").filter(x => x.length > 0).map(x => ({url: axiosInstance.defaults.baseURL.replace("api", "uploads/") + x}));
    return files as UploadUserFile[];
  } else return [{
    url: (axiosInstance.defaults.baseURL.replace("api", "uploads/") + model.value),
    name: "image"
  }];
})

const dialogImageUrl = ref('')
const dialogVisible = ref(false)

const handleRemove: UploadProps['onRemove'] = (uploadFile, uploadFiles) => {
  const fileName = uploadFile.url.split("uploads/")[1];
  model.value = model.value.replace(fileName, "");
}


const onSuccess: UploadProps['onSuccess'] = (response, file, files) => {
  if (model.value.length === 0)
    model.value = response.content;
  else model.value = [model.value, response.content].join(";");
}

const handlePictureCardPreview: UploadProps['onPreview'] = (uploadFile) => {
  dialogImageUrl.value = uploadFile.url!
  dialogVisible.value = true
}


</script>