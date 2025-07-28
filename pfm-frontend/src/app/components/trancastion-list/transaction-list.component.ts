import {Component, OnInit} from '@angular/core';
import {CategoryDto, FilterParams, PagedList, TransactionDto} from '../../model/model';
import {TransactionService} from '../../service/transaction-service';
import {NgClass} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {CategoryService} from '../../service/category-service';

@Component({
  selector: 'app-transaction-list',
  imports: [
    NgClass,
    FormsModule
  ],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css'
})
export class TransactionList implements OnInit{
  pagedTransactions: PagedList<TransactionDto> | null = null;
  loading = false;
  error = false;
  currentPage = 1;
  pageSize = 10;
  showFilter = false;
  showCategoryDialog = false;
  selectedTransaction: TransactionDto | null = null;
  selectedMainCategory: string | null = null;
  selectedSubCategory: string | null = null;
  mainCategories: CategoryDto[] = [];
  subCategories: CategoryDto[] = [];
  categoriesMap = new Map<string, CategoryDto[]>();
  filterParams: FilterParams = {
    'sort-by': 'date',
    'sort-order': 'Desc'
  };

  constructor(private service: TransactionService, private categoryService: CategoryService) {}

  ngOnInit() {
    this.loadTransactions();
    this.loadAllCategories();
  }

  toggleFilter() {
    this.showFilter = !this.showFilter;
  }

  resetFilters() {
    this.filterParams = {
      'sort-by': 'date',
      'sort-order': 'Desc'
    };
    this.currentPage = 1;
    this.loadTransactions();
    this.showFilter = false;
  }
  applyFilters() {
    this.currentPage = 1;
    this.loadTransactions();
    this.showFilter = false;
  }

  loadAllCategories() {
    this.categoryService.getCategoriesMap().subscribe({
      next: (map) => {
        console.log('Categories Map:', map);
        this.categoriesMap = map;
        this.mainCategories = map.get('root') || [];
        console.log('Main Categories:', this.mainCategories);
      },
      error: (err) => {
        console.error('Error loading categories:', err);
      }
    });
  }

  loadSubCategories() {
    console.log('Selected Main Category:', this.selectedMainCategory); // Debug
    console.log('Categories Map:', this.categoriesMap); // Debug

    if (this.selectedMainCategory) {
      this.subCategories = this.categoriesMap.get(this.selectedMainCategory) || [];
      console.log('Loaded Subcategories:', this.subCategories); // Debug
    } else {
      this.subCategories = [];
    }
  }


