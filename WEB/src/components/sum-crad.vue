<script setup>
const { title, amount, icon } = defineProps(['title', 'amount', 'icon'])

// Format amount: show "0.00" if null, otherwise "1,500.00"
const formatAmount = (value) => {
  const num = value == null ? 0 : value
  const hasDecimal = num % 1 !== 0
  return num.toLocaleString('en-US', {
    minimumFractionDigits: hasDecimal ? 2 : 0,
    maximumFractionDigits: hasDecimal ? 2 : 0
  })
}
</script>

<template>
  <div class="px-3 mb-3 mr-3 shadow flex flex-wrap w-full justify-between">
    <div class="flex items-center">
      <i :class="icon + ' p-3'" style="font-size: 45px;"></i>
    </div>
    <div class="flex flex-col justify-around">
      <div class="flex justify-end">{{ title }}</div>
      <div class="flex justify-end text-2xl">{{ formatAmount(amount) }}</div>
    </div>
  </div>
</template>
