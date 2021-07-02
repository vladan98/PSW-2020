export const RegisterMapper = {
    registerUserDataToRegisterUserDTO(dto) {
        return {
            ...dto,
            gender: dto.gender === "1" ? 1 : 0
        }
    }

}