<script setup lang="ts">
import { ref } from 'vue';
import InputRow from '../inputRow/InputRow.vue';
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import { useNotification } from '@kyvg/vue3-notification';
import RadioButton from './RadioButton.vue';

const emit = defineEmits(['submit']);
const { notify } = useNotification();

const formData = ref({
  civilite: '',
  nom: '',
  prenom: '',
  email: '',
  mdp: '',
  adresse: '',
  complement: '',
  codePostal: '',
  ville: '',
  pays: '',
  telephone: '',
  newsletter: false,
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

  if (!formData.value.prenom.trim()) {
    notify({
      title: 'Mauvaise saisie !',
      text: 'Le champ "Prénom" est requis.',
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

  if (!formData.value.mdp.trim()) {
    notify({
      title: 'Mauvaise saisie !',
      text: 'Le champ "Mot de passe" est requis.',
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
      text: 'Votre formulaire a été soumis avec succès.',
      type: 'success',
    });
  }
};
</script>

<template>
  <div class="form-section">
    <RadioButton
        v-model="formData.civilite"
        title="Civilité"
        :required="true"
        :color="'var(--couleur-noir)'"
        :fontSize="'1em'"
        :options="[
          { label: 'Madame', value: 'madame' },
          { label: 'Monsieur', value: 'monsieur' }
        ]"
      />
    <InputRow
      v-model="formData.nom"
      label="Nom"
      placeholder="Nom..."
      type="text"
      :required="true"
    />
    <InputRow
      v-model="formData.prenom"
      label="Prénom"
      placeholder="Prénom..."
      type="text"
      :required="true"
    />
    <InputRow
      v-model="formData.email"
      label="Email"
      placeholder="Email..."
      type="email"
      :required="true"
    />
    <InputRow
      v-model="formData.mdp"
      label="Mot de passe"
      placeholder="Mot de passe..."
      type="password"
      :required="true"
    />
    <InputRow
      v-model="formData.adresse"
      label="Adresse"
      placeholder="Adresse..."
      type="text"
    />
    <InputRow
      v-model="formData.complement"
      label="Complément d'adresse"
      placeholder="Complément d'adresse..."
      type="text"
    />
    <InputRow
      v-model="formData.codePostal"
      label="Code postal"
      placeholder="Code postal..."
      type="text"
    />
    <InputRow
      v-model="formData.ville"
      label="Ville"
      placeholder="Ville..."
      type="text"
    />
    <InputRow
      v-model="formData.pays"
      label="Pays"
      placeholder="Pays..."
      type="text"
    />
    <InputRow
      v-model="formData.telephone"
      label="Téléphone"
      placeholder="Téléphone..."
      type="text"
    />
  </div>
</template>

<style scoped>
.form-section {
  display: flex;
  flex-direction: column;
  gap: 10px;
}


</style>