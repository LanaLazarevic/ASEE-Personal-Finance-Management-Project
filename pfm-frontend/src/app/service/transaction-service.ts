import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {PagedList, PagedListRaw, TransactionDto, TransactionDtoRaw} from '../model/model';
import {data} from 'autoprefixer';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = 'http://localhost:3031/transactions';
  constructor(private http: HttpClient) {}


  getTransactions(
    page: number, params?: any
  ): Observable<PagedList<TransactionDto>> {
    let queryParams = new HttpParams()
      .set('page', page.toString());
    if (params) {
      for (const key in params) {
        if (params[key] !== undefined && params[key] !== null) {
          if (key === 'transaction-kind' && Array.isArray(params[key])) {
            queryParams = queryParams.set(key, params[key].join(','));
          } else {
            queryParams = queryParams.set(key, params[key]);
          }
        }
      }
    }
    return this.http
      .get<PagedListRaw<TransactionDtoRaw>>(this.apiUrl, { params: queryParams })
      .pipe(
        map(raw => ({
          items: raw.items.map(r => ({
            id: r.id,
            date: r.date,
            direction: r.direction,
            amount: r.amount,
            beneficiaryName: r['beneficiary-name'],
            description: r.description,
            currency: r.currency,
            mcc: r.mcc,
            kind: r.kind,
            catCode: r.catcode,
            splits: r.splits?.map(s => ({
              catCode: s.catcode,
              amount: s.amount
            }))
          })),
          totalCount: raw['total-count'],
          pageSize: raw['page-size'],
          page: raw.page,
          totalPages: raw['total-pages'],
          sortOrder: raw['sort-order'],
          sortBy: raw['sort-by'],
        }))
      );
  }

  categorizeTransaction(transactionId: string, body: { catcode: string }): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/${transactionId}/categorize`,
      body,
      { responseType: 'text' }
    );
  }
}

