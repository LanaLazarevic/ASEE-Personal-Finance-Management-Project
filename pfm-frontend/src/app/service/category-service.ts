import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable, shareReplay} from 'rxjs';
import {CategoriesResponse, CategoryDto} from '../model/model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'http://localhost:3031/categories';
  private allCategories$!: Observable<CategoryDto[]>;
  private categoriesMap$!: Observable<Map<string, CategoryDto[]>>;

  constructor(private http: HttpClient) {
    this.initCategories();
  }

  private initCategories() {
    this.categoriesMap$ = this.http.get<CategoriesResponse>(this.apiUrl).pipe(
      map(response => {
        const map = new Map<string, CategoryDto[]>();

        response.items.forEach(category => {
          const parentKey = category['parent-code'] ?? 'root';
          if (!map.has(parentKey)) {
            map.set(parentKey, []);
          }
          map.get(parentKey)!.push(category);
        });

        // Debug: Log the map structure
        console.log('Categories Map:', Array.from(map.entries()));
        return map;
      }),
      shareReplay(1)
    );
  }

  getCategoriesMap(): Observable<Map<string, CategoryDto[]>> {
    return this.categoriesMap$;
  }

  getMainCategories(): Observable<CategoryDto[]> {
    return this.categoriesMap$.pipe(
      map(map => map.get('root') || [])
    );
  }

  getSubCategories(parentCode: string): Observable<CategoryDto[]> {
    return this.categoriesMap$.pipe(
      map(map => map.get(parentCode) || [])
    );
  }
}
