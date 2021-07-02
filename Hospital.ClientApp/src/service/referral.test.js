import axios from "axios"
import { ReferralService } from "./referral"
jest.mock("axios")

describe("ReferralService", () => {
    const user = {
        id: 4,
        token: "adoiaufnonjsivfnzsmviksszm"
    }
    const headers = {
        Authorization: `Bearer ${user.token}`
    }

    it('should test constructor', () => {

        const create = jest.fn(() => null)
        axios.create = create

        const referralService = new ReferralService(user);

        expect(referralService.user).toBe(user);
        expect(referralService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it('should test generateAuthHeaders func', () => {

        const referralService = new ReferralService(user);

        expect(referralService.generateAuthHeaders()).toStrictEqual(headers);
    })

    it.skip('should test addReferral func', () => {

        const doctorId = "5"
        const patientId = "6"
        const specialization = 0

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))

        const referralService = new ReferralService(user);
        referralService.addReferral(doctorId, patientId, specialization)

        expect(post).toHaveBeenCalled();
    })

    it('should test getUserReferrals func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const referralService = new ReferralService(user);
        referralService.getUserReferrals()

        expect(get).toHaveBeenCalled();
    })


})