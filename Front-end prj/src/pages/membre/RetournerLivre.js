import React from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer';
import ReturnBook from '../components/ReturnBook';

function ReturnBookPage() {
  return (
    <div>
      <Header />
      <main>
        <h1>Retourner un livre</h1>
        <ReturnBook />
      </main>
      <Footer />
    </div>
  );
}

export default ReturnBookPage;
