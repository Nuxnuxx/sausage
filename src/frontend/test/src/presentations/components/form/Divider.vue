<template>
  <div class="divider-container">
    <div class="divider-line"></div>
    <div class="divider-text">{{ text }}</div>
    <div class="divider-line"></div>
  </div>
</template>

<script setup lang="ts">
defineProps({
  text: {
    type: String,
    default: 'OU'
  }
});
</script>

<style scoped>
.divider-container {
  display: flex;
  align-items: center;
  width: 100%;
  margin: 25px 0;
}

.divider-line {
  flex-grow: 1;
  height: 1px;
  background-color: var(--couleur-gris-fonce);
}

.divider-text {
  padding: 0 16px;
  font-family: var(--font-lexend, sans-serif);
  font-size: 14px;
  font-weight: 500;
  color: var(--couleur-noir, #666);
  text-transform: uppercase;
}
</style>
