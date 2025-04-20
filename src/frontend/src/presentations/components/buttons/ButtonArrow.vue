<script setup lang="ts">
import IconArrow from '@/presentations/components/svg/IconArrow.vue';

withDefaults(defineProps<{
  clickAction?: (payload: MouseEvent) => void;
  dark?: boolean;
  withArrow?: boolean;
  text: string;
}>(), {
  withArrow: true,
  dark: false,
});
</script>

<template>
	<button :class="{ dark }" @click="clickAction">
		{{ text }}
		<IconArrow v-if="withArrow" :stroke="dark ? 'var(--couleur-blanc)' : 'var(--couleur-principal)'" width="25" height="25"/>
	</button>
</template>
  
<style scoped>
button {
  padding: 12px 30px;
  border-radius: 32px;
  cursor: pointer;
  font-size: 1rem;
  transition: 0.25s;
  border: none;
}

button.dark {
  background-color: var(--couleur-principal);
  color: var(--couleur-blanc);
}

button:not(.dark) {
  background-color: var(--couleur-blanc);
  color: var(--couleur-noir);
  border: 1px solid #d3d3d3;
  font-weight: bold;
}

button:hover {
  box-shadow: 9px 9px 33px #d1d1d1, -9px -9px 33px #ffffff;
  font-size: 1.05em;
}

button > svg {
  margin-left: 6px;
}

button:hover > svg {
  transition: 0.5s;
  transform: translateX(5px);
}
</style>
