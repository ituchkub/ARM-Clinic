<script setup>
import { ref, watch } from 'vue'
import PieChart from './PieChart.vue'

const props = defineProps({
  title: String,
  amount: {
    type: Array,
    default: () => [0, 0]
  }
})

const chartData = ref({
  labels: ['ลูกค้าเก่า', 'ลูกค้าใหม่'],
  datasets: [
    {
      label: 'จำนวนลูกค้า',
      data: [...props.amount],
      backgroundColor: ['#F19E01', '#36A2EB']
    }
  ]
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: {
      position: 'top'
    },
    title: {
      display: false,
      text: 'Monthly Data'
    }
  }
}

watch(() => props.amount, (newVal) => {
  chartData.value = {
    ...chartData.value,
    datasets: [
      {
        ...chartData.value.datasets[0],
        data: [...newVal]
      }
    ]
  }
})
</script>

<template>
  <div style="display: flex; flex-direction: column; justify-content: space-around; width: 40%;">
    <PieChart :chartData="chartData" :chartOptions="chartOptions" />
  </div>
</template>
