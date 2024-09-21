from Connexion import connection
from datetime import datetime, timedelta

def connexion_user(data):
    conn=connection()
    email=data.get('email')
    password=data.get("password")
    # Vérification si l'utilisateur existe déjà dans la base de données
    cur = conn.cursor()
    cur.execute("SELECT * FROM UTILISATEURS WHERE email = %s AND MOT_DE_PASSE = %s", (email, password))
    # cur.execute(f"SELECT * FROM UTILISATEURS WHERE email = '{email}' AND MOT_DE_PASSE = '{password}'")
    result = cur.fetchone()
    if result:
        user = {
            'ID_User': result[0],  # Supposons que la première colonne est l'ID de l'utilisateur
            'Nom': result[1],
            'Prenom': result[2],
            'email': result[3],
            'Pswd':result[4],
            'Adresse':result[5],
            'Telephone':result[6],
            'Sexe':result[7]
                # Ajoutez d'autres champs utilisateur si nécessaire
            }
        cur.close()
        conn.close()
        return user,True
    else:
        return None,False

def get_all_books():
    conn=connection()
    # Vérification si l'utilisateur existe déjà dans la base de données
    cur = conn.cursor()
    cur.execute("SELECT * FROM LIVRES")
    # cur.execute(f"SELECT * FROM UTILISATEURS WHERE email = '{email}' AND MOT_DE_PASSE = '{password}'")
    result = cur.fetchall()
    mapped = []
    for item in result:
        mapped.append({
            'id': item[0],
            'nom': item[1],
            'autheur':item[2],
            'annee':item[3],
            'category_id':item[4],
            'chemin_livre':item[5],
        })
    return mapped

#inscription 
#     
def inscription_user(data):
    conn = connection()
    email = data.get('email')
    password = data.get('password')
    nom = data.get('nom')
    prenom = data.get('prenom')
    adresse = data.get('adresse')
    telephone = data.get('telephone')
    sexe = data.get('sexe')

    cur = conn.cursor()

    # Vérification si l'utilisateur existe déjà
    cur.execute("SELECT * FROM UTILISATEURS WHERE email = %s", (email,))
    result = cur.fetchone()
    
    if result:
        cur.close()
        conn.close()
        return None, False, "L'utilisateur existe déjà."

    try:
        # Insertion du nouvel utilisateur
        cur.execute(
            "INSERT INTO UTILISATEURS (NOM, PRENOM, EMAIL, MOT_DE_PASSE, ADRESSE, TELEPHONE, SEXE) VALUES (%s, %s, %s, %s, %s, %s, %s)",
            (nom, prenom, email, password, adresse, telephone, sexe)
        )
        conn.commit()

        # Récupérer l'utilisateur nouvellement créé
        cur.execute("SELECT * FROM UTILISATEURS WHERE email = %s", (email,))
        new_user = cur.fetchone()

        user = {
            'ID_User': new_user[0],
            'Nom': new_user[1],
            'Prenom': new_user[2],
            'email': new_user[3],
            'Adresse': new_user[5],
            'Telephone': new_user[6],
            'Sexe': new_user[7]
        }

        cur.close()
        conn.close()
        return user, True, "Inscription réussie."
    except Exception as e:
        cur.close()
        conn.close()
        return None, False, f"Erreur lors de l'inscription : {str(e)}"
    

#inscription 
#     
def reservation(data):
    conn = connection()
    user_id = data.get('user_id')
    book_id = data.get('book_id')
    reservation_date = data.get('reservation_date')
    return_date = data.get('return_date')

    cur = conn.cursor()
    try:
        # Insertion du nouvel utilisateur
        cur.execute(
            "INSERT INTO RESERVATION (ID_MEMBRE, ID_LIVRE, DATE_EMPRUNT, DATE_RETOUR) VALUES (%s, %s, TO_DATE(%s), TO_DATE(%s))",
            (user_id, book_id, reservation_date, return_date)
        )
        conn.commit()

        cur.close()
        conn.close()
        return  True, "Reservation réussie."
    except Exception as e:
        cur.close()
        conn.close()
        return False, f"Erreur de reservation : {str(e)}"
#creation dun livre 
#     
def create_book(data):
    conn = connection()
    title = data.get('title')
    author = data.get('author')
    year = data.get('year')
    category_id = data.get('category_id')
    book_path = data.get('book_path')

    cur = conn.cursor()
    try:
        # Insertion du nouvel utilisateur
        cur.execute(
            "insert into LIVRES (TITRE, AUTEUR, ANNEE, ID_CATEGORIE, CHEMIN_LIVRE) VALUES (%s, %s, %s, %s, %s);",
            (title, author, year, category_id, book_path)
        )
        conn.commit()

        cur.close()
        conn.close()
        return  True, "Livre ajoute."
    except Exception as e:
        cur.close()
        conn.close()
        return False, f"Erreur d`ajout du Livre : {str(e)}"
    
#inscription 
#     
def borrow_book(data):
    conn = connection()
    reservation_id = data.get('reservation_id')
    borrow_date = data.get('borrow_date')

    cur = conn.cursor()
    try:
        # Insertion du nouvel utilisateur
        cur.execute(
            "INSERT INTO EMPRUNT (ID_RESERVATION, DATE_EMPRUNT) VALUES (%s, TO_DATE(%s))",
            (reservation_id, borrow_date)
        )
        conn.commit()

        cur.close()
        conn.close()
        return  True, "Emprunt réussie."
    except Exception as e:
        cur.close()
        conn.close()
        return False, f"Erreur d`emprunt : {str(e)}"

def return_book(data):
    conn = connection()
    reservation_id = data.get('reservation_id')
    return_date = data.get('return_date')

    cur = conn.cursor()
    try:
        # Insertion du nouvel utilisateur
        cur.execute(
            "UPDATE RESERVATION SET RETURNED_DATE = TO_DATE(%s) WHERE ID_RESERVATION = %s",
            (return_date, reservation_id)
        )
        conn.commit()

        cur.close()
        conn.close()
        return  True, "Retour réussie."
    except Exception as e:
        cur.close()
        conn.close()
        return False, f"Erreur de retour : {str(e)}"