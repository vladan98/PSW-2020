import ScheduleAppointment from "."
import render from "../../../../helpers/renderComponent"

describe("ScheduleAppointment", () => {
    it("should render page with SearchFilter component", () => {
        const { container } = render(<ScheduleAppointment />)

        expect(container).toMatchSnapshot()
    })
})