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
        <path d="M11.88 26.5595L20.5733 17.8661C21.6 16.8395 21.6 15.1595 20.5733 14.1328L11.88 5.43945" :stroke="color" stroke-width="2" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>
</template>
