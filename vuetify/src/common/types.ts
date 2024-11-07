export enum EnumIntervalType {
  eon = 'eon',
  era = 'era',
  period = 'period',
  epoch = 'epoch',
  age = 'age',
  int = 'int',
}

// domain->kingdom->phylum->class->order->family->genus->species
export enum EnumRanks {
  Domain = 'Domain',
  Kingdom = 'Kingdom',
  Phylum = 'Phylum',
  Class = 'Class',
  Order = 'Order',
  Family = 'Family',
  Genus = 'Genus',
  Species = 'Species',
}

export enum EnumDomains {
  EUKARYA = 'Eukarya',
  BACTERIA = 'Bacteria',
  ARCHAEA = 'Archaea',
}

export enum EnumKingdoms {
  ANIMALIA = 'Animalia',
  PLANTAE = 'Plantae',
  FUNGI = 'Fungi',
  PROTISTA = 'Protista',
  MONERA = 'Monera',
}

export enum EnumClades {
  Domain = 'Domain',
  Kingdom = 'KingdoM',
  Pylum = 'Pyhylum',
  Class = 'Class',
  Order = 'Order',
  Family = 'Family',
  Genus = 'Genus',
  Species = 'Species',
}
export enum EnumPhylums {
  CHORDATA = 'Chordata',
  ARTHROPODA = 'Arthropoda',
  MOLLUSCA = 'Mollusca',
  ECHINODERMATA = 'Echinodermata',
  CNIDARIA = 'Cnidaria',
  PORIFERA = 'Porifera',
  PLANTAE = 'Plantae',
  FUNGI = 'Fungi',
  PROTISTA = 'Protista',
  MONERA = 'Monera',
}

export enum EnumClade {
  Mammalia = 'Mammalia',
  Aves = 'Aves',
  Reptilia = 'Reptilia',
  Amphibia = 'Amphibia',
  Pisces = 'Pisces',
  Insecta = 'Insecta',
  Arachnida = 'Arachnida',
  Crustacea = 'Crustacea',
  Annelida = 'Annelida',
  Chordata = 'Chordata',
  Arthropoda = 'Arthropoda',
  Mollusca = 'Mollusca',
  Echinodermata = 'Echinodermata',
  Cnidaria = 'Cnidaria',
  Porifera = 'Porifera',
  Platyhelminthes = 'Platyhelminthes',
  Nematoda = 'Nematoda',
}

export enum EnumEntityType {
  Occurrence = 'occurrence',
  Taxa = 'taxa',
  Prevalence = 'prevalence',
  Diversity = 'diversity',
  Interval = 'interval',
}

export enum EnumMessageType {
  INFO,
  WARNING,
  ERROR,
}

export type TRequestParams = {
  take: number;
  skip: number;
  orderBy: string;
  orderDir: string;
  queryParams: {
    [key: string]: string;
  }
}

export type TPaginationSettings = {
  take: number;
  skip: number;
  orderBy: string;
  orderDir: string;

}

// [TODO:] Revisit the idea is to have a single object for filtering and pagination via the request object
export type TRequest = {
  pagination: TPaginationSettings;
  queryParams?: {
    [key: string]: string;
  }
}

export type TQueryFilterField = {
  field: string;
  value: string | string[];
  operator: string;
  type?: string;
  label?: string;
}

export type TPrevalence = {
  taxonNo: number;
  taxonName: string;
  taxonRank: string;
  imageNo: number;
  numOccurances: number;
}

export type TPrevalenceDTO = {
  taxon_no: number;
  taxon_name: string;
  taxon_rank: string;
  image_no: number;
  num_occurances: number;
}

export type TDiversityDTO = {
  interval_name: string;
  min_ma: number | null;
  max_ma: number | null;
  color: string | null;
  count_of_phylum: number;
  count_of_classes: number;
  count_of_orders: number;
  count_of_families: number;
  count_of_genera: number;
  count_of_occurences: number;
}

export type TDiversity = {
  intervalName: string;
  countOfOccurrences: number | null;
  countOfFamilies: number | null;
  countOfClasses: string | null;
  countOfPhyla: number;
  countOfOrders: number;
  countOfGenera: number;
  maxMa: number;
  minMa: number;

}

