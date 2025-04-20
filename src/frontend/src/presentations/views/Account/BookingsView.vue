<script setup lang="ts">
import DestinationCard from '@/presentations/components/cards/DestinationCard.vue';
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import IconArrowRight from '@/presentations/components/svg/IconArrowRight.vue';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import Swal from 'sweetalert2';
import { createApp } from 'vue';
import ReviewForm from '@/presentations/components/form/ReviewForm.vue';

const router = useRouter();

// Interfaces
interface Reservation {
  id: number;
  title: string;
  location: string;
  price: string;
  nbPeople: string;
  date: string;
  description: string;
  image: string;
}

interface ReservationWithReview extends Reservation {
  reviewSubmitted: boolean;
}

// Données de réservation
const reservations = ref<ReservationWithReview[]>([
  {
    id: 1,
    title: 'Les Calanques',
    location: 'Corse, France',
    price: '1500€',
    nbPeople: '2 personnes',
    date: 'Du 01/02/2023 au 06/02/2023',
    description: 'Un récapitulatif vous a été envoyé par mail au moment de la réservation. Veuillez nous contacter pour toute autre question.',
    image: new URL('@/assets/img/background/reservation.png', import.meta.url).href,
    reviewSubmitted: false,
  },
  {
    id: 2,
    title: 'Flower Camping',
    location: 'Saint-Aubin-sur-Scie, France',
    price: '400€',
    nbPeople: '3 personnes',
    date: 'Du 10/06/2024 au 13/06/2024',
    description: 'Un récapitulatif vous a été envoyé par mail au moment de la réservation. Veuillez nous contacter pour toute autre question.',
    image: new URL('@/assets/img/background/accueil_offres_phares2_reservation.png', import.meta.url).href,
    reviewSubmitted: false,
  },
]);

const handleAddReview = async (id: number): Promise<void> => {
  const reservation = reservations.value.find((r) => r.id === id);
  if (!reservation) return;

  const wrapper = document.createElement('div');
  const app = createApp(ReviewForm, {
    image: reservation.image,
    title: reservation.title,
    onSubmit: (formValues: { rating: number; comment: string }) => {
      console.log(`Avis pour la réservation ${id} :`, formValues);
      reservation.reviewSubmitted = true;
      Swal.close();
      Swal.fire('Merci !', 'Votre avis a été enregistré.', 'success');
    },
  });

  Swal.fire({
    html: wrapper,
    showConfirmButton: false,
    showCloseButton: true, // Active la croix de fermeture
    customClass: {
      popup: 'swal2-popup-custom',
    },
    didOpen: () => {
      app.mount(wrapper);
    },
    willClose: () => {
      app.unmount();
    },
  });
};
</script>

<template>
  <h1 class="title-section">Mes réservations</h1>
  <div class="m-5">
    <div v-if="reservations.length > 0" class="reservations-container">
      <DestinationCard
        v-for="reservation in reservations"
        :key="reservation.id"
        :image="reservation.image"
        class="col-md-3 d-flex justify-content-center"
      >
        <h3>{{ reservation.title }}</h3>
        <p>{{ reservation.location }}</p>
        <p>Total : {{ reservation.price }}</p>
        <p>Nombre de personnes : {{ reservation.nbPeople }}</p>
        <p>Date : {{ reservation.date }}</p>
        <p class="description">{{ reservation.description }}</p>

        <!-- Bouton ou texte selon si l'avis a été soumis -->
        <div>
          <button
            v-if="!reservation.reviewSubmitted"
            id="rating-button"
            class="border-0 bg-transparent"
            @click="handleAddReview(reservation.id)"
          >
            <IconArrowRight
              width="28px"
              height="28px"
              :color="'var(--couleur-principal)'"
              class="rounded-pill border me-2 p-1"
              style="border-color: var(--couleur-principal) !important;"
            />
            Laisser un avis
          </button>
          

          <button
          v-else
            id="rating-button-published"
            class="border-0 bg-transparent"
            disabled
          >
            <IconArrowRight
              width="28px"
              height="28px"
              :color="'#28a745'"
              class="rounded-pill border me-2 p-1"
              style="border-color: #28a745 !important;"
            />
            Avis publié
          </button>

        </div>
      </DestinationCard>
    </div>

    <div v-else class="text-center d-flex flex-column align-items-center">
      <span class="m-5">Pourquoi attendre ? Imaginez et réservez votre prochaine évasion dès maintenant !</span>
      <ButtonArrow
        :dark="true"
        :withArrow="false"
        :text="'Y aller maintenant !'"
        :clickAction="() => router.push('/vos-envies')"
      />
    </div>
  </div>
</template>

<style scoped>
.reservations-container {
  display: flex;
  gap: 5em;
  flex-wrap: wrap;
}

#rating-button {
  display: inline-flex;
  color: var(--couleur-principal);
}

#rating-button:hover {
  color: var(--couleur-hover);
}

.avis-publie {
  display: inline-block;
  background-color: #f5f5f5;
  color: #28a745;
  font-weight: 500;
  font-size: 0.95rem;
  padding: 0.5em 1em;
  border-radius: 999px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
}

.swal2-close {
  font-size: 1.5rem;
  color: #555;
  opacity: 0.8;
  transition: opacity 0.3s ease;
}

.swal2-close:hover {
  opacity: 1;
  color: #000;
}
</style>
