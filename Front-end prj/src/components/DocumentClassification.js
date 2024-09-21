import React, { useState } from 'react';

function DocumentClassification() {
  const [document, setDocument] = useState({ title: '', category: '' });

  const handleChange = (e) => {
    setDocument({ ...document, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Classify Document:', document);
    // Ici, tu appellerais le service pour classer le document
  };

  return (
    <div>
      <h2>Classification des documents</h2>
      <form onSubmit={handleSubmit}>
        <input name="title" type="text" placeholder="Titre du document" onChange={handleChange} />
        <input name="category" type="text" placeholder="Catégorie" onChange={handleChange} />
        <button type="submit">Classifier</button>
      </form>
    </div>
  );
}

export default DocumentClassification;
