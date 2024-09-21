import React, { useState } from 'react';

function ReturnBook() {
  const [returnInfo, setReturnInfo] = useState({ bookId: '', memberId: '' });

  const handleChange = (e) => {
    setReturnInfo({ ...returnInfo, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Retour du livre:', returnInfo);
    // Appel API pour retourner un livre
  };

  return (
    <div>
      <h2>Retourner un livre</h2>
      <form onSubmit={handleSubmit}>
        <input name="bookId" type="text" placeholder="ID du livre" onChange={handleChange} />
        <input name="memberId" type="text" placeholder="ID du membre" onChange={handleChange} />
        <button type="submit">Retourner</button>
      </form>
    </div>
  );
}

export default ReturnBook;
