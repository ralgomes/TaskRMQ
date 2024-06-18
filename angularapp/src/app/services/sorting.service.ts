import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class SortingService {
  sortColumn: string = '';
  sortDirection: string = 'asc';
}
