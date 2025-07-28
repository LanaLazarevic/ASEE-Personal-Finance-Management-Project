export interface TransactionDto {
  id: string;
  date: string;
  direction: string;
  amount: number;
  beneficiaryName?: string;
  description?: string;
  currency: string;
  mcc?: string;
  kind: string;
  catCode?: string;
  splits: SplitItemDto[];
}

export interface SplitItemDto {
  catCode: string;
  amount: number;
}

export interface PagedList<T> {
  items: T[];
  totalCount: number;
  pageSize: number;
  page: number;
  totalPages: number;
  sortOrder: string;
  sortBy: string;
}

export interface TransactionDtoRaw {
  id: string;
  date: string;
  direction: string;
  amount: number;
  'beneficiary-name'?: string;
  description?: string;
  currency: string;
  mcc?: string;
  kind: string;
  catcode?: string;
  splits: SplitItemDtoRaw[];
}

export interface PagedListRaw<T> {
  items: T[];
  'total-count': number;
  'page-size': number;
  page: number;
  'total-pages': number;
  'sort-order': string;
  'sort-by': string;
}

export interface SplitItemDtoRaw {
  catcode: string;
  amount: number;
}

export interface FilterParams {
  'transaction-kind'?: string[];
  'start-date'?: string;
  'end-date'?: string;
  'sort-by'?: string;
  'sort-order'?: string;
}

export interface CategoryDto {
  'parent-code': string;
  code: string;
  name: string;
}

export interface CategoriesResponse {
  items: CategoryDto[];
}
