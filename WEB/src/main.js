import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import { createRouter, createWebHistory } from 'vue-router';
import routes from './router';
import TypeaheadInput from 'vue3-typeahead-input';
import 'vue3-typeahead-input/dist/style.css';
import axios from 'axios'
import store from './store/index'; 
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import 'bootstrap-icons/font/bootstrap-icons.css';
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});
const app = createApp(App);
app.component('TypeaheadInput', TypeaheadInput)
app.component('VueDatePicker', VueDatePicker);
app.use(router);
app.use(store);
fetch("/config.json").then(res => res.json()).then(config => {
    axios.defaults.baseURL = config.apiUrl;
    app.provide('config', config);
    // Axios interceptor for handling 401 errors
    axios.interceptors.response.use(
        response => response,
        error => {
            if (error.response && error.response.status === 401) {
                router.push('/login'); // Redirect to login page
            }
            return Promise.reject(error);
        }
    );
    app.mount('#app');
});
