import axios from "axios"
import { AuthService } from "./auth"

jest.mock("axios")

// class LocalStorageMock {
//     constructor() {
//         this.store = {};
//     }

//     clear() {
//         this.store = {};
//     }

//     getItem(key) {
//         return this.store[key] || null;
//     }

//     setItem(key, value) {
//         this.store[key] = String(value + "");
//     }

//     removeItem(key) {
//         delete this.store[key];
//     }
// };

// global.localStorage = new LocalStorageMock;


describe("AuthService", () => {
    const user = {
        id: 4,
        token: "adoiaufnonjsivfnzsmviksszm"
    }
    it('should test constructor', () => {

        const create = jest.fn(() => null)
        axios.create = create

        const authService = new AuthService();

        expect(authService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it.skip('should test getUser', () => {

        localStorage.setItem("hospital_user", user)

        const authService = new AuthService();

        expect(authService.getUser()).toBe(user);
    })

    it.skip('should test login', () => {

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))

        const authService = new AuthService();
        authService.login()

        expect(get).toHaveBeenCalled();
    })


    it.skip('should test logout', () => {

        localStorage.setItem("hospital_user", user)

        const authService = new AuthService();

        authService.logout()
    })


})