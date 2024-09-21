import React, { useState } from 'react';

function BarcodeScanner() {
  const [barcode, setBarcode] = useState('');

  const handleChange = (e) => {
    setBarcode(e.target.value);
  };

  const handleScan = () => {
    console.log('Scan:', barcode);
    // Appel API pour traiter les informations scannées
  };

  return (
    <div>
      <h2>Scanner un code-barres</h2>
      <input type="text" placeholder="Code-barres" onChange={handleChange} />
      <button onClick={handleScan}>Scanner</button>
    </div>
  );
}

export default BarcodeScanner;
