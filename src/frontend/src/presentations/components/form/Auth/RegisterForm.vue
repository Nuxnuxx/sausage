<script setup lang="ts">
import AuthForm from '@/presentations/components/form/Auth/AuthLayoutForm.vue';
import CustomInput from '@/presentations/components/form/InputField.vue';
import RadioButton from '@/presentations/components/form/RadioButton.vue';
import IconMail from '@/presentations/components/svg/IconMail.vue';
import IconLock from '@/presentations/components/svg/IconLock.vue';
import { ref } from 'vue';
import { useNotification } from '@kyvg/vue3-notification';
import { useRouter } from 'vue-router';
import { validateForm } from '@/presentations/components/utils/formValidation';
import { useAuthStore } from '@/store/authStore';

const { notify } = useNotification();
const router = useRouter();
const authStore = useAuthStore();

const formData = ref({
	civilite: '',
	nom: '',
	prenom: '',
	email: '',
	password: '',
});

const submitForm = async () => {
	const requiredFields: { field: keyof typeof formData.value; label: string }[] = [
		{ field: 'civilite', label: 'Civilité' },
		{ field: 'nom', label: 'Nom' },
		{ field: 'prenom', label: 'Prénom' },
		{ field: 'email', label: 'Email' },
		{ field: 'password', label: 'Mot de passe' },
	];

	if (!validateForm(formData.value, requiredFields)) {
		return;
	}

	try {
		await authStore.registerUser(formData.value);

		notify({
			type: 'success',
			title: 'Inscription réussie',
			text: 'Votre inscription a été réalisée avec succès.',
		});

		router.push('/');
	} catch (error) {
		console.error(error);
		notify({
			type: 'error',
			title: 'Erreur',
			text: 'Une erreur est survenue lors de l\'inscription.',
		});
	}
};
</script>

<template>
  <AuthForm
    submitText="S'inscrire"
    @submit="submitForm"
  >
    <template #fields>
      <RadioButton
        class="mb-4"
        v-model="formData.civilite"
        title="Civilité"
        :required="true"
        :options="[
          { label: 'Madame', value: 'madame' },
          { label: 'Monsieur', value: 'monsieur' }
        ]"
      />

      <CustomInput
        v-model="formData.nom"
        label="Nom"
        placeholder="Nom..."
        type="text"
        :required="true"
      />

      <CustomInput
        v-model="formData.prenom"
        label="Prenom"
        placeholder="Prenom..."
        type="text"
        :required="true"
      />

      <CustomInput
        v-model="formData.email"
        label="Email"
        placeholder="adresse mail..."
        type="email"
        :required="true"
      >
        <template #icon>
          <IconMail />
        </template>
      </CustomInput>

      <CustomInput
        v-model="formData.password"
        label="Mot de passe"
        placeholder="votre mot de passe..."
        type="password"
        :required="true"
      >
        <template #icon>
          <IconLock />
        </template>
      </CustomInput>
    </template>
  </AuthForm>
</template>