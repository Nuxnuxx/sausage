<script setup lang="ts">
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import { useRouter } from 'vue-router';

const router = useRouter();
</script>

<template>
    <div class="not-found-container">
        <div class="not-found-content">
            <h1 class="not-found-title">Oupss...</h1>
            <h3 class="not-found-message">cette destination est introuvable !</h3>
            <h5 class="not-found-description">
            On dirait que vous avez pris un vol vers une destination inconnue. Retournez à l’accueil pour retrouver votre chemin vers vos rêves d’évasion.
            </h5>
            <ButtonArrow :clickAction="() => router.push('/')" :dark="true" :text="'Retour'" id="not-found-button"></ButtonArrow>
        </div>
    </div>
</template>
  
<style scoped>
.not-found-container {
    background-color: #EAEAEA;
    border-radius: 16px;
    flex: 1;
    display: flex;
    justify-content: center;
    align-items: center;
    margin: 64px;
    font-family: 'Lexend', sans-serif;
    height: 100%;
}

.not-found-content{
    max-width: 80%;
    margin: auto;
    padding-block: 64px;
}

.not-found-title {
    font-size: 3rem;
    color: #1C61AD;
    margin-bottom: 0.5rem;
    font-weight: 600;
}

.not-found-message {
    font-size: 2rem;
    color: #1C61AD;
    margin-bottom: 1rem;
    line-height: 110%;
}

.not-found-description {
    font-size: 1.5rem;
    line-height: 110%;
    color: #1A1A1A;
    margin-bottom: 2rem;
}

#not-found-button {
    margin-left: 64px;
}
</style>
  