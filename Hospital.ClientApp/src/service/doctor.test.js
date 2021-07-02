import axios from "axios"
import { DoctorService } from "./doctor"
jest.mock("axios")

describe("DoctorService", () => {
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

        const doctorService = new DoctorService(user);

        expect(doctorService.user).toBe(user);
        expect(doctorService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it('should test generateAuthHeaders func', () => {

        const doctorService = new DoctorService(user);

        expect(doctorService.generateAuthHeaders()).toStrictEqual(headers);
    })

    it('should test getDoctors func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const doctorService = new DoctorService(user);
        doctorService.getDoctors()

        expect(get).toHaveBeenCalled();
    })

    it('should test getAllDoctors func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const doctorService = new DoctorService(user);
        doctorService.getAllDoctors()

        expect(get).toHaveBeenCalled();
    })

})