<template>
  <div class="filter-container">
    <div class="filter-header">
      <i class="filter-icon bi bi-people"></i>
      <label class="filter-label">Participants</label>
    </div>
    <div class="filter-input">
      <div class="participants-control">
        <button
          type="button"
          class="btn-control"
          @click="decrement"
          :disabled="participantsCount <= 1"
        >
          <i class="bi bi-dash"></i>
        </button>
        <div class="participants-display">
          {{ participantsText }}
        </div>
        <button
          type="button"
          class="btn-control"
          @click="increment"
          :disabled="participantsCount >= 10"
        >
          <i class="bi bi-plus"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
//INIT
import { ref, watch, computed } from 'vue';
const props = defineProps<{
  modelValue: number;
}>();
const emit = defineEmits<{
  'update:modelValue': [value: number];
}>();
const participantsCount = ref(props.modelValue);
watch(participantsCount, (newValue) => {
  emit('update:modelValue', newValue);
});
watch(() => props.modelValue, (newValue) => {
  participantsCount.value = newValue;
});
const increment = () => {
  if (participantsCount.value < 10) {
    participantsCount.value++;
  }
};
const decrement = () => {
  if (participantsCount.value > 1) {
    participantsCount.value--;
  }
};
const participantsText = computed(() => {
  return participantsCount.value === 1
    ? '1 participant'
    : `${participantsCount.value} participants`;
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
.participants-control {
  display: flex;
  align-items: center;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  overflow: hidden;
  height: 38px;
}
.btn-control {
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #f8f9fa;
  border: none;
  width: 38px;
  height: 100%;
  cursor: pointer;
  transition: background-color 0.2s;
}
.btn-control:hover:not(:disabled) {
  background-color: #e9ecef;
}
.btn-control:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}
.participants-display {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0 0.5rem;
  font-weight: 500;
}
</style>