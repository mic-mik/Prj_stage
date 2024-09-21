import React, { useState } from 'react';

function PayerAmende() {
  const [amount, setAmount] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Amende payée:', amount);
    // Appel API pour payer l'amende
  };

  return (
    <div>
      <h2>Payer une amende</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" placeholder="Montant" value={amount} onChange={(e) => setAmount(e.target.value)} />
        <button type="submit">Payer</button>
      </form>
    </div>
  );
}

export default PayerAmende;
