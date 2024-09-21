import React, { useState } from 'react';

function BookReservation() {
  const [reservation, setReservation] = useState({ bookId: '', memberId: '' });

  const handleChange = (e) => {
    setReservation({ ...reservation, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Réservation:', reservation);
    // Appel API pour réserver un livre
  };

  return (
    <div>
      <h2>Réserver un livre</h2>
      <form onSubmit={handleSubmit}>
        <input name="bookId" type="text" placeholder="ID du livre" onChange={handleChange} />
        <input name="memberId" type="text" placeholder="ID du membre" onChange={handleChange} />
        <button type="submit">Réserver</button>
      </form>
    </div>
  );
}

export default BookReservation;