// export interface Diversity {
//   intervalNo: number;
//   intervalName: string;
//   maxMya: number;
//   minMya: number;
//   xFt: number;
//   xBL: number;
//   xFL: number;
//   xBt: number;
//   sampledInBin: number;
//   impliedInBin: number;
//   numOccurances: number;
// }

// export interface DiversityJSON {
//   interval_no: number;
//   interval_name: string;
//   max_mya: number;
//   min_mya: number;
//   x_Ft: number;
//   x_bL: number;
//   x_FL: number;
//   x_Bt: number;
//   sampled_in_bin: number;
//   implied_in_Bin: number;
//   num_occs: number;
// }
export type TTaxa = {
  taxonNo: number;
  recordType: string;
  taxonRank: string;
  taxonName: string;
  taxonAttr: string;
  acceptedNo: number;
  acceptedRank: string;
  acceptedName: string;
  parentNo: number;
  referenceNo: number;
  isExtant: string;
  numOccurances: number;
}
export type TaxaDTO = {
  taxon_no: number;
  record_type: string;
  taxon_rank: string;
  taxon_name: string;
  taxon_attr: string;
  accepted_no: number;
  accepted_rank: string;
  accepted_name: string;
  parent_no: number;
  reference_no: number;
  is_extant: string;
  n_occs: number;
}
export type TOccurrence= {
  occurrenceNo: number;
  recordType: string;
  collectionNo: number;
  identifiedName: string;
  identifiedRank: string;
  identifiedNo: number;
  acceptedName: string;
  acceptedRank: string;
  acceptedNo: number;
  earlyInterval: string;
  earlyIntervalNo: number;
  lateInterval: string;
  lateIntervalNo: number;
  maxMa: number;
  minMa: number;
  referenceNo: number;
  cc: string;
  latlngBasis: string;
  latlngPrecision: number;
  geogscale: string;
  phylum: EnumPhylums;
  class: string;
  order: string;
  family: string
  genus: string;
  abundUnit?: string;
  taxonEnvironment?: string;
  environmentBasis?: string;
  motility?: string;
  lifeHabit?: string;
  vision?: string;
  diet?: string;
  reproduction?: string;

  ontogeny?: string;
  timeContain?: string;
  timeMajor?: string;
  timeBuffer?: string;
  timeOverlap?: string;
  formation?: string;
  stratGroup?: string;
  member?: string;
}
export type TOccurrenceDTO = {
  occurrence_no: number;
  record_type: string;
  collection_no: number;
  identified_name: string;
  identified_rank: string;
  identified_no: number;
  accepted_name: string;
  accepted_rank: string;
  accepted_no: number;
  early_interval: string;
  late_interval: string;
  max_mya: number;
  min_mya: number;
  reference_no: number;
  cc: string;
  latlng_basis: string;
  latlng_precision: number;
  geogscale: string;
  phylum: EnumPhylums;
  class: string;
  order: string;
  family: string;
  genus: string;
}
export type TInterval = {
  intervalNo: number;
  recordType: EnumIntervalType;
  intervalName: string;
  abbrv: string;
  parentNo: number;
  color: string;
  maxMa: number;
  minMa: number;
  referenceNo: number;
  scaleNo: number;
}


export type TIntervalDTO = {
  interval_no: number;
  record_type: EnumIntervalType;
  interval_name: string;
  abbrv: string;
  parent_no: number;
  color: string;
  max_mya: number;
  min_mya: number;
  reference_no: number;
}

export type TChartData = {
  labels: string[];
  datasets: {
    label: string;
    data: number[];
    backgroundColor: string;
  }[];
}
export type TChartDataItem = {
  category: string;
  value: number;
}

export type TMessage = {
  message: string;
  type: EnumMessageType;
}
export interface IError extends IMessage {
  code: number;
  statckTrace?: string;
}

export interface IQueryFilterField {
  field: string;
  value: string | string[];
  operator: string;
  type?: string;
  label?: string;
}

export interface IOccurancesFilterField extends IQueryFilterField {
  field: EnumOccuranceFilterFields;
// }
// export interface IIntervalsFilterField extends IFilterField {
//   field: IntervalJSON;
// }
}
