import React, { useState } from 'react';

function EncaisserFrais() {
  const [frais, setFrais] = useState({ memberId: '', amount: '', method: 'credit_card' });

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Encaissement de frais :', frais);
    // Logique pour intégrer une API de paiement (comme PayPal ou Stripe)
  };

  const handleChange = (e) => {
    setFrais({ ...frais, [e.target.name]: e.target.value });
  };

  return (
    <div>
      <h2>Encaisser les frais</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>ID du Membre</label>
          <input 
            type="text" 
            name="memberId" 
            placeholder="ID du membre" 
            value={frais.memberId} 
            onChange={handleChange} 
            required 
          />
        </div>
        
        <div>
          <label>Montant à payer</label>
          <input 
            type="number" 
            name="amount" 
            placeholder="Montant" 
            value={frais.amount} 
            onChange={handleChange} 
            required 
          />
        </div>
        
        <div>
          <label>Méthode de paiement</label>
          <select name="method" value={frais.method} onChange={handleChange}>
            <option value="credit_card">Carte de crédit</option>
            <option value="debit_card">Carte de débit</option>
            <option value="paypal">PayPal</option>
          </select>
        </div>
        
        <button type="submit">Encaisser</button>
      </form>
    </div>
  );
}

export default EncaisserFrais;
