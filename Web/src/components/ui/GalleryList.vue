<script setup>
const imagesData = [
  { img: '/images/galery1.png', title: 'Switzerland' },
  { img: '/images/galery2.png', title: 'Indonesia' },
  { img: '/images/galery3.png', title: 'Greece' },
  { img: '/images/galery4.png', title: 'India' },
  { img: '/images/galery5.png', title: 'Japan' }
]

function urlImage(url) {
  return new URL(url, import.meta.url).href
}

function galleryColumn(idx) {
  if (idx === 1 || idx === 2) {
    return 'col-span-3'
  } else {
    return 'col-span-2'
  }
}
</script>

<template>
  <div class="container mx-auto" id="gallery">
    <h3 class="text-4xl text-center">{{ $t('gallery.label') }}</h3>
    <p class="text-xl text-[#4D4949] mt-3 text-center">
      {{ $t('gallery.description') }}
    </p>
    <div class="grid grid-cols-6 gap-4 mt-8">
      <div
        class="relative group transition-all duration-500 ease-in-out"
        v-for="(item, idx) in imagesData"
        :key="item.img"
        :class="galleryColumn(idx + 1)"
      >
        <img
          :src="urlImage(item.img)"
          :alt="`${item.title} image`"
          class="w-full object-cover"
          @error="this.src = '/path/to/default-image.png'"
        />
        <div
          class="absolute top-0 w-full h-full flex justify-center items-center bg-[#00000099] opacity-0 transition-opacity duration-500 ease-in-out group-hover:opacity-100"
        >
          <h3 class="text-white text-xl">
            {{ item.title }}
          </h3>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.group:hover .group-hover\:opacity-100 {
  opacity: 1;
}
</style>
