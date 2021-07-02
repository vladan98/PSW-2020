export const AppointmentMapper = {
    searchAppointmentToSearchAppointmentDTO({ startDate, selectedDoctorId, typeOfAppointment, endDate, priority }, userId) {
        const type = !typeOfAppointment ? 0 : parseInt(typeOfAppointment)
        const searchPriority = !priority ? 0 : parseInt(priority)

        return {
            from: startDate,
            to: endDate,
            doctorId: selectedDoctorId,
            typeOfAppointment: type,
            priority: searchPriority,
            userId
        }
    },

    appointmentToAppointmentDTO({ doctor, ...appointmentDTO }, userId) {
        return {
            ...appointmentDTO,
            patientId: userId
        }
    }
}