<script setup lang="ts">
import AuthForm from '@/presentations/components/form/Auth/AuthLayoutForm.vue';
import CustomInput from '@/presentations/components/form/InputField.vue';
import IconMail from '@/presentations/components/svg/IconMail.vue';
import { ref } from 'vue';
import { useNotification } from '@kyvg/vue3-notification';
import { useRouter } from 'vue-router';
import { validateForm } from '@/presentations/components/utils/formValidation';

const emit = defineEmits(['submit']);
const { notify } = useNotification();
const router = useRouter();

const formData = ref({
    email: '',
});

const submitForm = () => {
	const requiredFields: { field: keyof typeof formData.value; label: string }[] = [
		{ field: 'email', label: 'Email' },
	];

	if (!validateForm(formData.value, requiredFields)) {
		return;
	}

	console.log('Form data:', formData.value);

	emit('submit', formData.value);

	notify({
		type: 'info',
		title: 'Vérifiez votre adresse email',
		text: 'Un mail a été envoyé à votre adresse email pour procéder au changement de votre mot de passe.',
		duration: 15000,
	});

	router.push('/connexion');
};
</script>

<template>
  <AuthForm
    submitText="Mot de passe oublié ?"
	:socialLinks=false
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
    </template>
  </AuthForm>
</template>