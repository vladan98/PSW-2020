import { formatDateTime, formatAppointmentTime } from "./date"

describe("Date", () => {
    it("should format datetime", () => {
        expect(formatDateTime("2021-07-10T15:00:00")).toBe("2021-07-10 15:00:00")
    })
    it("should format appointment date", () => {
        expect(formatAppointmentTime("2021-07-10T15:00:00")).toBe("10. Jul 2021. 15:00")
    })
})