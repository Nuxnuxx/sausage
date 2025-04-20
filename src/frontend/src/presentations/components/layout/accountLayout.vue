<script setup lang="ts">
import Sidebar from "@/presentations/components/sidebar/Sidebar.vue";
import Title from "@/presentations/components/title/Title.vue";
</script>

<template>
    <Title title="Mon compte" backgroundImage="/src/assets/img/background/mon_compte.jpg"></Title>

    <div class="container-fluid">
        <div class="row flex-nowrap min-vh-100">
            <div class="col-auto col-md-3 col-xl-2 px-0">
                <Sidebar></Sidebar>
            </div>
            
            <div class="col m-4">
                <RouterView />
            </div>
        </div>
    </div>
</template>