import AppointmentsList from "."
import render from "../../../helpers/renderComponent"

describe("AppointmentsList", () => {
    it("should render page with loading spinner", () => {
        const { container } = render(<AppointmentsList />)

        expect(container).toMatchSnapshot()
    })
})