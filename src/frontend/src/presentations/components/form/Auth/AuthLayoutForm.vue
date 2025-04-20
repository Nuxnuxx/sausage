<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
import Button from '@/presentations/components/form/FormButton.vue';
import Divider from '@/presentations/components/form/Divider.vue';
import SocialAuth from '@/presentations/components/form/SocialAuth.vue';

defineProps({
  submitText: {
    type: String,
    required: true,
  },
  footerText: {
    type: String,
    required: false,
    default: '',
  },
  footerLinkText: {
    type: String,
    required: false,
    default: '',
  },
  footerLink: {
    type: String,
    required: false,
    default: '',
  },
  socialLinks: {
    type: Boolean,
    required: false,
    default: true,
  },
});

const emit = defineEmits(['submit']);
</script>

<template>
    <form class="form-container" @submit.prevent="$emit('submit')">
        <!-- Slot pour les champs du formulaire -->
        <slot name="fields"></slot>

        <div class="d-flex justify-content-center my-4">
        <Button type="submit">{{ submitText }}</Button>
        </div>

        <template v-if="socialLinks">
          <Divider text="OU" />

          <SocialAuth />
        </template>

        <div v-if="footerText && footerLinkText" class="form-footer mt-4 text-center">
            {{ footerText }}
            <a :href="footerLink">{{ footerLinkText }}</a>
        </div>
    </form>
</template>

<style scoped>
.form-container {
  width: 100%;
  max-width: 500px;
  padding: 30px;
}

.form-footer {
  font-size: 0.9em;
  color: var(--couleur-texte-secondaire, #666);
}

.form-footer a {
  color: var(--couleur-cta);
}
</style>