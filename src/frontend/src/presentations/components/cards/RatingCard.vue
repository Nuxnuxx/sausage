<script setup lang="ts">
import IconStar from '@/presentations/components/svg/IconStar.vue';

defineProps({
  name: { type: String, required: true },
  mark: { type: Number, required: true },
  text: { type: String, required: true },
  photo: { type: String, required: true },
});
</script>

<template>
    <div class="card rating-card shadow-sm border-0 rounded-4">

        <div class="photo-container position-absolute">
            <img :src="photo" :alt="name" class="photo rounded-circle" />
        </div>

        <div class="card-body mx-4">
            <div class="d-flex justify-content-between mt-4">
                <h4 class="card-title fw-bold mb-2">{{ name }}</h4>
                <div class="stars mb-3 d-flex">
                    <IconStar
                    v-for="n in 5"
                    :key="n"
                    :class="{ 'filled': n <= mark, 'empty': n > mark }"
                    :fill="n <= mark ? '#FFDA18' : '#E4E5E9'"
                    class="star me-1"
                    />
                </div> 
            </div>
            
            <p class="fs-5 avis-text">{{ text }}</p>
        </div>
    </div>
</template>

<style scoped>
.rating-card {
    padding-top: 2rem;
}

.photo-container {
    width: 90px;
    height: 90px;
    top: -40px;
    left: 40px;
}

.photo {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.stars {
  font-size: 1.2rem;
}

.star {
  width: 20px;
  height: 20px;
}

.star.filled {
  fill: #FFDA18;
}

.star.empty {
  fill: #E4E5E9;
}

.avis-text {
	display: -webkit-box;
	-webkit-line-clamp: 5; /* Limite à 5 lignes */
	line-clamp: 5; /* Limite à 5 lignes */
	-webkit-box-orient: vertical;
	overflow: hidden;
}
</style>