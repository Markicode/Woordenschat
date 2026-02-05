import { api } from "../axios";
import type { Book, CreateBook } from "../../types/Book"

export const booksService = {
  getAll: async (): Promise<Book[]> => {
    const response = await api.get<Book[]>("/books");
    return response.data;
  },

  add: async (book: CreateBook): Promise<Book> => {
    const response = await api.post<Book>("/books", book);
    return response.data;
  }
};
