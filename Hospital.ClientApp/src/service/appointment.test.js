import axios from "axios"
import { AppointmentService } from "./appointment"
jest.mock("axios")

describe("AppointmentService", () => {
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

        const appointmentService = new AppointmentService(user);

        expect(appointmentService.user).toBe(user);
        expect(appointmentService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it('should test generateAuthHeaders func', () => {

        const appointmentService = new AppointmentService(user);

        expect(appointmentService.generateAuthHeaders()).toStrictEqual(headers);
    })

    it('should test getUserAppointments func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const appointmentService = new AppointmentService(user);
        appointmentService.getUserAppointments()

        expect(get).toHaveBeenCalled();
    })

    it('should test cancelAppointment func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const appointmentService = new AppointmentService(user);
        appointmentService.cancelAppointment()

        expect(get).toHaveBeenCalled();
    })

    it('should test searchAppointments func', () => {

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))
        const searchParams = {
            startDate: "2021-07-10T15:00:00",
            selectedDoctorId: 2,
            typeOfAppointment: "1",
            endDate: "2021-07-10T15:15:00",
            priority: "0"
        }

        const appointmentService = new AppointmentService(user);
        appointmentService.searchAppointments(searchParams)

        expect(post).toHaveBeenCalled();
    })

    it.skip('should test scheduleAppointment func', () => {

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))

        const appointmentService = new AppointmentService(user);
        appointmentService.scheduleAppointment()

        expect(post).toHaveBeenCalled();
    })
})