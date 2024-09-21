import React, { useState } from 'react';

function ConsulterLivres() {
  const [livres, setLivres] = useState([
    { id: 1, title: 'Livre A' },
    { id: 2, title: 'Livre B' },
  ]);

  return (
    <div>
      <h2>Mes livres empruntés</h2>
      <ul>
        {livres.map((livre) => (
          <li key={livre.id}>
            {livre.title}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ConsulterLivres;
