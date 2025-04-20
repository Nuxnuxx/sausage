<script setup lang="ts">

defineProps({
  	image: { type: String, required: true },
});
</script>

<template>
	<div class="card rounded-4 shadow-sm overflow-hidden border border-light d-flex flex-column justify-conten-between min-h-100">
		<img :src="image" :alt="image" class="card-image w-100 object-fit-cover" />
		<div class="card-content flex-grow-1 d-flex flex-column">
			<slot></slot>
		</div>
	</div>
</template>

<style scoped>
.card-image {
	height: 300px;
}

.card-content {
	padding: 1.5rem;
}

.discover-button {
	color: var(--couleur-principal);
}

.discover-button:hover {
	color: var(--couleur-hover);
}

.card-title h4, .card-title h6 {
	margin-block: 4px;
}

.description {
	display: -webkit-box;
	-webkit-line-clamp: 10; /* Limite à 10 lignes */
	line-clamp: 10; /* Limite à 10 lignes */
	-webkit-box-orient: vertical;
	overflow: hidden;
}

.destination-card {
  width: 300px;
  border: 1px solid #ddd;
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
}

.destination-image {
  width: 100%;
  height: 150px;
  object-fit: cover;
}

.destination-details {
  padding: 15px;
  text-align: left;
  display: flex;
  flex-direction: column;
  flex-grow: 1;
}

.destination-details h3 {
  margin: 0 0 10px;
  font-size: 18px;
  color: #333;
}

.destination-details p {
  margin: 5px 0;
  font-size: 14px;
  color: #555;
}

</style>