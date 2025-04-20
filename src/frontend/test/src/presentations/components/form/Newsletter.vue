<script setup lang="ts">
import { ref } from 'vue';
import { useNotification } from '@kyvg/vue3-notification';

const email = ref<string>('');
const { notify } = useNotification();

// Fonction pour valider le format de l'email
const isValidEmail = (email: string): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
};

// Fonction appelée lors de la soumission
const subscribe = () => {
  if (email.value.trim() === '') {
    notify({
      title: 'Mauvaise saisie !',
      text: 'Veuillez entrer une adresse e-mail valide.',
      type: 'error',
    });
    return;
  }

  if (!isValidEmail(email.value)) {
    notify({
      title: 'Format incorrect !',
      text: 'Veuillez entrer une adresse e-mail valide (exemple : nom@exemple.com).',
      type: 'error',
    });
    return;
  }

  notify({
    title: 'Inscription newletter réussie !',
    text: `Merci pour votre inscription : ${email.value}`,
    type: 'success',
  });

  email.value = '';
};
</script>

<template>
    <div class="w-100 d-inline-flex justify-content-between align-items-center rounded-pill py-2 px-3">
        <input
        v-model="email"
        type="email"
        placeholder="Adresse mail"
        class="col-8"
        />
        <button @click="subscribe" class="rounded-pill px-4 h-100 col-4">
        S'inscrire
        </button>
    </div>
</template>

<style scoped>
input {
  border: 0;
}

input:focus-visible {
  outline: unset;
}

div {
  background-color: var(--couleur-blanc);
  min-height: 64px;
}

button {
  background-color: var(--couleur-cta);
  color: white;
  border: none;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: var(--couleur-hover);
}
</style>