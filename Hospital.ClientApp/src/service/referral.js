
import axios from "axios"
import { ReferralMapper } from "../model/ReferralMapper";

export class ReferralService {
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

    async getUserReferrals() {
        const response = await this.httpClient.get("/referral/patient/" + this.user.id, {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }

    async addReferral(doctorId, patientId, specialization) {
        const referralDTO = ReferralMapper.referralDataToCreateReferralDTO(doctorId, patientId, specialization)
        const response = await this.httpClient.post("/referral/add", referralDTO, {
            headers: this.generateAuthHeaders()
        })
        return response.data
    }


}