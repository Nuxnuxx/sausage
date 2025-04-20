<script setup lang="ts">
interface ResultItem {
  id: number;
  title: string;
  location: string;
  rating: number;
  price: number;
  image: string;
  description: string;
}
const props = defineProps<{
  result: ResultItem;
}>();
const emit = defineEmits<{
  'select': [resultId: number];
}>();
const handleCardClick = () => {
  emit('select', props.result.id);
};
const formatPrice = (price: number) => {
  return `${price}€`;
};
</script>

<template>
  <div class="result-card" @click="handleCardClick">
    <div class="left-column">
      <div class="image-container">
        <img :src="result.image || '/placeholder-image.jpg'" :alt="result.title" />
        <div class="rating">
          <span>⭐</span> {{ result.rating }}
        </div>
      </div>
      <div class="card-info">
        <div class="card-header">
          <h3 class="title">{{ result.title }}</h3>
          <div class="price">{{ formatPrice(result.price) }}</div>
        </div>
        <div class="location">
          <i class="bi bi-geo-alt location-icon"></i>
          <span>{{ result.location }}</span>
        </div>
      </div>
    </div>
    <div class="content">
      <p class="description">{{ result.description }}</p>
      <button class="view-more-btn">
        Voir plus <i class="bi bi-arrow-right"></i>
      </button>
    </div>
  </div>
</template>

<style scoped>
.result-card {
  display: flex;
  background-color: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
}
.result-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.15);
}
.left-column {
  display: flex;
  flex-direction: column;
  width: 250px;
  flex-shrink: 0;
}
.image-container {
  position: relative;
  width: 100%;
  height: 250px;
}
.image-container img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}
.rating {
  position: absolute;
  top: 10px;
  right: 10px;
  background-color: rgba(0, 0, 0, 0.7);
  color: white;
  padding: 4px 10px;
  border-radius: 20px;
  font-size: 0.9rem;
  display: flex;
  align-items: center;
  gap: 4px;
}
.card-info {
  padding: 1rem;
  background-color: white;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.5rem;
}
.title {
  font-size: 1.1rem;
  font-weight: 600;
  color: var(--couleur-bleu-fonce, #0E3A6A);
  margin: 0;
}
.price {
  font-size: 1.2rem;
  font-weight: 700;
  color: var(--couleur-principal, #1C61AD);
}
.location {
  display: flex;
  align-items: center;
  font-size: 0.9rem;
  color: #555;
}
.location-icon {
  margin-right: 0.5rem;
  color: #777;
}
.content {
  flex: 1;
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
.description {
  font-family: var(--font-lora, serif);
  font-size: 1rem;
  line-height: 1.5;
  color: #444;
  margin-bottom: 1rem;
}
.view-more-btn {
  align-self: flex-start;
  background-color: transparent;
  color: var(--couleur-principal, #1C61AD);
  border: 1px solid var(--couleur-principal, #1C61AD);
  border-radius: 20px;
  padding: 0.5rem 1.25rem;
  font-size: 0.9rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  cursor: pointer;
  transition: all 0.2s;
}
.view-more-btn:hover {
  background-color: var(--couleur-principal, #1C61AD);
  color: white;
}
/* Responsive */
@media (max-width: 768px) {
  .result-card {
    flex-direction: column;
  }
  .left-column {
    width: 100%;
  }
}
</style>