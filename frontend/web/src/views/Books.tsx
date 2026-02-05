import { useEffect, useState } from "react";
import { booksService } from "../api/services/books.service";
import { NavLink } from "react-router-dom";
import type { Book } from "../types/Book";

export default function Books() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    booksService
      .getAll()
      .then(setBooks)
      .catch(() => setError("Kon boeken niet laden"))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Laden...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <div>
        <h1>Boeken</h1>
        <NavLink to="/addbook">Boek toevoegen</NavLink>
      </div>
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            <strong>{book.title}</strong> –{" "}
            {book.authors.map((author) => (
              <span key={author.id}>
                {author.firstName} {author.lastName}{" "}
              </span>
            ))}
          </li>
        ))}
      </ul>
    </div>
  );
}
