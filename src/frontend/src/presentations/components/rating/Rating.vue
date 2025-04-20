<script setup lang="ts">
import IconStar from '@/presentations/components/svg/IconStar.vue';

defineProps({
    mark: Number
});
</script>

<template>
    <div class="rating rounded-2 position-absolute top-0 end-0 mt-3 me-3 px-2 py-1 text-light d-flex align-items-center">
        <IconStar class="me-1" width="14" height="14" />
        <small>{{ mark }}</small>
    </div>
</template>

<style scoped>
.rating {
    background-color: rgba(0, 0, 0, 0.5);
    font-weight: 600;
}
</style>