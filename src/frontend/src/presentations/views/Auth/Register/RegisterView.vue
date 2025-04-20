<script setup lang="ts">
import AuthLayout from "@/presentations/components/layout/AuthLayout.vue";
import RegisterForm from "@/presentations/components/form/Auth/RegisterForm.vue";

const imgBackground = new URL('@/assets/img/background/inscription.jpg', import.meta.url).href;
</script>

<template>
	<AuthLayout
	title="Bienvenue"
	description="S'inscrire avec un e-mail"
	:imageSrc="imgBackground"
	>
		<RegisterForm/>
	</AuthLayout>
</template>
