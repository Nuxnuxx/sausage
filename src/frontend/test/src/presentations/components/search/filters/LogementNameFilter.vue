<template>
  <div class="filter-container">
    <div class="filter-header">
      <i class="filter-icon bi bi-house"></i>
      <label for="logement-name" class="filter-label">
        Nom du logement
      </label>
    </div>
    <div class="filter-input">
      <input
        type="text"
        id="logement-name"
        class="form-control"
        :class="{ 'is-invalid': validationError }"
        v-model="logementName"
        placeholder="Rechercher par nom"
        @input="handleInput"
      />
      <div v-if="validationError" class="invalid-feedback d-block">
        {{ validationError }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';

const props = defineProps({
  modelValue: {
    type: String,
    default: ''
  },
  error: {
    type: String,
    default: ''
  }
});

const emit = defineEmits<{
  'update:modelValue': [value: string];
}>();

const logementName = ref(props.modelValue);
const validationError = ref('');

const handleInput = () => {
  emit('update:modelValue', logementName.value);
  validationError.value = '';
};

watch(() => props.modelValue, (newValue) => {
  logementName.value = newValue;
});

watch(() => props.error, (newValue) => {
  validationError.value = newValue;
});
</script>

<style scoped>
.filter-container {
  display: flex;
  flex-direction: column;
  gap: 8px;
  position: relative;
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
  color: var(--couleur-non-interactifs, #404040);
}

.filter-input {
  width: 100%;
  position: relative;
}

.invalid-feedback {
  color: var(--couleur-rouge, #DC3545);
  font-size: 0.875rem;
  margin-top: 0.25rem;
}
</style>