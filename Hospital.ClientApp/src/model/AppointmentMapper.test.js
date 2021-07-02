import { AppointmentMapper } from "./AppointmentMapper"

describe("Appointment Mapper", () => {

    it('should searchAppointmentToSearchAppointmentDTO mapper', () => {

        const appointment = {
            startDate: "12.2.2012.",
            endDate: "12.2.2012.",
            selectedDoctorId: 4,
            typeOfAppointment: "3",
            priority: "0",
        }

        const response = AppointmentMapper.searchAppointmentToSearchAppointmentDTO(appointment, 6);

        expect(response.startDate).toBe(appointment.from);
        expect(response.endDate).toBe(appointment.to);
        expect(response.selectedDoctorId).toBe(appointment.doctorId);
        expect(response.typeOfAppointment).toBe(3);
        expect(response.priority).toBe(0);
        expect(response.userId).toBe(6);
    })

    it('should test appointmentToAppointmentDTO func', () => {
        const appointmentDTO = {
            startDate: "12.2.2012.",
            endDate: "12.2.2012.",
            choosenDoctorId: 4,
            typeOfAppointment: 3,
            priority: 0,
            doctor: {}
        }

        const response = AppointmentMapper.appointmentToAppointmentDTO(appointmentDTO, 6);

        expect(response.startDate).toBe(appointmentDTO.startDate);
        expect(response.endDate).toBe(appointmentDTO.endDate);
        expect(response.selectedDoctorId).toBe(appointmentDTO.selectedDoctorId);
        expect(response.typeOfAppointment).toBe(3);
        expect(response.priority).toBe(0);
        expect(response.patientId).toBe(6);
    })
})