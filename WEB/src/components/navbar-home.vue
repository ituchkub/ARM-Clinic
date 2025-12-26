<script setup>
const emit = defineEmits(['callback'])
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
// import _apiLoginAZure from '../api/azureAD-auth'
import { getTokenStorage, removeTokenStorage } from '../helpers/storage';
const $router = useRouter()
const employDetail = ref({})
const onLogout = async () => {
    const storage = getTokenStorage()
    // const params = {
    //     tenantId: storage.azureDetail.tenantToken,
    //     redirectUri: storage.azureDetail.redirectUri
    // }
    // await _apiLoginAZure.logoff(params)
    removeTokenStorage()
    emit("result-logout", true)
}
onMounted( ()=> {
    employDetail.value = getTokenStorage()
    console.log("employDetail", employDetail.value.sestionInfo )
} )
</script>
<template>
    <div class="w-full absolute top-0 flex justify-between px-12 pt-6  absolute items-center" style="z-index: 1;">
        <div>
            <button @click="$router.push('/')" class="h-16 px-6 focus:ring-2 focus:ring-black">
                <img style='display:block; height:100px;' id='base64image' src='/src/assets/images/3b796649-ada2-4058-bbb6-8b55a3c9a1d5.png' />
            </button>
        </div>
        <div class="flex items-center space-x-3">
            <div class="flex flex-col ">
                <span class="font-bold flex justify-end">{{employDetail?.sestionInfo?.Name}}</span>
                <span class="text-gray-400 flex justify-end">{{ employDetail?.sestionInfo?.Name }} ({{ employDetail?.sestionInfo?.Name }})</span>
            </div>
            <div class="hidden md:block">
                <div class="dropdown dropdown-end flex items-center">
                    <div tabIndex="0" role="button" class="btn btn-ghost btn-circle avatar ml-2">
                        <div class="w-16 rounded-full">
                            <img src="../assets/images/user-avatar.png" />
                        </div>
                    </div>
                    <div tabIndex="0"
                        class="mt-52 z-[1] p-2 shadow menu menu-xl dropdown-content bg-base-100 rounded-box ">
                        <div class="px-4 py-3">
                            <div class="text-lg font-medium">{{ employDetail?.sestionInfo?.Name+' '+employDetail?.sestionInfo?.Name }}</div>
                            <div class="text-md">{{ employDetail?.sestionInfo?.Name }} ({{ employDetail?.sestionInfo?.Name }})</div>
                            <div class="text-md">{{employDetail?.sestionInfo?.Name}}</div>
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
        </div>
    </div>
</template>
