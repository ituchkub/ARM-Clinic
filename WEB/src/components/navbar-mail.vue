<script setup>
// import _apiLoginAZure from '../api/azureAD-auth'
import { getTokenStorage, removeTokenStorage } from '../helpers/storage';
const emit = defineEmits(['callback'])
import { useRouter } from 'vue-router';
import { onMounted, ref } from 'vue';
const $router = useRouter()
const employDetail = ref({})
const onLogout = async () => {
    const storage = getTokenStorage()
    const params = {
        tenantId: storage.azureDetail.tenantToken,
        redirectUri: storage.azureDetail.redirectUri
    }
    await _apiLoginAZure.logoff(params)
    removeTokenStorage()
    emit("result-logout", true)
}
const onChangePage = (path) => {
    $router.push(path)
}
onMounted( ()=> {
    employDetail.value = getTokenStorage()
} )
</script>
<template>
    <div class="flex justify-between px-12 items-center bg-white shadow h-5 ">
        <!-- <div class="flex space-x-3">
            <img style='display:block; height:50px;' id='base64image' :src=employDetail?.sestionInfo?.LogoImg />
        </div>
        <div class="flex items-center space-x-3">
            <div class="flex flex-col ">
                <span class="font-bold flex justify-end">{{employDetail?.sestionInfo?.PMEmail}}</span>
                <span class="text-gray-400 flex justify-end">{{ employDetail?.sestionInfo?.PMRole }} ({{ employDetail?.sestionInfo?.CompanyCode }})</span>
            </div>
            <div class="hidden md:block">
            <div class="dropdown dropdown-end flex items-center">
                <div tabIndex="0" role="button" class="btn btn-ghost btn-circle avatar ml-2">
                    <div class="w-16 rounded-full">
                        <img src="../assets/images/user-avatar.png" />
                    </div>
                </div>
                <div tabIndex="0"
                    class="mt-52 z-[1] p-2 shadow menu menu-xl dropdown-content bg-base-100 rounded-box">
                    <div class="px-4 py-3">
                        <div class="text-lg font-medium">{{ employDetail?.sestionInfo?.PMFirstname+' '+employDetail?.sestionInfo?.PMLastname }}</div>
                        <div class="text-md">{{ employDetail?.sestionInfo?.PMRole }} ({{ employDetail?.sestionInfo?.CompanyCode }})</div>
                        <div class="text-md">{{employDetail?.sestionInfo?.PMEmail}}</div>
                    </div>
                    <hr>
                    <ul class="">
                        <li>
                            <a @click="onLogout">
                                <svg class="w-6 h-6 text-gray-800" aria-hidden="true" xmlns="http://www.w3.org/2000/svg"
                                    fill="none" viewBox="0 0 24 24">
                                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round"
                                        stroke-width="2"
                                        d="M20 12H8m12 0-4 4m4-4-4-4M9 4H7a3 3 0 0 0-3 3v10a3 3 0 0 0 3 3h2" />
                                </svg>Logout
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div> -->
</div></template>