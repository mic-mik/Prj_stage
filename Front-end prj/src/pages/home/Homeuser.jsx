import React from "react";
import Header from "../../components/Header_temp";
import Footer from "../../components/Footer";
import "./Homepage.scss";

function HomePage() {
  return (
    <div>
      <Header />
      <main>
        {/* Section Bannière */}
        <section className="banner">
          <div className="banner-content">
            <h1>Bienvenue à BookManager</h1>
            <p>
              Gérez vos emprunts, réservations et paiements en ligne avec
              facilité.
            </p>
            <div className="banner-search">
              <select className="search-dropdown">
                <option value="catalogue">Catalogue</option>
                <option value="genre">Genre</option>
              </select>
              <input
                type="text"
                placeholder="Saisissez les termes de votre recherche"
                className="search-input"
              />
              <button className="search-btn">🔍</button>
            </div>
          </div>
        </section>

        {/* Section des livres ou des nouveautés */}
        <section className="books-section">
          <h2>Nos Dernières Nouveautés</h2>
          <div className="book-list">
            <div className="book-item">
              <img
                src={require("../../assets/images/backg.jpg")}
                alt="Livre 1"
              />
              <h3>Neutraliser</h3>
              <p>Découvrez ce livre passionnant...</p>
            </div>
            <div className="book-item">
              <img
                src={require("../../assets/images/image5.jpg")}
                alt="Livre 2"
              />
              <h3>Devenir riche</h3>
              <p>Un voyage incroyable à travers...</p>
            </div>
            <div className="book-item">
              <img
                src={require("../../assets/images/image4.jpg")}
                alt="Livre 3"
              />
              <h3>Il faut que ca change</h3>
              <p>Apprenez de nouvelles choses...</p>
            </div>
          </div>
        </section>

        {/* Section Services avec des effets */}
        <section className="services">
          <h2>Nos Services</h2>
          <div className="services-list">
            <div className="service-item">
              <img
                src={require("../../assets/images/image1.jpg")}
                alt="Service 1"
              />
              <h3>Service de Prêt</h3>
              <p>Empruntez vos livres préférés en toute simplicité.</p>
            </div>
            <div className="service-item">
              <img
                src={require("../../assets/images/image2.jpg")}
                alt="Service 2"
              />
              <h3>Réservations en Ligne</h3>
              <p>Réservez les derniers ouvrages en un clic.</p>
            </div>
            <div className="service-item">
              <img
                src={require("../../assets/images/image3.jpg")}
                alt="Service 3"
              />
              <h3>Classement des Documents</h3>
              <p>Organisez et trouvez rapidement vos documents.</p>
            </div>
          </div>
        </section>
      </main>
      <Footer />
    </div>
  );
}

export default HomePage;
