<script setup lang="ts">
defineProps({
    percent: Number
});
</script>

<template>
    <div class="rounded-2 bg-danger fw-bold position-absolute top-0 end-0 mt-5 me-3 px-2 py-1 text-light d-flex align-items-center">
        <small>-{{ percent }}%</small>
    </div>
</template>

<style scoped>
small {
    font-size: 0.7rem;
}
</style>
