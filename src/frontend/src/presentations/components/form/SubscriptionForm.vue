<script setup lang="ts">
import { ref } from 'vue';
import Checkbox from './Checkbox.vue';

const newsletterSubscription = ref(false);
</script>

<template>
  <div class="subscription-section">
    <Checkbox
      v-model="newsletterSubscription"
      label="S’abonner à la newsletter Next Horizon Plus et recevoir les offres promotionnelles"
    />
  </div>
</template>

<style scoped>
.subscription-section {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-top: 20px;
}
</style>