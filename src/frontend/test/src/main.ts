import './assets/css/main.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.min.css';
import 'bootstrap';
import 'bootstrap-icons/font/bootstrap-icons.min.css'; 
import 'sweetalert2/dist/sweetalert2.min.css';

import { createApp } from 'vue'
import App from './App.vue'
import router from './router';
import pinia from './store';
import vSelect from 'vue-select';
import Notifications from '@kyvg/vue3-notification';
import VueSweetalert2 from 'vue-sweetalert2';

const app = createApp(App).component('v-select', vSelect);

app.use(router);
app.use(pinia);
app.use(Notifications);
app.use(VueSweetalert2);

app.mount('#app');