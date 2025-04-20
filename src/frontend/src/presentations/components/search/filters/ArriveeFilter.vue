<template>
  <div class="filter-container">
    <div class="filter-header">
      <i class="filter-icon bi bi-calendar-event"></i>
      <label for="arrival-date" class="filter-label">Arrivée</label>
    </div>
    <div class="filter-input">
      <input
        type="date"
        id="arrival-date"
        class="form-control"
        :class="{ 'is-invalid': error }"
        v-model="arrivalDate"
        placeholder="Saisir date"
      />
      <div v-if="error" class="invalid-feedback d-block">
        {{ error }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
const props = defineProps<{
  modelValue: string;
  error?: string;
}>();
const emit = defineEmits<{
  'update:modelValue': [value: string];
}>();
const arrivalDate = ref(props.modelValue);
watch(arrivalDate, (newValue) => {
  emit('update:modelValue', newValue);
});
watch(() => props.modelValue, (newValue) => {
  arrivalDate.value = newValue;
});
</script>

<style scoped>
.filter-container {
  display: flex;
  flex-direction: column;
  gap: 8px;
}
.filter-header {
  display: flex;
  align-items: center;
  gap: 8px;
}
.filter-icon {
  font-size: 20px;
  color: var(--couleur-principal, #1C61AD);
}
.filter-label {
  font-family: var(--font-lexend, sans-serif);
  font-weight: 500;
  font-size: 16px;
  color: #333;
}
.filter-input {
  width: 100%;
}
</style>