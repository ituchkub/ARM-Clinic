<script setup>
const props = defineProps(['formModalWarning'])
const emit = defineEmits()
const emitValue = () => {
    emit('emitValue', true)
}
const emitCloasModal = () => {
    document.getElementById('modal-warning').close()
    emit('emitCloasModal', false)
}
</script>
<template>
  <dialog id="modal-warning" class="modal">
        <div class="modal-box max-w-4xl   px-8">
            <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2" @click="emitCloasModal">
                ✕
            </button>
            <div class="flex flex-col">
                <span class="font-bold">{{ props?.formModalWarning?.label || "" }}</span>
                <div class="flex justify-end mt-16 space-x-3">
                    <button class="bg-gray-400 hover:bg-gray-500 text-white px-3 py-2 rounded-full w-40" @click="emitCloasModal">Cancel</button>
                    <!-- <button class="bg-custom-blue hover:bg-amber-600t text-white px-3 py-2 rounded-full w-40" @click="emitValue"></button> -->
                </div>
            </div>
        </div>
    </dialog>
</template>
