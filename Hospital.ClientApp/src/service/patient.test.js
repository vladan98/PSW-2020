import axios from "axios"
import { PatientService } from "./patient"
jest.mock("axios")

describe("PatientService", () => {
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

        const patientService = new PatientService(user);

        expect(patientService.user).toBe(user);
        expect(patientService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it('should test generateAuthHeaders func', () => {

        const patientService = new PatientService(user);

        expect(patientService.generateAuthHeaders()).toStrictEqual(headers);
    })

    it('should test register func', () => {

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))
        const dto = {
            username: "newuser",
            password: "newuser",
            firstName: "New",
            lastName: "User",
            choosenDoctorId: 5,
            gender: "1"
        }

        const patientService = new PatientService(user);
        patientService.register(dto)

        expect(post).toHaveBeenCalled();
    })

    it('should test getAll func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const patientService = new PatientService(user);
        patientService.getAll()

        expect(get).toHaveBeenCalled();
    })

    it('should test getMalicious func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const patientService = new PatientService(user);
        patientService.getMalicious()

        expect(get).toHaveBeenCalled();
    })

    it('should test blockPatient func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const patientService = new PatientService(23);
        patientService.blockPatient()

        expect(get).toHaveBeenCalled();
    })

})