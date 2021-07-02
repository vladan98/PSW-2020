import axios from "axios"
import { RegisterMapper } from "../model/RegisterMapper"

export class PatientService {
    user
    httpClient

    constructor(user) {
        this.httpClient = axios.create({
            baseURL: 'https://localhost:5021',
            timeout: 3000
        });
        this.user = user
    }

    generateAuthHeaders() {
        return {
            Authorization: `Bearer ${this.user.token}`
        }
    }

    async register(userData) {
        const registerUserDTO = RegisterMapper.registerUserDataToRegisterUserDTO(userData)
        const response = await this.httpClient.post("/register", {
            ...registerUserDTO
        })
        return response
    }

    async getAll() {
        const response = await this.httpClient.get("/patient/all", {
            headers: this.generateAuthHeaders()
        })
        return response.data
    }

    async getMalicious() {
        const response = await this.httpClient.get("/patient/malicious", {
            headers: this.generateAuthHeaders()
        })
        return response.data
    }

    async blockPatient(id) {
        const response = await this.httpClient.get("/patient/block/" + id, {
            headers: this.generateAuthHeaders()
        })
        return response.data
    }

}