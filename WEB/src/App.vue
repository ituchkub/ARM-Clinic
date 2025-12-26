<script setup>
import { ref, onMounted, nextTick, provide, watch } from 'vue';
import { useRouter } from 'vue-router';
import login from "./pages/login/login.vue";
import Navbar from './components/navbar.vue';
import navbarHome from './components/navbar-home.vue'
import navbarMail from './components/navbar-mail.vue'
import MenuPage from './components/menu-page.vue';
import { getTokenStorage } from './helpers/storage';
import loading from './components/loading.vue';
import { useStore } from 'vuex'
const $router = useRouter()
const $store = useStore()
const layout = ref('home')
const loginMode = ref(true)
const pageOptions = ref({
    pageHeight: 0,
    pageWidth: 0
})
const resultLogin = (response) => {
    if (response === true) {
        loginMode.value = false
        $router.push('/')
    }
}
const resultLogout = (response) => {
    if (response === true) {
        loginMode.value = true
        $router.push('/login')
    }
}
const onResize = async () => {
    pageOptions.value.pageHeight = window.innerHeight
    pageOptions.value.pageWidth = window.innerWidth
    provide('pageOptions', pageOptions.value)
}
const onCheckLayout = () => {
    if (($router.currentRoute.value.path === '/' || $router.currentRoute.value.path === '/login')) {
        layout.value = 'home'
    } else if ($router.currentRoute.value.path === '/mail-link') {
        layout.value = 'mail'
    } else if ($router.currentRoute.value.path === '/manual-view' || $router.currentRoute.value.path === '/mail-found') {
        layout.value = 'manual'
    } else {
        layout.value = 'content'
    }
}
watch(() => $router.currentRoute.value, () => {
    onCheckLayout()
})
const onCheckLogin = () => {
    if ( window.location.pathname == '/mail-found' || window.location.pathname == '/mail-link') {
        // console.log("****")
        loginMode.value = false
    } else {
        // console.log("#####")
        const result = getTokenStorage()
        if (result?.token) {
            loginMode.value = false
        } else {
            loginMode.value = true
        }
    }
    if( window.location.pathname == '/login' && loginMode.value == false){
        $router.push('/')
    }
}
onMounted(async () => {
    onCheckLogin()
    onCheckLayout()
    await onResize()
    await nextTick(() => {
        window.addEventListener('resize', onResize);
    })
})
</script>
<template>
    <loading :status="$store.state.statusLoading" />
    <div v-if="loginMode == true">
        <login @result-login="resultLogin" />
    </div>
    <div class="h-screen overflow-y-auto bg-gray-200" v-else-if="loginMode == false">
        <!-- <navbar-home v-show="layout == 'home'" @result-logout="resultLogout" /> -->
        <navbar v-show="layout != 'content'" @result-logout="resultLogout" />
        <navbar-mail v-show="layout == 'mail'" @result-logout="resultLogout" />
        <router-view />
    </div>
</template>
<style scoped></style>
