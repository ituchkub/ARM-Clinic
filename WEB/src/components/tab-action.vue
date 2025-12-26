<script setup>
import { ref } from "vue";
const { tabList } = defineProps(['tabList'])
const emit = defineEmits()
const tabValue = ref(0)
const onChangeTap = (val) => {
    tabValue.value = val
    emit("tab-value", val)
}
</script>
<template>
    <div role="tablist" class="tabs tabs-boxed bg-white h-16  items-center">
        <a v-for="(item, index) in tabList" :key="index" role="tab" @click="onChangeTap(index)"
            :class="{ 'tab h-full': true, 'tab-active': tabValue === index }">
            <span :class="{ 'text-lg font-bold': true, 'text-white': tabValue === index }">{{ item }}</span>
        </a>
    </div>
</template>