import axios from "axios"

export class DoctorService {
    httpClient
    user

    constructor(user) {
        this.user = user
        this.httpClient = axios.create({
            baseURL: 'https://localhost:5021',
            timeout: 3000
        });
    }

    generateAuthHeaders() {
        return {
            Authorization: `Bearer ${this.user.token}`
        }
    }

    async getDoctors() {
        const response = await this.httpClient.get("/doctors/get")
        return response?.data
    }

    async getAllDoctors() {
        const response = await this.httpClient.get("/doctors/getAll", {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }

}