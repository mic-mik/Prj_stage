from flask import Blueprint, Flask, request, jsonify
from Controller_User import reservation, create_book, borrow_book, get_all_books, return_book
action_pb = Blueprint('action', __name__)

#Route pour livre
@action_pb.route('/api/livre', methods=['POST'])
def create_livre_route():
    data = request.json
    success, message = create_book(data)
    if success:
        return jsonify({'success': True, 'message': message}), 201
    else:
        return jsonify({'success': False, 'message': message}), 500

#Route pour livre
@action_pb.route('/api/livre', methods=['GET'])
def get_all_livres_route():
    return jsonify({'success': True, 'data': get_all_books()}), 200

# Route pour la reservation
@action_pb.route('/api/reservation', methods=['POST'])
def reservation_route():
    data = request.json
    success, message = reservation(data)
    if success:
        return jsonify({'success': True, 'message': message}), 201
    else:
        return jsonify({'success': False, 'message': message}), 500

# Route pour l`emprunt
@action_pb.route('/api/livre/emprunt', methods=['POST'])
def borrow_route():
    data = request.json
    success, message = borrow_book(data)
    if success:
        return jsonify({'success': True, 'message': message}), 201
    else:
        return jsonify({'success': False, 'message': message}), 500

# Route pour l`emprunt
@action_pb.route('/api/livre/return', methods=['PATCH'])
def return_book_route():
    data = request.json
    success, message = return_book(data)
    if success:
        return jsonify({'success': True, 'message': message}), 201
    else:
        return jsonify({'success': False, 'message': message}), 500