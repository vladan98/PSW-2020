import axios from "axios"

export class FeedbackService {
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

    async getPublishedFeedback() {
        const response = await this.httpClient.get("/feedback/published")
        return response?.data
    }

    async getAllFeedback() {
        const response = await this.httpClient.get("/feedback/all", {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }

    async postFeedback(title, content) {
        const response = await this.httpClient.post("/feedback/post", {
            title,
            content,
            patientId: this.user.id
        }, {
            headers: this.generateAuthHeaders()
        })
        return response
    }

    async updateFeedback(id) {
        const response = await this.httpClient.get("feedback/update/" + id, {
            headers: this.generateAuthHeaders()
        })
        return response.data
    }


}