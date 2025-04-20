<script setup lang="ts">
import AuthLayout from "@/presentations/components/layout/AuthLayout.vue";
import FotgotPasswordForm from "@/presentations/components/form/Auth/ForgotPasswordForm.vue";

const imgBackground = new URL('@/assets/img/background/mdp_oublie.jpg', import.meta.url).href;
</script>

<template>
	<AuthLayout
	title="Mot de passe oublié ?"
	description="Changez votre mot de passe"
	:imageSrc="imgBackground"
	>
		<FotgotPasswordForm
		submit-text="Changer le mot de passe"
		/>
	</AuthLayout>
</template>
