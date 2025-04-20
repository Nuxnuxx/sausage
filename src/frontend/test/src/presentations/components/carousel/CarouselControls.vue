<script setup lang="ts">
import ArrowLeft from '@/presentations/components/svg/IconArrowLeft.vue';
import ArrowRight from '@/presentations/components/svg/IconArrowRight.vue';

defineProps({
    targetId: { type: String, required: true },
    light: { type: Boolean, default: false }
});
</script>

<template>
    <div class="controls">
        <button :data-bs-target="'#' + targetId" data-bs-slide="prev"> 
            <ArrowLeft :color="light ? 'var(--couleur-blanc)' : 'var(--couleur-gris-fonce)'" />
        </button>
        <button :data-bs-target="'#' + targetId" data-bs-slide="next">
            <ArrowRight :color="light ? 'var(--couleur-blanc)' : 'var(--couleur-gris-fonce)'" />
        </button>
    </div>
  </template>

<style scoped>
.controls {
    display: flex;
    justify-content: center;
    gap: 1rem;
}

button {
    background-color: unset;
    border: 1px solid var(--couleur-gris-clair);
    padding: 8px 8px;
    border-radius: 16px;
    cursor: pointer;
}

button:hover {
    border: 1px solid var(--couleur-gris-fonce);
}
</style>