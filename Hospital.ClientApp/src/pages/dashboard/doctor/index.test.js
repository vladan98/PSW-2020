import DoctorDashboard from "."
import render from "../../../helpers/renderComponent"

describe("DoctorDashboard", () => {
    it("should render doctor dashboard", () => {
        const { container } = render(<DoctorDashboard />)

        expect(container).toMatchSnapshot()
    })
})