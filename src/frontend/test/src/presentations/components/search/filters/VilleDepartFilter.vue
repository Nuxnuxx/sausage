<template>
  <div class="filter-container">
    <div class="filter-header">
      <i class="filter-icon bi bi-geo-fill"></i>
      <label for="departure-city" class="filter-label">
        Ville de départ
      </label>
    </div>
    <div class="filter-input">
      <input
        type="text"
        id="departure-city"
        class="form-control"
        :class="{ 'is-invalid': validationError }"
        v-model="departureCity"
        placeholder="Saisir ville de départ"
        @input="handleInput"
        @focus="handleFocus"
        @blur="handleBlur"
      />
      <div v-if="validationError" class="invalid-feedback d-block">
        {{ validationError }}
      </div>

      <div v-if="showSuggestions && filteredCities.length > 0" class="suggestions-dropdown">
        <ul class="suggestions-list">
          <li
            v-for="city in filteredCities"
            :key="city"
            @mousedown="selectCity(city)"
            class="suggestion-item"
          >
            {{ city }}
          </li>
        </ul>
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
const departureCity = ref(props.modelValue);
const validationError = ref('');
// Liste des villes françaises principales (à titre d'exemple)
const cities = ref([
  'Paris',
  'Lyon',
  'Marseille',
  'Toulouse',
  'Nice',
  'Nantes',
  'Strasbourg',
  'Montpellier',
  'Bordeaux',
  'Lille',
  'Rennes',
  'Reims'
]);
const filteredCities = ref<string[]>([]);
const showSuggestions = ref(false);
const handleInput = () => {
  filterCities();
  showSuggestions.value = true;
};
watch(() => props.modelValue, (newValue) => {
  departureCity.value = newValue;
});
watch(() => props.error, (newValue) => {
  validationError.value = newValue;
});
const filterCities = () => {
  if (!departureCity.value) {
    filteredCities.value = [...cities.value];
    return;
  }
  const query = departureCity.value.toLowerCase();
  filteredCities.value = cities.value.filter(
    city => city.toLowerCase().includes(query)
  );
};
const selectCity = (city: string) => {
  departureCity.value = city;
  validationError.value = '';
  emit('update:modelValue', city);
  showSuggestions.value = false;
};
const handleFocus = () => {
  filterCities();
  showSuggestions.value = true;
};
const handleBlur = () => {
  setTimeout(() => {
    showSuggestions.value = false;
  }, 200);
};
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
.suggestions-dropdown {
  position: absolute;
  top: 100%;
  left: 0;
  width: 100%;
  max-height: 200px;
  overflow-y: auto;
  background-color: var(--couleur-blanc, #FFFFFF);
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  z-index: 1000;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
.suggestions-list {
  list-style: none;
  padding: 0;
  margin: 0;
}
.suggestion-item {
  padding: 0.5rem 1rem;
  cursor: pointer;
  font-family: var(--font-lexend, sans-serif);
}
.suggestion-item:hover {
  background-color: var(--couleur-gris-clair, #EAEAEA);
}
.invalid-feedback {
  color: var(--couleur-rouge, #DC3545);
  font-size: 0.875rem;
  margin-top: 0.25rem;
}
</style>