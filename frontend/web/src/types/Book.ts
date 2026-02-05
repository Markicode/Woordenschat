import type { Author } from "../types/Author";

export interface Book {
  id: number;
  title: string;
  authors: Author[];
}

export interface CreateBook {
  title: string;
  authors: number[];
}