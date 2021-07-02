import axios from "axios"

export class AuthService {
    user
    httpClient

    constructor() {
        this.httpClient = axios.create({
            baseURL: 'https://localhost:5021',
            timeout: 3000,
        });
        const localStorageToken = localStorage.getItem('hospital_user')
        if (localStorageToken) {
            this.user = JSON.parse(localStorageToken)
        }
    }

    getUser() {
        const localStorageToken = localStorage.getItem('hospital_user')
        if (localStorageToken) {
            this.user = JSON.parse(localStorageToken)
        }
        return this.user
    }

    async login(username, password) {
        const response = await this.httpClient.post("/login", {
            username, password
        })
        if (response.status === 200)
            localStorage.setItem('hospital_user', JSON.stringify(response.data));
        return response
    }

    async logout() {
        localStorage.removeItem('hospital_user');
    }

}