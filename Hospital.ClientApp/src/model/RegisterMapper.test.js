import { RegisterMapper } from "./RegisterMapper"

describe("Referral Mapper", () => {

    it('should registerUserDataToRegisterUserDTO mapper', () => {

        const dto = {
            username: "newuser",
            password: "newuser",
            firstName: "New",
            lastName: "User",
            choosenDoctorId: 5,
            gender: "1"
        }

        const response = RegisterMapper.registerUserDataToRegisterUserDTO(dto);

        expect(response.username).toBe(dto.username);
        expect(response.password).toBe(dto.password);
        expect(response.firstName).toBe(dto.firstName);
        expect(response.lastName).toBe(dto.lastName);
        expect(response.choosenDoctorId).toBe(dto.choosenDoctorId);
        expect(response.gender).toBe(1);

    })

})