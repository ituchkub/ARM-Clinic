<script setup>
const { status } = defineProps(['status'])
</script>
<template>
    <div v-show="status"
        class="z-50 absolute  bg-cover  bg-center backdrop-blur-3xl backdrop-filter backdrop-grayscale backdrop-saturate-150 backdrop-opacity-50 h-screen w-screen flex items-center justify-center">
        <div class="flex flex-col items-center">
            <div class="animate-spin rounded-full h-32 w-32 border-t-2 border-b-2 border-gray-900"></div>
            <span class="mt-6 text-3xl text-gray-900 ">Loading</span>
        </div>
    </div>
</template>
<style scoped>
@keyframes spin {
    0% {
        transform: rotate(0deg);
        color: #00FF00;
    }
    100% {
        transform: rotate(360deg);
        color: #FFA500;
    }
}
.animate-spin {
    animation: spin 2s linear infinite;
}
</style>