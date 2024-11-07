<template>
  <h2>{{ intervals?.length ? intervals.length : 0 }} Geological Intervals</h2>

  <div class="card">
    <DataTable
      table-style="min-width: 50rem;min-height:50rem;"
      :value="intervals"
    >
      <Column field="interval_name" header="Name" />
      <Column field="record_type" header="Type" />
      <Column field="max_ma" header="Start MYA" />
      <Column field="min_ma" header="End MYA" />
    </DataTable>
  </div>
</template>

<script lang="ts">
  import { TInterval, TIntervalDTO, TOccurrence } from '@/common/types'
  import { reactive, ref, watchEffect } from 'vue'
  import DataTable from 'primevue/datatable'
  import Column from 'primevue/column'
  import ColumnGroup from 'primevue/columngroup' // optional
  import Row from 'primevue/row'

  const products = ref()
  const columns = [
    { field: 'code', header: 'Code' },
    { field: 'name', header: 'Name' },
    { field: 'category', header: 'Category' },
    { field: 'quantity', header: 'Quantity' },
  ]

  const intervals = ref<TInterval[]>([])
  export default {
    name: 'IntervalsList',
    components: {
    },
    onMounted () {
      console.log('mounted')
      fetch('/api/interval').then((res: Response) => res.json()).then((data: TIntervalDTO[]) => {
        console.log(`fetch - data:`, data)
        data.map((interval:TIntervalDTO) => {
          const _interval = {
            intervalNo: interval.interval_no,
            intervalName: interval.interval_name,
            minMa: interval.min_mya,
            maxMa: interval.max_mya,
            color: interval.color,
            parentNo: interval.parent_no,
            recordType: interval.record_type,
            referenceNo: interval.reference_no,
          } as TInterval
          console.log(`data.map - _interval:`, _interval)
          intervals.value.push(_interval)
        })
        // intervals.value.push(...data)
      })
    },

    // onCreated () {
    //   console.log('created')
    //   fetch('/api/interval').then((res: Response) => res.json()).then((data: TInterval[]) => {
    //     console.log('data', data)
    //     intervals.value.push(...data)
    //   })
    // },
    data () {
      return {
        count: 0,
        intervals,
      }
    },
  }
</script>
<script lang="ts" setup>

watchEffect(() => {
  console.log('Intervlas WATCH EFFECT', intervals)
  fetch('/api/interval')
    .then((res: Response) => res.json())
    .then((data: TInterval[]) => {
      console.log('data', data)
      intervals.value = data
    })
})

</script>
<script lang="ts">

</script>
<style scoped>
  .card {
    padding: 2em;
  }
</style>
