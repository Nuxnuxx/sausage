<script setup lang="ts">
import FormNewsletter from '@/presentations/components/form/Newsletter.vue'
</script>

<template>
    <section class="newsletter-section d-inline-flex align-items-center justify-content-between rounded-5 overflow-hidden">
        <div class="d-flex w-100">
            <img src="@/assets/img/background/home_newsletter.webp" alt="Vue d'un avion en vol" class="col-4"/>
            
            <div class="col-8 d-flex flex-column align-items-center justify-content-center">
                <h3 class="fw-bold subtitle-section mb-5">Recevez nos offres spéciales et plus encore</h3>
                <FormNewsletter />
            </div>
        </div>
    </section>
</template>

<style scoped>
.newsletter-section {
  background-color: var(--couleur-gris-clair);
}

img {
  height: 50vh;
  min-height: 100%;
  object-fit: cover;
  object-position: top;
}

.col-8 {
  padding: 5vh 5vw;
}
</style>
