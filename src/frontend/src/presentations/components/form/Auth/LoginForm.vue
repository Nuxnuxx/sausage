<script setup lang="ts">
import AuthForm from '@/presentations/components/form/Auth/AuthLayoutForm.vue';
import CustomInput from '@/presentations/components/form/InputField.vue';
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
    email: '',
    password: '',
});

const submitForm = async () => {
    const requiredFields: { field: keyof typeof formData.value; label: string }[] = [
        { field: 'email', label: 'Email' },
        { field: 'password', label: 'Mot de passe' },
    ];

    if (!validateForm(formData.value, requiredFields)) {
        return; 
    }

    try {
		await authStore.loginUser(formData.value);
		
        notify({
            type: 'success',
            title: 'Connexion réussie',
            text: 'Vous êtes bien connecté.',
        });

		router.push('/');
	} catch (error) {
		console.error(error);
		notify({
			type: 'error',
			title: 'Erreur',
			text: 'Une erreur est survenue lors de la connexion.',
		});
	}
};
</script>

<template>
  <AuthForm
    title="Connexion"
    submitText="Se connecter"
    footerText="Vous n'avez pas de compte ?"
    footerLinkText="Inscrivez-vous maintenant."
    footerLink="/inscription"
    @submit="submitForm"
  >
    <template #fields>
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

      <span class="d-flex justify-content-end"><RouterLink to="/mot-de-passe-oublie">Mot de passe oublié ?</RouterLink></span>
    </template>
  </AuthForm>
</template>