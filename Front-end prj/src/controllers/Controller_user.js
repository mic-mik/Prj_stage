import axios from "axios";
//stokage   de l'url de notre api dans une variable
const url = 'http://127.0.0.1:5000/api'
//fonction de login d,un utilisateur
export async function loginUser(email, password) {
    try {
        //creation d'un json avec les informations du login
        const body = {
            'email': email,
            'password': password
        }
        //appel a la route login avec les parametre requis (url et notre json body)
        const { data } = await axios.post(`${url}/loginUser`, body)
        console.log(data.data)
        if (data.success === false) {
            return false
        }
        // si l'authentification se passe bien
        sessionStorage.setItem('id_user', data.data.ID_User)
        sessionStorage.setItem('nom_user', data.data.Nom)
        sessionStorage.setItem('prenom_user', data.data.Prenom)  //creation d'une variable de session token de l'utilisateur conn
        sessionStorage.setItem('email_user', data.data.email)
        sessionStorage.setItem('telephone_user', data.data.Telephone)
        sessionStorage.setItem('pswd_user', data.data.Pswd)
        sessionStorage.setItem('adresse', data.data.Adresse)
        return true
    } catch (error) {
        //message d'erreur en cas de bad authentification
 
        throw error;
    }
}
 

