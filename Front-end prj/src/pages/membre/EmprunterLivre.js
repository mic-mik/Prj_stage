import React from "react";
import Header from "../../components/Header_temp";
import Footer from "../../components/Footer";
import BookBorrowing from "../../components/BookBorrowing";

function EmprunterLivre() {
  return (
    <div>
      <Header />
      <main>
        <h1>Emprunter un livre</h1>
        <BookBorrowing />
      </main>
      <Footer />
    </div>
  );
}

export default EmprunterLivre;
