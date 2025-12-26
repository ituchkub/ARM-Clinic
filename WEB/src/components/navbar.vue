<script setup>
// import _apiLoginAZure from '../api/azureAD-auth'
import { getTokenStorage, removeTokenStorage } from '../helpers/storage';
const emit = defineEmits(['callback'])
import { useRouter } from 'vue-router';
import { onMounted, ref } from 'vue';
const $router = useRouter()
const employDetail = ref({})
const onLogout = async() => {
    const storage = getTokenStorage()
    // const params = {
    //     tenantId: storage.azureDetail.tenantToken,
    //     redirectUri: storage.azureDetail.redirectUri
    // }
    // await _apiLoginAZure.logoff(params)
    removeTokenStorage()
    emit("result-logout", true)
}
const onChangePage = (path) => {
    $router.push(path)
}
const onOpenManual = () => {
    window.open('./manual/manual.pdf')
}
onMounted( ()=> {
    employDetail.value = getTokenStorage()
    console.log("--->", employDetail.value)
} )
</script>
<template>
    <div class="flex justify-between px-4 items-center bg-white shadow h-16 ">
        <div class="flex space-x-3">
            <img style='display:block; height:50px;' id='base64image' src='/src/assets/images/3b796649-ada2-4058-bbb6-8b55a3c9a1d5.png' />
            <div class="" style="border-left: 2px solid rgb(216, 216, 216);"></div>
            <button class="text-gray-400 hover:text-gray-600" @click="onChangePage('/')">
                <div class="flex items-center space-x-3">
                    <svg width="16" height="18" viewBox="0 0 16 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M1.97458 16.0776H5.32076V10.1353H10.6284V16.0776H13.9746V7.07765L7.97458 2.55842L1.97458 7.07765V16.0776ZM0.474609 17.5776V6.32767L7.97458 0.683472L15.4746 6.32767V17.5776H9.12843V11.6353H6.82073V17.5776H0.474609Z"
                            fill="#BBBCBC" />
                    </svg>
                    <span class="font-bold">Home</span>
                </div>
            </button>
            <!-- <button class="text-gray-400 hover:text-gray-600" @click="onOpenManual">
                <div class="flex items-center space-x-3">
                    <svg width="16" height="20" viewBox="0 0 16 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M3.53228 19.5776C2.68742 19.5776 1.96659 19.2792 1.36981 18.6824C0.773009 18.0856 0.474609 17.3648 0.474609 16.5199V3.82764C0.474609 2.92507 0.790634 2.15776 1.42268 1.52571C2.05473 0.893663 2.82204 0.577637 3.72461 0.577637H15.4746V14.9622C15.0451 14.9622 14.6781 15.1145 14.3736 15.419C14.0691 15.7234 13.9169 16.0904 13.9169 16.5199C13.9169 16.9494 14.0691 17.3164 14.3736 17.6209C14.6781 17.9254 15.0451 18.0776 15.4746 18.0776V19.5776H3.53228ZM1.97458 13.9219C2.2015 13.7668 2.44477 13.6514 2.70438 13.5757C2.964 13.5001 3.23997 13.4623 3.53228 13.4623H4.16691V2.07761H3.72461C3.24383 2.07761 2.83196 2.24909 2.48901 2.59204C2.14606 2.93499 1.97458 3.34685 1.97458 3.82764V13.9219ZM5.66686 13.4623H13.9746V2.07761H5.66686V13.4623ZM3.53228 18.0776H12.8765C12.7316 17.8507 12.6188 17.61 12.538 17.3555C12.4573 17.101 12.4169 16.8225 12.4169 16.5199C12.4169 16.234 12.4547 15.9597 12.5304 15.6968C12.606 15.434 12.7214 15.1891 12.8765 14.9622H3.53228C3.08613 14.9622 2.71498 15.1145 2.41881 15.419C2.12266 15.7234 1.97458 16.0904 1.97458 16.5199C1.97458 16.9661 2.12266 17.3372 2.41881 17.6334C2.71498 17.9295 3.08613 18.0776 3.53228 18.0776Z"
                            fill="#BBBCBC" />
                    </svg>
                    <span class="font-bold">User manual</span>
                </div>
            </button> -->
        </div>
        <div class="flex items-center space-x-3">
            <div class="flex flex-col ">
                <span class="font-bold flex justify-end">{{employDetail?.sestionInfo?.Name +' '+ employDetail?.sestionInfo?.Surname}}</span>
                <span class="text-gray-400 flex justify-end">{{ employDetail?.sestionInfo?.Email }}</span>
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
                            <div class="text-lg font-medium">{{ employDetail?.sestionInfo?.Name+' '+employDetail?.sestionInfo?.Name }}</div>
                            <div class="text-md">{{ employDetail?.sestionInfo?.Name }} ({{ employDetail?.sestionInfo?.Name }})</div>
                            <div class="text-md">{{employDetail?.sestionInfo?.Email}}</div>
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
                                    </svg>ออกจากระบบ
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>