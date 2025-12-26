<script setup>
import { reactive, watch } from 'vue'
import ColumnChart from './ColumnChart.vue'

const props = defineProps({
  title: String,
  amount: {
    type: Array,
    default: () => [0, 0, 0, 0] // default array if no prop is passed
  }
})

// Reactive chart data
const chartData = reactive({
  labels: ['Jan', 'Feb', 'Mar', 'Apr'], // You can modify this as needed
  datasets: [
    {
      label: 'จำนวนเงิน',
      data: [...props.amount],
      backgroundColor: '#42A5F5'
    }
  ]
})

// Chart options
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

// Watch for changes in `amount` prop and update dataset
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
  <div style="display: flex; flex-direction: column; justify-content: space-around; width: 100%;" class="mr-3">
    <div class="flex justify-end">{{ title }}</div>
    <ColumnChart :chartData="chartData" :chartOptions="chartOptions" />
  </div>
</template>
