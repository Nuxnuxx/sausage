<script setup lang="ts">
import { RouterLink } from 'vue-router'
import IconFacebook from '@/presentations/components/svg/social/IconFacebook.vue';
import IconInstagram from '@/presentations/components/svg/social/IconInstagram.vue';
import IconLinkedIn from '@/presentations/components/svg/social/IconLinkedIn.vue';
import IconsPayment from '@/presentations/components/svg/IconsPayment.vue';

defineProps<{ isLogged: boolean }>();

const currentYear = new Date().getFullYear();
</script>

<template>
    <footer class="footer">
        <div class="container">
            <div class="footer-columns">
                <div class="footer-column">
                    <h3>Navigation</h3>
                    <ul>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/vos-envies">Vos envies</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/voyages-organises">Voyages organisés</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/bons-plans">Nos bons plans</RouterLink>
                        </li>

                        <li class="nav-item" v-if="isLogged">
                            <RouterLink class="nav-link" active-class="active" to="/mon-compte">Mon compte</RouterLink>
                        </li>
                        <li class="nav-item" v-else>
                            <RouterLink class="nav-link" active-class="active" to="/connexion">Connexion</RouterLink>
                        </li>
                    </ul>
                </div>

                <div class="footer-column">
                    <h3>Informations</h3>
                    <ul>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/a-propos">À propos</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/mention-legales">Mentions légales</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/conditions-generales-vente">Conditions générales de vente</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/politique-confidentialite">Politique de confidentialité</RouterLink>
                        </li>
                    </ul>
                </div>

                <div class="footer-column">
                    <h3>Assistance</h3>
                    <ul>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/contact">Nous contacter</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink class="nav-link" active-class="active" to="/faq">Aides et FAQ</RouterLink>
                        </li>
                    </ul>
                </div>

                <div class="footer-column">
                    <h3>Réseaux sociaux</h3>
                    <div class="social-icons">
                        <a href="https://www.facebook.com/" target="_blank">
                            <IconFacebook width="32" height="32" fill="var(--couleur-blanc)" />
                        </a>
                        <a href="https://www.instagram.com/" target="_blank">
                            <IconInstagram width="32" height="32" fill="var(--couleur-blanc)" />
                        </a>
                        <a href="https://www.linkedin.com/" target="_blank">
                            <IconLinkedIn width="30" height="30" fill="var(--couleur-blanc)" />
                        </a>
                    </div>
                </div>
            </div>

            <hr />

            <div class="footer-bottom">
                <p>&copy; Copyright {{ currentYear }} Next Horizon Plus. Tous droits réservés</p>
                <IconsPayment width="278" height="30" />
            </div>
        </div>
    </footer>
</template>
  
<style scoped>
.footer {
    background-color: var(--couleur-bleu-fonce);
    color: var(--couleur-blanc);
    padding: 2rem;
}

.container {
    max-width: 1200px;
}

.footer-columns {
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
}

.footer-column {
    min-width: 200px;
    margin-bottom: 1rem;
}

.footer-column h3 {
    margin-bottom: 1rem;
    color: var(--couleur-gris-clair);
    font-size: 1.2rem;
    font-weight: 600;
}

.footer-column ul {
    list-style: none;
    padding: 0;
}

.footer-column ul li {
    margin-bottom: 0.5rem;
}

.footer-column ul li a {
    opacity: 0.8;
}

.footer-column ul li a:hover {
    opacity: 1;
}

.social-icons {
    display: flex;
    gap: 24px;
}

hr {
    margin: 1.5rem 0;
    opacity: 0.2;
}

.footer-bottom {
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-wrap: wrap;
}

p {
    font-family: 'Lexend', sans-serif;
}
</style>
  