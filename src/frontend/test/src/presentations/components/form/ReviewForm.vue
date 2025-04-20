<script setup lang="ts">
import { ref, defineEmits, defineProps } from 'vue';

const rating = ref<number | null>(null);
const comment = ref<string>('');
const emit = defineEmits(['submit']);

defineProps({
  image: { type: String, required: true },
  title: { type: String, required: true },
});

const handleSubmit = () => {
  if (!rating.value || rating.value < 1 || rating.value > 5) {
    alert('Veuillez entrer une note entre 1 et 5.');
    return;
  }
  emit('submit', { rating: rating.value, comment: comment.value });
};
</script>

<template>
  <div class="review-form">
    <!-- Image -->
    <img :src="image" alt="Destination" class="review-image" />

    <!-- Titre -->
    <h2 class="review-title">Laissez un avis</h2>
    <h3 class="review-subtitle">{{ title }}</h3>

    <!-- Notes -->
    <div class="review-rating">
      <label>Notes :</label>
      <div class="stars">
        <span
            v-for="star in 5"
            :key="star"
            class="star"
            :class="{ active: rating !== null && star <= rating }"
            @click="rating = star"
            >
          ★
        </span>
      </div>
    </div>

    <!-- Commentaire -->
    <div class="review-comment">
      <label for="comment">Avis (facultatif) :</label>
      <textarea
        id="comment"
        v-model="comment"
        placeholder="Écrire..."
        class="comment-textarea"
      ></textarea>
    </div>

    <!-- Bouton Envoyer -->
    <button @click="handleSubmit" class="submit-button">Envoyer</button>
  </div>
</template>

<style scoped>
.review-form {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 1rem;
  padding: 1rem;
}

.review-image {
  width: 100%;
  max-width: 300px;
  border-radius: 10px;
  object-fit: cover;
}

.review-title {
  font-size: 1.5rem;
  font-weight: bold;
  margin: 0;
}

.review-subtitle {
  font-size: 1.2rem;
  color: #555;
  margin: 0;
}

.review-rating {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.stars {
  display: flex;
  gap: 0.5rem;
}

.star {
  font-size: 2rem;
  cursor: pointer;
  color: #ccc;
}

.star.active {
  color: #f5c518;
}

.review-comment {
  width: 100%;
}

.comment-textarea {
  width: 100%;
  height: 100px;
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 5px;
  resize: none;
}

.submit-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  cursor: pointer;
}

.submit-button:hover {
  background-color: #0056b3;
}
</style>