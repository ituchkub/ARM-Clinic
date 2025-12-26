<script setup>
import { computed } from "vue"
const emit = defineEmits(['model-input'])
const { inputList } = defineProps(['inputList'])
const model = computed({
    get: () => inputList,
    set: (newValue) => emit('model-input', newValue)
})
const onchangeInput = () => {
    emit('model-input', model.value)
}
</script>
<template>
    <div class="grid grid-cols-3 gap-y-5">
        <template v-for="(item, key) in model" :key="key">
            <!-- <div class="col-span-full">aaaa</div> -->
            <div v-if="item.type === 'header'" class="custom-blue h-14 flex items-center col-span-full px-6">
                <span class="text-lg font-bold text-black">{{ item.label }}</span>
            </div>
            <div v-else-if="item.type === 'radio'" :class="{ 'flex flex-col mx-9': true, 'col-span-full': item?.fullRow }">
                <label class="font-bold">{{ item.label }}</label>
                <div class="flex items-center space-x-4 mt-2">
                    <template v-for="(val, index) in item.list" :key="index">
                        <input type="radio" class="radio bg-white outline" :name="item.field" v-model="item.model">
                        <label class="font-bold">{{ val.label }}</label>
                    </template>
                </div>
            </div>
            <div v-else-if="item.type === 'textarea'"
                :class="{ 'flex flex-col mx-9': true, 'col-span-full': item?.fullRow }">
                <label class="font-bold">{{ item.label }}</label>
                <textarea :rows="item?.rows"
                    :class="{ 'mt-2 outline-none px-3': true, 'ring-1 ring-primary focus:ring-2 foocus:ring-primary': !item.readonly }" v-model="item.model"></textarea>
            </div>
            <div v-else-if="item.type === 'date'" :class="{ 'flex flex-col mx-9': true, 'col-span-full': item?.fullRow }">
                <label class="font-bold">{{ item.label }}</label>
                <input type="date" :readonly="item.readonly"
                    :class="{ 'h-12 mt-2 outline-none px-3': true, 'ring-1 ring-primary focus:ring-2 foocus:ring-primary': !item.readonly }"
                    @keypress="onchangeInput" v-model="item.model" />
            </div>
            <!-- <div v-else-if="item.type === 'autocomplete'"
                :class="{ 'flex flex-col mx-9': true, 'col-span-full': item?.fullRow }">
                <label class="font-bold">{{ item.label }}</label>
                <TypeaheadInput :readonly="item.readonly" 
                :class="{ 'h-12 mt-2 outline-none px-3': true, 'ring-1 ring-primary focus:ring-2 foocus:ring-primary': !item.readonly }"
                 v-model="item.model"
                    :items="item.list" itemText="label" itemValue="value" placeholder="- ALL -">
                </TypeaheadInput>
            </div> -->
            <div v-else :class="{ 'flex flex-col mx-9': true, 'col-span-full': item?.fullRow }">
                <label class="font-bold">{{ item.label }}</label>
                <input :readonly="item.readonly"
                    :class="{ 'h-12 mt-2 outline-none px-3': true, 'ring-1 ring-primary focus:ring-2 foocus:ring-primary': !item.readonly }"
                    @keypress="onchangeInput" v-model="item.model" />
            </div>
        </template>
        <!-- <div class="flex flex-col mx-9">
            <label class="font-bold">aaaaaa</label>
            <select :class="{ 'h-12 mt-2 outline-none px-3': true }">
                <option> 1</option>
                <option>2 </option>
                <option> 3</option>
            </select>
        </div> -->
    </div>
</template>
<style scoped>
.ta-input {
    height: 3rem;
    border: none;
    border-radius: none
}
.ta-input-panel-place{
  top:calc(100% + 0.1rem) !important;
}
.ta-input-input{
  /* font-family: 'my-font'; */
  font-size:14px !important;
}
</style>