import React from 'react';

function ReportGeneration() {
  const generateReport = (type) => {
    console.log(`Génération du rapport : ${type}`);
    // Appel API pour générer le rapport (retournés, empruntés, réservés)
  };

  return (
    <div>
      <h2>Génération de rapports</h2>
      <button onClick={() => generateReport('retournés')}>Rapport des livres retournés</button>
      <button onClick={() => generateReport('empruntés')}>Rapport des livres empruntés</button>
      <button onClick={() => generateReport('réservés')}>Rapport des livres réservés</button>
    </div>
  );
}

export default ReportGeneration;
