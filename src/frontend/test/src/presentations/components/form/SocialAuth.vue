<script setup lang="ts">
import { computed } from 'vue';
import IconApple from '../svg/social/IconApple.vue';
import IconGoogle from '../svg/social/IconGoogle.vue';
import IconFacebook from '../svg/social/IconFacebook.vue';
import { useNotification } from '@kyvg/vue3-notification';

const props = defineProps({
  enabledProviders: {
    type: Array,
    default: () => ['google', 'facebook', 'apple']
  }
});

const emit = defineEmits(['auth']);
const { notify } = useNotification();

const providers = [
  { name: 'google', label: 'Google' },
  { name: 'facebook', label: 'Facebook' },
  { name: 'apple', label: 'Apple' }
];

const filteredProviders = computed(() => {
  return providers.filter(provider =>
    props.enabledProviders.includes(provider.name)
  );
});

const authWithProvider = (providerLabel: string) => {
  notify({
    type: 'error',
    title: 'Une erreur s\'est produite',
    text: `Connexion avec ${providerLabel} momentanément indisponible.`
  });
};
</script>

<template>
  <div class="social-auth-container">
    <button
      type="button" 
      v-for="provider in filteredProviders"
      :key="provider.name"
      class="social-auth-button"
      @click="authWithProvider(provider.label)"
      :aria-label="`Se connecter avec ${provider.label}`"
    >
      <template v-if="provider.name === 'google'">
        <IconGoogle />
      </template>

      <template v-else-if="provider.name === 'facebook'">
        <IconFacebook />
      </template>

      <template v-else-if="provider.name === 'apple'">
        <IconApple />
      </template>
    </button>
  </div>
</template>

<style scoped>
.social-auth-container {
  display: flex;
  justify-content: center;
  gap: 15px;
  margin: 20px 0;
}

.social-auth-button {
  width: 90px;
  height: 45px;
  background-color: var(--couleur-gris-clair);
  border: none;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: background-color 0.2s;
}

.social-auth-button:hover {
  background-color: #e0e0e0;
}

.social-auth-button svg {
  width: 28px;
  height: 28px;
  fill: var(--couleur-principal) !important;
}
</style>
