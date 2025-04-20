<script setup lang="ts">
import AccountForm from "@/presentations/components/form/AccountForm.vue";
import SubscriptionForm from "@/presentations/components/form/SubscriptionForm.vue";
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useNotification } from '@kyvg/vue3-notification';

const router = useRouter();
const { notify } = useNotification();
const handleLogout = () => {
  notify({
    title: 'Modifications sauvegardées',
    text: 'Vos modifications ont été sauvegardées avec succès.',
    type: 'success',
  });

  router.push('/mon-compte');
};

</script>

<template>
  <div>
    <h1 class="title-section">Informations de votre compte</h1>
    <AccountForm />
    <h1 class="title-section">Abonnements</h1>
    <SubscriptionForm />
    <div class="text-end m-5">
      <ButtonArrow @click="handleLogout" :dark="true" :withArrow="false" :text="'Enregistrer'" :clickAction="() => router.push('/mon-compte')" />
    </div>
  </div>

</template>