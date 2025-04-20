<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

const props = defineProps({
	modelValue: {
		type: String,
		required: true,
	},
	title: {
		type: String,
		required: true,
	},
	options: {
		type: Array as () => { label: string; value: string }[],
		required: true,
		default: () => [],
	},
	required: {
		type: Boolean,
		default: false, 
	},
	color: {
		type: String,
		default: 'var(--couleur-principal)',
	},
	fontSize: {
		type: String,
		default: '1.25em',
	},
});

const emit = defineEmits(['update:modelValue']);

const updateValue = (value: string) => {
  	emit('update:modelValue', value);
};
</script>

<template>
	<div class="d-flex w-100 " :style="{ color: color }">
		<span class="radio-label d-flex" :class="fontSize ? fontSize : 'fs-5'" >
			{{ title }} <span v-if="required">&nbsp;*</span>
		</span>
		<div v-for="option in options" class="d-flex justify-content-around w-100">
			<div class="mx-auto">
				<input
					type="radio"
					class=""
					:value="option.value"
					:checked="modelValue === option.value"
					:required="required"
					:style="{ borderColor: color, fontSize: fontSize }"
					@change="updateValue(option.value)"
				/>
				<label class="radio-text ms-3" :class="fontSize ? fontSize : 'fs-5'">{{ option.label }}</label>
			</div>
		</div>
	</div>
</template>

<style scoped>


input[type='radio'] {
  width: 1em;
  height: 1em;
  border: 2px solid;
  border-radius: 0;
  background-color: transparent;
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  transition: background-color 0.3s ease, border-color 0.3s ease;
  cursor: pointer;
  position: relative;
}

input[type='radio']:checked {
  border-color: var(--couleur-cta);
  background-color: var(--couleur-cta);
}

input[type='radio']:checked::after {
  content: '✕';
  color: var(--couleur-blanc);
  font-size: 0.9em;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>