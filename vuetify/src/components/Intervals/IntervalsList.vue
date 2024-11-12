<template>
  <h2>{{ intervals?.length ? intervals.length : 0 }} Geological Intervals</h2>

  <div class="card">
    <h5>{{ selectedInterval }}</h5>
    <b>DATA TABLE???</b>
    <DataTable
      table-style="min-width: 50rem;min-height:50rem;"
      :value="intervals"
      paginator
      :rows="25"
      :row-hover="true"
      :rows-per-page-options="[25, 50, 100]"
      selection-mode="single"
    >
      <Column field="intervalName" header="Name" />
      <Column field="recordType" header="Type" />
      <Column field="maxMa" header="Start MYA" />
      <Column field="minMa" header="End MYA" />
      <Column header="Details">
        <template #body="{data}">
          <b>{{ data.intervalName }}</b>
          <!-- <RouterLink :to="{ name: 'Diversity', query: { intervalName: data.intervalName } }">Diversity</RouterLink> -->
          <Button @click="onDetailsButtonClicked(data)">Details</Button>
        </template>
      </Column>
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
  import Button from 'primevue/button'
  import { useRouter } from 'vue-router'
  // import { useAppStore } from '@/stores/app'
  import ProgressSpinner from 'primevue/progressspinner'

  import router from '@/router'
  const intervals = reactive<TInterval[]>([])
  const loading = ref<boolean>(false)
  const selectedInterval = ref<string>('')
  // const router = useRouter()
  console.log(`router:`, router)

  export default {
    name: 'IntervalsList',
    components: {

    },
    onMounted () {
      console.log('mounted')
      // [TODO:]
      // fetch('/api/interval').then((res: Response) => res.json()).then((data: TInterval[]) => {
    },
    data () {
      return { intervals }
    },
    created () {
      console.log('COMPONENTED CREATED - FETCHING', intervals)
      // loading.value = true
      fetch('/api/interval')
        .then((res: Response) => res.json())
        .then((data: TInterval[]) => {
          console.log('data', data)
          // [TODO:] - store intervals in the store instead on the component ?
          // appStore.$patch({ intervals: data })
          const _intervals = data.map((interval: TIntervalDTO) => {
            const _interval = {
              intervalNo: interval.interval_no,
              intervalName: interval.interval_name,
              minMa: interval.min_ma,
              maxMa: interval.max_ma,
              color: interval.color,
              parentNo: interval.parent_no,
              recordType: interval.record_type,
              referenceNo: interval.reference_no,
            } as TInterval
            console.log(`data.map - _interval:`, _interval)
            return _interval
          })
          console.log(`data.map - _intervals:`, _intervals)
          // loading.value = false
        })
    },
  }

</script>
<script lang="ts" setup>
// const appStore = useAppStore()
// [TODO:] - Maybe use https://webdevnerdstuff.github.io/vuetify-drilldown-table/ for the drilldown table where diversity is the drilldown
const onDetailsButtonClicked = (data: TInterval) => {
    console.log(`onDetailsButtonClicked - data:`, data)
    const { intervalNo, intervalName } = data
    selectedInterval.value = intervalName
    router.push(`/diversity/${intervalName}`)
  }

</script>
<script lang="ts">

</script>
<style scoped>
  .card {
    padding: 2em;
  }
</style>
