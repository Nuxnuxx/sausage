<script setup lang="ts">
defineProps<{
    onClick?: (event: MouseEvent) => void;
}>();
</script>

<template>
	<button class="btn fw-bold px-5 py-2 rounded-pill" type="button" @click="onClick">
		<slot></slot>
	</button>
</template>

<style scoped>
.btn {
	background-color: var(--couleur-cta);
	color: var(--couleur-blanc);
	transition: background-color 0.3s;
}

.btn:hover {
	background-color: var(--couleur-hover);
}
</style>
