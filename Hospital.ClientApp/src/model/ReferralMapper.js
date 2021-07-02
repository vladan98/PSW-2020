export const ReferralMapper = {
    referralDataToCreateReferralDTO(doctorId, patientId, specialization) {

        return {
            doctorId: parseInt(doctorId),
            patientId: parseInt(patientId),
            specialization
        }
    },
}