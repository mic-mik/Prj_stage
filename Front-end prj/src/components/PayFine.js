import React, { useState } from 'react';

function PayFine() {
  const [paymentInfo, setPaymentInfo] = useState({ memberId: '', amount: '' });

  const handleChange = (e) => {
    setPaymentInfo({ ...paymentInfo, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Paiement de l\'amende:', paymentInfo);
    // Appel API pour traiter le paiement (carte, PayPal, etc.)
  };

  return (
    <div>
      <h2>Payer une amende</h2>
      <form onSubmit={handleSubmit}>
        <input name="memberId" type="text" placeholder="ID du membre" onChange={handleChange} />
        <input name="amount" type="text" placeholder="Montant" onChange={handleChange} />
        <button type="submit">Payer</button>
      </form>
    </div>
  );
}

export default PayFine;
