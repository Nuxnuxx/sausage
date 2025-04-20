<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

defineProps({
  modelValue: {
    type: Boolean,
    required: true,
  },
  label: {
    type: String,
    required: true,
  },
  required: {
    type: Boolean,
    default: false,
  },
});

const emit = defineEmits(['update:modelValue']);

const updateValue = (event: Event) => {
  emit('update:modelValue', (event.target as HTMLInputElement).checked);
};
</script>

<template>
  <div class="checkbox-container">
    <input
      type="checkbox"
      :checked="modelValue"
      @change="updateValue"
      class="checkbox-input"
    />
    <label class="checkbox-label">
      {{ label }}
      <span v-if="required"> *</span>
    </label>
  </div>
</template>

<style scoped>
.checkbox-container {
  display: flex;
  align-items: center;
  gap: 10px;
}

.checkbox-input {
  width: 1.2em;
  height: 1.2em;
  cursor: pointer;
}

.checkbox-label {
  font-size: 18px;
  color: var(--couleur-noir);
}
</style>