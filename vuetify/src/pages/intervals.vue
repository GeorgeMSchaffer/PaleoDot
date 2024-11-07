
<script lang="ts">
  import { TOccurrence } from '@/common/types'
  import { ref, watchEffect, reactive } from 'vue'
  import {DataTable, Column, Row, ColumGroup} from 'primevue/datatable';
  import {IntervalsList} from '@/components/Intervals/IntervalsList.vue';

  const occurrences = reactive<TOccurrence[]>([])

  export default {
    setup () {
      watchEffect(async () => {
        console.log('WATCH EFFECT', occurrences)
        occurrences.values = await (await fetch('/api/occurrence')).json()
        console.log('occurrences', occurrences)
      })

      return {
        occurrences,
      }
    },
    data () {
      return {
        count: 0,
      }
    },
    created () {
      console.log('created')
      fetch('/api/occurrence').then((res: Response) => res.json()).then((data: TOccurrence[]) => {
        console.log('data', data)
        occurrences.push(...data)
      })
    },
    watch: {
      count (newVal, oldVal) {
        console.log('count changed', newVal, oldVal)
      },
    },
    errorCaptured (err: Error, vm: any, info: string) {
      console.error('errorCaptured', err, vm, info)
    },
    mounted () {
      console.log('mounted')
      // this.fetchOccurrances()
    },
    methods: {
      handleClick (evt: Event) {
        console.log('handle click clicked', evt)
      },
    },

  }
</script>

<style scoped>
</style>

<template>
  <h2>Intervals Container</h2>
  <p>
    {{ JSON.stringify(occurrences) }}
  </p>
</template>
