<script setup lang="ts">
defineProps<{
  title: string;
  description: string;
  imageSrc: string;
}>();
</script>

<template>
    <div class="row vh-100 g-0">
        <div class="col-md-5 d-none d-md-block">
            <img :src="imageSrc" alt="Voyage" class="img-fluid object-fit-cover" />
        </div>
        <div class="auth-content col-md-7 col-12 d-flex flex-column justify-content-center align-items-center">
            <h1>{{ title }}</h1>
            <h4 class="text-muted fs-5">{{ description }}</h4>
            <slot></slot>
        </div>
    </div>
</template>

<style scoped>
img {
  height: 100%;
  width: 100%;
  max-height: 100vh;
  object-fit: cover;
}

.auth-content {
    margin-top: 5em;
}

@media screen and (max-width: 768px) {
    .auth-content {
        margin-top: 7.5em;
    }
}

h1 {
  font-family: var(--font-lexend, sans-serif);
  font-size: 5rem;
  color: var(--couleur-principal, #333);
  line-height: 1.25em;
}

p, h1 {
  margin: 0;
}
</style>