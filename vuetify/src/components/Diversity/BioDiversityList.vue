<template>
  <h2>{{ intervals?.length ? intervals.length : 0 }} Geological Intervals</h2>

  <div class="card">
    <h5>{{ selectedInterval }} Bio Diversity</h5>
    <h1>{{ loading ? 'Loading' : 'FALSE' }}</h1>

    <ProgressSpinner
      style="width: 50px; height: 50px"
      stroke-width="8"
      fill="transparent"
      animation-duration=".5s"
      aria-label="Custom ProgressSpinner"
    />
    <DataTable
      v-show="!loading"
      table-style="min-width: 50rem;min-height:50rem;"
      :value="diversity"
      paginator
      :rows="25"
      :row-hover="true"
      :rows-per-page-options="[25, 50, 100]"
      selection-mode="single"

    >
      <Column field="intervalName" header="Name" />
      <Column field="maxMya" header="Max Mya" />
      <Column field="minMya" header="Min Mya" />
      <Column field="countOfOccurrences" header="Type" />xpo
      <Column field="countOfFamilies" header="Family" />
      <Column field="countOfClasses" header="Classes" />
      <Column field="countOfPhyla" header="Phyla" />
      <Column field="countOfOrders" header="Orders" />
      <Column field="countOfGenera" header="Genera" />
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
<!--
  intervalName: string;
  countOfOccurrences: number | null;
  countOfFamilies: number | null;
  countOfClasses: string | null;
  countOfPhyla: number;
  countOfOrders: number;
  countOfGenera: number;
  maxMya: number;
  minMya: number;
-->
<script lang="ts">
  import { TDiversity, TInterval, TIntervalDTO, TOccurrence } from '@/common/types'
  import { reactive, ref, watchEffect } from 'vue'
  import DataTable from 'primevue/datatable'
  import Column from 'primevue/column'
  import ColumnGroup from 'primevue/columngroup' // optional
  import Row from 'primevue/row'
  import Button from 'primevue/button'
  import { useRouter } from 'vue-router'
  import { useAppStore } from '@/stores/app'
  import router from '@/router'

  const intervals = ref<TInterval[]>([])
  const selectedInterval = ref<string>('')
  const loading = ref<boolean>(false)

  export default {
    name: 'BioDiversityList',
    components: {
    },
    onMounted () {
      console.log('mounted')
      // [TODO:]
      // fetch('/api/interval').then((res: Response) => res.json()).then((data: TInterval[]) => {
    },

    // onCreated () {
    //   console.log('created')
    //   fetch('/api/interval').then((res: Response) => res.json()).then((data: TInterval[]) => {
    //     console.log('data', data)
    //     intervals.value.push(...data)
    //   })
    // },
    data () {
      return { intervals }
    },
  }

</script>
<script lang="ts" setup>
const appStore = useAppStore()
const diversity = reactive<TDiversity[]>([])
const loading = ref<boolean>(false)

const onDetailsButtonClicked = (data: TInterval) => {
    console.log(`onDetailsButtonClicked - data:`, data)
    const { intervalNo, intervalName } = data
    selectedInterval.value = intervalName
    router.push(`/diversity/${intervalName}`)
  }
watchEffect(() => {
  loading.value = true
  fetch('/api/occurrence/diversity')
    .then((res: Response) => res.json())
    .then((data: TDiversity[]) => {
      console.log('data', data)
      diversity.push(...data)
      // [TODO:] - store intervals in the store instead on the component ?
      appStore.$patch({ intervals: data })
      loading.value = false
})
})
watch(()=>selectedInterval, (newVal, oldVal) => {
  console.log(`watch - newVal:`, newVal, `oldVal:`, oldVal)
})
</script>
<script lang="ts">

</script>
<style scoped>
  .card {
    padding: 2em;
  }
</style>
