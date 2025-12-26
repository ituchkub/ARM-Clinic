<script setup>
const props = defineProps(['formModalAlert'])
// const emit = defineEmits(['emitCloasModal'])
const emitCloasModal = () => {
    document.getElementById('modal-alert').close()
    // emit('emitCloasModal', false)
}
</script>
<template>
  <dialog id="modal-alert" class="modal">
        <div class="modal-box max-w-4xl   px-8">
            <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2" @click="emitCloasModal">
                ✕
            </button>
            <div class="flex flex-col">
                <span class="font-bold">{{ props?.formModalAlert?.label || "" }}</span>
                <div class="flex justify-end mt-16 space-x-3">
                    <button class="bg-custom-blue hover:bg-amber-600t text-white px-3 py-2 rounded-full w-40" @click="emitCloasModal">Confirm</button>
                </div>
            </div>
        </div>
    </dialog>
</template>
