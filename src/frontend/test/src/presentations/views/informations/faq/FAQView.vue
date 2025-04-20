<script setup lang="ts">
import Title from "@/presentations/components/title/Title.vue";
import { ref } from "vue";

// Données pour la FAQ
const faqItems = ref([
  {
    question: "Puis-je modifier ou annuler ma réservation ? Y a-t-il des frais associés ?",
    answer: "Oui, vous pouvez modifier ou annuler à tout moment votre réservation sans frais. Vous serez remboursé dans un délai de 15 jours si vous avez annulé 72 heures avant le début du séjour ou si vous aviez souscrit à une assurance annulation.",
    id: "faq1",
  },
  {
    question: "Quels sont les moyens de paiement acceptés ?",
    answer: "Nous acceptons les cartes de crédit, PayPal et les virements bancaires.",
    id: "faq2",
  },
  {
    question: "Combien de temps à l’avance dois-je réserver mon voyage ?",
    answer: "Il est conseillé de réserver au moins 2 semaines à l'avance pour garantir la disponibilité.",
    id: "faq3",
  },
  {
    question: "Est-ce que je reçois une confirmation après avoir effectué une réservation ?",
    answer: "Oui, une confirmation vous sera envoyée par e-mail après la réservation.",
    id: "faq4",
  },
  {
    question: "Aidez-vous à obtenir des visas ou d'autres documents de voyage nécessaires ?",
    answer: "Oui, nous offrons une assistance pour l'obtention des visas selon votre destination.",
    id: "faq5",
  },
  {
    question: "Comment puis-je vous contacter en cas d’urgence pendant mon voyage ?",
    answer: "Vous pouvez nous contacter via notre hotline d'urgence disponible 24/7.",
    id: "faq6",
  },
]);
</script>

<template>
  <div>
    <Title title="Aides et FAQ" backgroundImage="/src/assets/img/background/faq.jpg"></Title>

    <div class="container my-5">
      <div class="accordion" id="faqAccordion">
        <div class="accordion-item" v-for="item in faqItems" :key="item.id">
          <h2 class="accordion-header">
            <button
              class="accordion-button fw-bold"
              type="button"
              data-bs-toggle="collapse"
              :data-bs-target="'#' + item.id"
              aria-expanded="false"
              :aria-controls="item.id"
            >
              {{ item.question }}
            </button>
          </h2>
          <div
            :id="item.id"
            class="accordion-collapse collapse"
            data-bs-parent="#faqAccordion"
          >
            <div class="accordion-body text-muted fw-light">
              {{ item.answer }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>
