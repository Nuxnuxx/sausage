<script setup lang="ts">
import { ref } from 'vue';
import CustomInput from './InputField.vue';
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import IconMail from '@/presentations/components/svg/IconMail.vue';
import { useNotification } from '@kyvg/vue3-notification';

const emit = defineEmits(['submit']);
const { notify } = useNotification();

const formData = ref({
  nom: '',
  email: '',
  message: '',
});

const validateForm = () => {
  if (!formData.value.nom.trim()) {
    notify({
      title: 'Mauvaise saisie !',
      text: 'Le champ "Nom" est requis.',
      type: 'error',
    });
    return false;
  }

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailRegex.test(formData.value.email)) {
    notify({
      title: 'Mauvaise saisie !',
      text: 'Veuillez entrer une adresse e-mail valide.',
      type: 'error',
    });
    return false;
  }

  if (!formData.value.message.trim()) {
    notify({
      title: 'Mauvaise saisie !',
      text: 'Le champ "Message" est requis.',
      type: 'error',
    });
    return false;
  }

  return true;
};

const submitForm = () => {
  if (validateForm()) {
    emit('submit', formData.value);
    notify({
      title: 'Succès !',
      text: 'Votre message a été envoyé avec succès.',
      type: 'success',
    });
  }
};
</script>

<template>
    <!-- Formulaire -->
    <div class="form-section">
    <CustomInput
        v-model="formData.nom"
        label="Nom"
        placeholder="Nom..."
        type="nom"
        :required="true"
    >
    <template #icon>
        <IconMail />
    </template>
    </CustomInput>

    <CustomInput
        v-model="formData.email"
        label="Email"
        placeholder="Adresse mail..."
        type="email"
        :required="true"
    >
    <template #icon>
        <IconMail />
    </template>
    </CustomInput>

    <CustomInput
    v-model="formData.message"
    label="Votre message"
    placeholder="Votre message..."
    type="textarea"
    :required="true"
    class="message-field"
    >
        <template #icon>
            <IconMail />
        </template>
    </CustomInput>

    <div class="button-contact">
      <ButtonArrow :dark="true" :withArrow="false" :text="'Envoyer'" @click="submitForm" />
    </div>
    
    </div>
  </template>

<style scoped>
.container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 2rem;
  padding: 2rem;
  flex-wrap: wrap;
}

.form-section {
  flex: 1;
  min-width: 300px;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.image-section {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 400px;
}

.image-section img {
  height: 100%;
  width: auto;
  object-fit: cover;
  border-radius: 1rem;
}

.button-contact{
    display: flex;
    justify-content: center;
}

.custom-input-container {
    margin: 0;
}
</style>