  loadTransactions() {
    this.loading = true;
    this.error = false;
    const params: any = {
      'page-size': this.pageSize,
      'sort-by': this.filterParams['sort-by'],
      'sort-order': this.filterParams['sort-order'],
    };


    if (this.filterParams['transaction-kind'] && this.filterParams['transaction-kind'].length > 0) {
      params['transaction-kind'] = this.filterParams['transaction-kind'];
    }

    if (this.filterParams['start-date']) {
      params['start-date'] = new Date(this.filterParams['start-date']).toISOString();
    }
    if (this.filterParams['end-date']) {
      params['end-date'] = new Date(this.filterParams['end-date']).toISOString();
    }
    this.service.getTransactions(this.currentPage, params)
      .subscribe({
        next: (data) => {
          this.pagedTransactions = data;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error loading transactions:', err);
          this.error = true;
          this.loading = false;
        }
      });
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('sr-RS', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit'
    });
  }

  formatAmount(amount: number): string {
    return new Intl.NumberFormat('sr-RS', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }).format(amount);
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadTransactions();
    }
  }

  nextPage() {
    if (this.pagedTransactions && this.currentPage < this.pagedTransactions.totalPages) {
      this.currentPage++;
      this.loadTransactions();
    }
  }

  goToPage(page: number) {
    if (page !== this.currentPage && page >= 1 &&
      this.pagedTransactions && page <= this.pagedTransactions.totalPages) {
      this.currentPage = page;
      this.loadTransactions();
    }
  }

  getVisiblePages(): number[] {
    if (!this.pagedTransactions) return [];

    const totalPages = this.pagedTransactions.totalPages;
    const current = this.currentPage;
    const pages: number[] = [];

    if (totalPages <= 7) {
      // Ako ima 7 ili manje stranica, prikaži sve
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i);
      }
    } else {
      // Uvek prikaži prvu stranicu
      pages.push(1);

      if (current <= 4) {
        // Početne stranice
        for (let i = 2; i <= 5; i++) {
          pages.push(i);
        }
        pages.push(-1); // ...
        pages.push(totalPages);
      } else if (current >= totalPages - 3) {
        // Krajnje stranice
        pages.push(-1); // ...
        for (let i = totalPages - 4; i <= totalPages; i++) {
          pages.push(i);
        }
      } else {
        // Srednje stranice
        pages.push(-1); // ...
        for (let i = current - 1; i <= current + 1; i++) {
          pages.push(i);
        }
        pages.push(-1); // ...
        pages.push(totalPages);
      }
    }

    return pages;
  }

  getStartItem(): number {
    if (!this.pagedTransactions) return 0;
    return (this.currentPage - 1) * this.pageSize + 1;
  }

  getEndItem(): number {
    if (!this.pagedTransactions) return 0;
    const end = this.currentPage * this.pageSize;
    return Math.min(end, this.pagedTransactions.totalCount);
  }

  getTransactionTypeLabel(kind: string): string {
    const labels: { [key: string]: string } = {
      'dep': 'Deposit',
      'wdw': 'Withdrawal',
      'pmt': 'Payment',
      'fee': 'Fee',
      'inc': 'Income',
      'rev': 'Reversal',
      'adj': 'Adjustment',
      'lnd': 'Loan given',
      'lnr': 'Loan repayment',
      'fcx': 'Foreign exchange',
      'aop': 'Account opening',
      'acl': 'Account closing',
      'spl': 'Split',
      'sal': 'Sale'
    };
    return labels[kind] || kind.toUpperCase();
  }

  kindClasses(kind: string): string {
    const map: Record<string, string> = {
      'dep': 'bg-green-800/80 text-green-100 border-green-700',
      'inc': 'bg-green-800/80 text-green-100 border-green-700',
      'wdw': 'bg-red-800/80 text-red-100 border-red-700',
      'pmt': 'bg-red-800/80 text-red-100 border-red-700',
      'fee': 'bg-red-800/80 text-red-100 border-red-700',
      'fcx': 'bg-blue-800/80 text-blue-100 border-blue-700',
      'lnd': 'bg-blue-800/80 text-blue-100 border-blue-700',
      'lnr': 'bg-blue-800/80 text-blue-100 border-blue-700',
      'rev': 'bg-amber-800/80 text-amber-100 border-amber-700',
      'adj': 'bg-amber-800/80 text-amber-100 border-amber-700',
      'aop': 'bg-purple-800/80 text-purple-100 border-purple-700',
      'acl': 'bg-purple-800/80 text-purple-100 border-purple-700'
    };
    return map[kind] || 'bg-gray-800/80 text-gray-100 border-gray-700';
  }

  openCategoryDialog(transaction: TransactionDto) {
    this.selectedTransaction = transaction;
    this.selectedMainCategory = null;
    this.selectedSubCategory = null;
    this.subCategories = [];
    this.showCategoryDialog = true;
  }

  assignCategory() {
    if (!this.selectedTransaction || !this.selectedMainCategory) return;

    const categoryCode = this.selectedSubCategory || this.selectedMainCategory;

    this.service.categorizeTransaction(
      this.selectedTransaction.id,
      { catcode: categoryCode }
    ).subscribe();

    if (this.pagedTransactions?.items) {
      const transaction = this.pagedTransactions.items.find(t => t.id === this.selectedTransaction?.id);
      if (transaction) {
        transaction.catCode = categoryCode;
      }
    }
    this.showCategoryDialog = false;
  }
}
