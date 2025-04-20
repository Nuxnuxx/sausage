<script setup lang="ts">
defineProps({
    color: {
        type: String,
        default: '#1A1A1A',
    },
})
</script>

<template>
    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 32 32" fill="none">
        <path d="M20.12 26.5595L11.4267 17.8661C10.4 16.8395 10.4 15.1595 11.4267 14.1328L20.12 5.43945" :stroke="color" stroke-width="2" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>
</template>
