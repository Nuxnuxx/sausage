<script setup lang="ts">
import AuthLayout from "@/presentations/components/layout/AuthLayout.vue";
import LoginForm from "@/presentations/components/form/Auth/LoginForm.vue";

const imgBackground = new URL('@/assets/img/background/login.jpg', import.meta.url).href;
</script>

<template>
	<AuthLayout
	title="Connectez vous"
	description="Se connecter avec un e-mail"
	:imageSrc="imgBackground"
	>
		<LoginForm
		submit-text="Se connecter"
		footer-link-text="Inscrivez-vous maintenant."
		footer-text="Vous n'avez pas de compte ?"
		footer-link="/inscription"
		/>
	</AuthLayout>
</template>
