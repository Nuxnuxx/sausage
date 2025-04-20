<script setup lang="ts">
import Title from "@/presentations/components/title/Title.vue";
import ContactForm from "@/presentations/components/form/ContactForm.vue";
</script>

<template>
    <div>
        <Title title="Contactez nous" backgroundImage="/src/assets/img/background/contactez_nous_header.jpg"></Title>

        <div class="container">
            <div class="page-content row align-items-center">
                <!-- Section Formulaire -->
                <div class="form-section col-lg-6 col-md-12 mb-4">
                    <ContactForm />
                </div>

                <!-- Section Image -->
                <div class="image-section col-lg-6 col-md-12">
                    <img src="@/assets/img/contactez-nous.jpg" alt="Contactez nous" />
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.page-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-block: 5rem;
}

.form-section {
    flex: 1;
    margin-inline: 4rem;
}

.image-section {
    flex: 1;
    display: flex;
    justify-content: center;
    align-items: center;
}

.image-section img {
    width: 120%;
    height: 100%;
    border-radius: 1rem;
}
</style>