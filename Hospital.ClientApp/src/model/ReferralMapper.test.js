import { ReferralMapper } from "./ReferralMapper"

describe("Referral Mapper", () => {

    it('should referralDataToCreateReferralDTO mapper', () => {

        const doctorId = "5"
        const patientId = "6"
        const specialization = 0

        const response = ReferralMapper.referralDataToCreateReferralDTO(doctorId, patientId, specialization);

        expect(response.doctorId).toBe(5);
        expect(response.patientId).toBe(6);
        expect(response.specialization).toBe(specialization);
    })

})