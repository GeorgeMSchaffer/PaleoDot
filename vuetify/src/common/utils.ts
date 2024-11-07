import { EnumEntityType, TPaginationSettings, TQueryFilterField, TRequestParams } from './types'

export function getEnumKeys<
  T extends string,
  TEnumValue extends string | number
> (enumVariable: { [key in T]: TEnumValue }) {
  return Object.keys(enumVariable) as Array<T>
}

export const buildApiUrl = (
  entity: EnumEntityType,
  filters: TQueryFilterField[],
  pagination: TPaginationSettings
): string => {
  const valueMap: { [key: string]: any[] } = {}

  filters &&
    filters.map((filter: TQueryFilterField) => {
      if (valueMap[filter.field]) {
        valueMap[filter.field].push(filter.value)
      } else {
        valueMap[filter.field] = [filter.value]
      }
      return ''
    })
  const filterKeys = Object.keys(valueMap)

  const { take, skip, orderBy, orderDir } = pagination
  //  const start = (page * perPage);

  // if there is no sortby we omit it
  const orderClause = (orderBy) ? `&orderBy=${orderBy}&orderDir=${orderDir}` : ''

  let url = `/api/${entity}/?take=${take}&skip=${skip}${orderClause}`
  filters.forEach((filter: TQueryFilterField) => {
    const operator = filter.operator || '='; // Default to '=' if no operator is provided
    url += `${filter.field}${operator}${filter.value}&`
  })
  // for (const filter of filters) {
  //   url += `${filter.field}${filter.operator}${filter.value}&`;
  // }
  return url ?? ''
}

export function buildRequestParams (query: TRequestParams) {
  const params: TRequestParams = {
    take: (query?.take ?? 10),
    skip: query?.skip ?? 0,
    orderBy: query?.orderBy ?? '', // there is no value that would work with all entities, so leave it up to the caller to set the value.
    orderDir: query?.orderDir ?? 'ASC',
    queryParams: {},
  }
  const filterKeys = Object.keys(query)
  console.log('🚀 ~ buildRequestParams ~ query:', query)
  console.log('🚀 ~ UTILS ~ buildRequestParams ~ filterKeys:', filterKeys)
  if (filterKeys.length) {
    filterKeys.forEach((key:string) => {
      // Ignore pagination and sorting params
      if (
        key === 'take' ||
        key === 'skip' ||
        key === 'orderBy' ||
        key === 'orderDir'
      ) {
        return
      }
      if (query?.queryParams && query?.queryParams[key]) {
        return query.queryParams[key]
      }
      const filterValue = query?.queryParams?.[key]
      // const filterValue = query?.queryParams && query.queryParams[key]
      // [TODO] checking the entity for a valid field via hasOwnProperty does not work with the entity objects so we need to find a way to validate the keys

      if (filterValue?.length && query) {
        // Add your custom query handling logic here
      }
    })
  }
  return params
}
