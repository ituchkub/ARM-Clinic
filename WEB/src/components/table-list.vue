<script setup>
import { ref, onMounted } from "vue";
const {columns, rows, option} = defineProps(['columns', 'rows'])
// onMounted( async()=> {
//     console.log("columns --->", columns)
//     console.log("rows --->", rows)
// } )
</script>
<template>
    <table class="table table-pin-rows" >
        <thead>
            <tr>
                <th v-for="(item, key) in columns" :key="key" :style="{width: item.width}">
                    <span :class="'text-'+item.size+' text-black'">{{ item.label }}</span>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(row, index) in rows" :key="index" >
                <td v-for="(item, key) in row" :key="key"> 
                    <div v-if="item?.type === 'button'" >
                        <button :class="item.class" @click="()=>(item.action())">{{ item.label }}</button>
                    </div>
                    <div v-else >
                        {{ item }}
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</template>