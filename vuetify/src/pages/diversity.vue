<template>
  <h2>{{ intervalName }} Interval Diversity</h2>
  <BioDiversityList :intervalName="intervalName" />
</template>
<script setup lang="ts">
  import { defineProps } from 'vue'
  import BioDiversityList from '@/components/Diversity/DiversityList.vue'
  defineProps({
    intervalName: String,
  })
</script>
