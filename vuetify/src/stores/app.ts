// Utilities
import { defineStore } from 'pinia'
import { TDiversity, TInterval, TOccurrence } from '@/common/types'
import Occurrences from '@/pages/occurrences.vue'
export const useAppStore = defineStore('app', {
  state: () => ({
    intervals: [] as TInterval[],
    occurrences: [] as TOccurrence[],
    diversity: [] as TDiversity[],
  }),
  actions: {
    setIntervals (intervals: TInterval[]) {
      this.intervals = intervals
    },
    setOccurrences (occurrences: TOccurrence[]) {
      this.occurrences = occurrences
    },
    setDiversity (diversity: TDiversity[]) {
      this.diversity = diversity
    },
  },
})
