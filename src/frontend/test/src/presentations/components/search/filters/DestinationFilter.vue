<template>
  <div class="filter-container">
    <div class="filter-header">
      <i class="filter-icon bi bi-geo-alt"></i>
      <label for="destination" class="filter-label">
        Destination
      </label>
    </div>
    <div class="filter-input">
      <select
        id="destination"
        class="form-select"
        :class="{ 'is-invalid': validationError }"
        v-model="destination"
        @change="handleChange"
        :disabled="isLoading"
      >
        <option value="" disabled selected>{{ isLoading ? 'Chargement...' : 'Sélectionner une destination' }}</option>
        <option v-for="type in destinationTypes" :key="type.id" :value="type.nomTypeDestination">
          {{ type.nomTypeDestination }}
        </option>
      </select>
      <div v-if="isLoading" class="loading-indicator">
        <div class="spinner-border spinner-border-sm text-primary" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
      <div v-if="validationError" class="invalid-feedback d-block">
        {{ validationError }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted } from 'vue';
import { TypeDestinationDAO } from '@/domain/daos/TypeDestinationDAO';
import { TypeDestination } from '@/domain/entities/TypeDestination';

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

const destination = ref(props.modelValue);
const validationError = ref('');
const destinationTypes = ref<TypeDestination[]>([]);
const isLoading = ref(false);

const handleChange = () => {
  emit('update:modelValue', destination.value);
  validationError.value = '';
};

watch(() => props.modelValue, (newValue) => {
  destination.value = newValue;
});

watch(() => props.error, (newValue) => {
  validationError.value = newValue;
});

onMounted(async () => {
  try {
    isLoading.value = true;
    const typeDestinationDAO = TypeDestinationDAO.getInstance();
    destinationTypes.value = await typeDestinationDAO.list();
  } catch (error) {
    console.error('Failed to load destination types:', error);
    validationError.value = 'Impossible de charger les destinations';
  } finally {
    isLoading.value = false;
  }
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
  font-family: var(--font-lexend, 'Lexend', sans-serif);
  font-weight: 500;
  font-size: 16px;
  color: var(--couleur-non-interactifs, #404040);
}
.filter-input {
  width: 100%;
  position: relative;
}
.loading-indicator {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  z-index: 1;
}
.invalid-feedback {
  color: var(--couleur-rouge, #DC3545);
  font-size: 0.875rem;
  margin-top: 0.25rem;
}
</style>