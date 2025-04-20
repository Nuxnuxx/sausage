<template>
	<div class="custom-input-container">
    <div class="label-container" v-if="label">
			<label :for="id" class="input-label">
				{{ label }}
				<span v-if="required">*</span>
			</label>
		</div>
		<div class="input-wrapper">
			<div :class="type === 'textarea' ? 'icon-textarea' : 'icon-container'">
				<slot name="icon"></slot>
			</div>
			<template v-if="type === 'textarea'">
				<textarea
				:id="id"
				:value="modelValue"
				:placeholder="placeholder"
				:required="required"
				@input="updateValue"
				class="custom-textarea"
				></textarea>
			</template>
			<template v-else>
				<input
				:id="id"
				:type="type"
				:value="modelValue"
				:placeholder="placeholder"
				:required="required"
				@input="updateValue"
				class="custom-input"
				/>
			</template>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'CustomInput',
  props: {
    id: {
      type: String,
      default: () => `input-${Math.random().toString(36).substr(2, 9)}`
    },
    label: {
      type: String,
      // required: true
	  default: ''
    },
    modelValue: {
      type: [String, Number],
      default: ''
    },
    placeholder: {
      type: String,
      default: ''
    },
    type: {
      type: String,
      default: 'text'
    },
    required: {
      type: Boolean,
      default: false
    }
  },
  emits: ['update:modelValue'],
  methods: {
    updateValue(event: Event) {
      const target = event.target as HTMLInputElement;
      this.$emit('update:modelValue', target.value);
    }
  }
});
</script>

<style scoped>
.custom-input-container {
	position: relative;
	margin-top: 20px;
	margin-bottom: 20px;
	width: 100%;
}

.label-container {
	position: absolute;
	top: -10px;
	left: 15px;
	padding: 0 5px;
	background-color: white;
	z-index: 1;
}

.input-label {
	font-family: var(--font-lexend, sans-serif);
	font-size: 16px;
	font-weight: 500;
	color: var(--couleur-cta, #2969b0);
}

.input-wrapper {
	position: relative;
	display: flex;
	align-items: center;
}

.icon-container, .icon-textarea {
	position: absolute;
	left: 15px;
	transform: translateY(-50%);
	display: flex;
	align-items: center;
	justify-content: center;
	color: var(--couleur-cta, #2969b0);
}

.icon-container {
	top: 50%;
}

.icon-textarea {
	top: 20%;
}

.custom-input {
	width: 100%;
	padding: 16px;
	padding-left: 50px; /* Espace pour l'icône */
	border: 1.5px solid var(--couleur-cta, #2969b0);
	border-radius: 10px;
	font-size: 16px;
	font-family: var(--font-lexend, sans-serif);
	box-sizing: border-box;
}

.custom-input:focus, .custom-textarea:focus {
	outline: none;
	border-color: var(--couleur-hover, #1e4c7a);
}

.custom-textarea {
	width: 100%;
	min-height: 200px;
	padding: 16px;
	padding-left: 50px; /* Espace pour l'icône */
	border: 1.5px solid var(--couleur-cta, #2969b0);
	border-radius: 10px;
	font-size: 16px;
	font-family: var(--font-lexend, sans-serif);
	box-sizing: border-box;
	resize: vertical;
}
</style>
