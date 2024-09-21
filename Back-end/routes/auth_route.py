# routes/auth_routes.py
from flask import Blueprint, request, jsonify
from Controller_User import connexion_user, inscription_user

# Créer un Blueprint pour les routes d'authentification
auth_bp = Blueprint('auth', __name__)

# Route pour la connexion
@auth_bp.route('/api/connexion', methods=['POST'])
def connexion():
    data = request.json
    user, success = connexion_user(data)
    if success:
        return jsonify({'success': True, 'user': user}), 200
    else:
        return jsonify({'success': False, 'message': 'Email ou mot de passe incorrect'}), 401

# Route pour l'inscription
@auth_bp.route('/api/inscription', methods=['POST'])
def inscription():
    data = request.json
    user, success, message = inscription_user(data)
    if success:
        return jsonify({'success': True, 'user': user, 'message': message}), 201
    else:
        return jsonify({'success': False, 'message': message}), 400

