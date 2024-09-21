import React from "react";
import { Link } from "react-router-dom";
import "./Header.css";  // Assure-toi que le chemin vers le fichier CSS est correct

function Header() {
  return (
    <header className="header">
      <nav className="navbar">
        <Link to="/" className="navbar-brand">Bibliotheque</Link>
        <ul className="nav-list">
          <li className="nav-item">
            <Link to="/" className="nav-link">
              Home
            </Link>
          </li>
          {/* <li className="nav-item"> */}
            {/* <Link to="/gerant/dashboard" className="nav-link"> */}
              {/* Gérant Dashboard */}
            {/* </Link> */}
          {/* </li> */}
          {/* <li className="nav-item"> */}
            {/* <Link to="/prepose/dashboard" className="nav-link"> */}
              {/* Préposé Dashboard */}
            {/* </Link> */}
          {/* </li> */}
          {/* <li className="nav-item"> */}
            {/* <Link to="/membre/dashboard" className="nav-link"> */}
              {/* Membre Dashboard */}
            {/* </Link> */}
          {/* </li> */}
          <li className="nav-item">
            <Link to="/login" className="nav-link">
              Connexion
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/register" className="nav-link">
              Inscription
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;
