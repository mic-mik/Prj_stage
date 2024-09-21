import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Header from "./Header_temp";
import Footer from "./Footer";
function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const userData = {
      email: email,
      password: password,
    };

    try {
      const response = await axios.post(
        "http://localhost:5000/api/connexion",
        userData
      );

      if (response.data.success) {
        setMessage(
          `Bienvenue ${response.data.user.Nom} ${response.data.user.Prenom}`
        );
        // Stocker les informations de l'utilisateur dans localStorage
        localStorage.setItem("user", JSON.stringify(response.data.user));
        // Rediriger vers la page d'accueil du membre
        navigate("/accueil");
      } else {
        setMessage("Email ou mot de passe incorrect");
      }
    } catch (error) {
      console.error("Erreur lors de la connexion:", error);
      setMessage("Une erreur est survenue. Veuillez réessayer plus tard.");
    }
  };

  return (
    <div>
      <h2>Connexion</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Mot de passe:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit">Se connecter</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}

export default Login;
