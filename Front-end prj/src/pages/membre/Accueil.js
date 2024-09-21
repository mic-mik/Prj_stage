import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../styles/Accueil.css"; // Importer les styles CSS pour la page

function Accueil() {
  const [userName, setUserName] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    // Récupérer les informations de l'utilisateur depuis localStorage
    const user = JSON.parse(localStorage.getItem("user"));
    if (user) {
      setUserName(`${user.Nom} ${user.Prenom}`);
    }
  }, []);

  const handleNavigate = (path) => {
    navigate(path);
  };

  return (
    <div className="accueil-container">
      <div className="welcome-section">
        <h1>Bienvenue, {userName} !</h1>
        <p>
          Nous sommes ravis de vous revoir. Accédez à toutes vos fonctionnalités
          en un seul endroit.
        </p>
      </div>

      <div className="actions-section">
        <button
          onClick={() => handleNavigate("/membre/consulter-livres")}
          className="action-button"
        >
          Consulter les Livres
        </button>
        <button
          onClick={() => handleNavigate("/membre/emprunter-livre")}
          className="action-button"
        >
          Emprunter un Livre
        </button>
        <button
          onClick={() => handleNavigate("/membre/payer-amende")}
          className="action-button"
        >
          Payer une Amende
        </button>
      </div>
    </div>
  );
}

export default Accueil;
