<script setup lang="ts">
defineProps({
  backgroundImage: { type: String, required: true },
});
</script>

<template>
  <div class="background-section position-relative" :style="{ backgroundImage: `url(${backgroundImage})` }">
    <div class="overlay"></div> <!-- Calque d'assombrissement -->

    <div class="content position-absolute top-50 start-50 translate-middle">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped>
.background-section {
  height: 90vh;
  background-size: cover;
  background-position: center;
  position: relative;
}

.background-section .overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 1;
}

.background-section .content {
  width: 75%;
  z-index: 2;
}
</style>