<script setup lang="ts">
defineProps<{
  backgroundImage: string;
  title: string;
}>();
</script>

<template>
  <div class="title-container" :style="{ 'backgroundImage': `url(${backgroundImage})` }">
    <h1 class="title">{{ title }}</h1>
  </div>
</template>
  
<style scoped>

.title-container {
  width: 100%;
  height: 250px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-size: cover;
  background-position: center;
  position: relative;
  overflow: hidden; 
}

.title-container::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: var(--couleur-blanc);
  opacity: 0.4;
}

.title {
  position: relative;
  color: var(--couleur-bleu-fonce);
  text-align: center;
  font-size: 4vw;
  font-style: normal;
  font-weight: 600;
  line-height: 48px;
}
</style>
