import React, { useState } from 'react';

function BookBorrowing() {
  const [borrowData, setBorrowData] = useState({ bookId: '', memberId: '' });

  const handleChange = (e) => {
    setBorrowData({ ...borrowData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Emprunt de livre:', borrowData);
    // Ici, tu appellerais une API pour enregistrer l'emprunt du livre
    // Exemple : bookService.borrowBook(borrowData.bookId, borrowData.memberId)
  };

  return (
    <div>
      <h2>Emprunter un livre</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>ID du Livre</label>
          <input
            name="bookId"
            type="text"
            placeholder="Entrez l'ID du livre"
            onChange={handleChange}
            value={borrowData.bookId}
          />
        </div>
        <div>
          <label>ID du Membre</label>
          <input
            name="memberId"
            type="text"
            placeholder="Entrez votre ID de membre"
            onChange={handleChange}
            value={borrowData.memberId}
          />
        </div>
        <button type="submit">Emprunter</button>
      </form>
    </div>
  );
}

export default BookBorrowing;
