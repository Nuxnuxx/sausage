<script setup lang="ts">
import CustomInput from './../form/InputField.vue';

defineProps({
  id: {
    type: String,
    default: () => `input-${Math.random().toString(36).substr(2, 9)}`,
  },
  label: {
    type: String,
    default: '',
  },
  modelValue: {
    type: [String, Number],
    required: true,
  },
  placeholder: {
    type: String,
    default: '',
  },
  type: {
    type: String,
    default: 'text',
  },
  required: {
    type: Boolean,
    default: false,
  },
});

const emit = defineEmits(['update:modelValue']);

const updateValue = (value: string | number) => {
  emit('update:modelValue', value);
};
</script>

<template>
  <div class="input-row">
    <label v-if="label" class="input-label col-2 text-start" :for="id">
      {{ label }}
      <span v-if="required"> *</span> :
    </label>
    <CustomInput
      :id="id"
      :placeholder="placeholder"
      :type="type"
      :required="required"
      :modelValue="modelValue"
      @update:modelValue="updateValue"
      class="input-textbox"
    >
      <slot name="icon"></slot>
    </CustomInput>
  </div>
</template>



<style scoped>
.input-row {
  display: flex;
  align-items: center;
  gap: 10px; /* Espacement entre le label et l'input */
}

.input-label {
  font-size: 14px;
  font-weight: bold;
  color: var(--couleur-noir);
  min-width: 100px; /* Largeur minimale pour aligner les labels */
  text-align: right; /* Aligne le texte à droite */
}

.input-textbox {
  width: 25vw;
}

@media screen and (max-width: 1296px) {
  .input-textbox {
    width: 100% !important;
  }
  
}
</style>