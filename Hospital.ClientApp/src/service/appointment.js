import axios from "axios"
import { AppointmentMapper } from "../model/AppointmentMapper";

export class AppointmentService {
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

    async getUserAppointments() {
        const response = await this.httpClient.get("/appointments/user/" + this.user.id, {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }

    async cancelAppointment(id) {
        const response = await this.httpClient.get("/appointments/cancel/" + id, {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }

    async searchAppointments(searchData) {
        const searchAppointmentDTO = AppointmentMapper.searchAppointmentToSearchAppointmentDTO(searchData, this.user.id)
        const response = await this.httpClient.post("/appointments/search", searchAppointmentDTO, {
            headers: this.generateAuthHeaders(),
        })
        return response?.data
    }

    async scheduleAppointment(appointment) {
        const appointmentDTO = AppointmentMapper.appointmentToAppointmentDTO(appointment, this.user.id)
        const response = await this.httpClient.post("/appointments/schedule", appointmentDTO, {
            headers: this.generateAuthHeaders(),
        })
        return response?.data
    }


}