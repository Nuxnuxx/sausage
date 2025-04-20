<script setup lang="ts">
  import DefaultLayout from "@/presentations/components/layout/DefaultLayout.vue";
</script>

<template>
  <DefaultLayout />
  <Notifications :pauseOnHover="true" :max="5" :ignoreDuplicates="true" width="25vw" :speed="200"/>
</template>