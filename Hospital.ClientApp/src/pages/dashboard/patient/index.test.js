import PatientDashboard from "."
import render from "../../../helpers/renderComponent"

describe("PatientDashboard", () => {
    it("should render patient dashboard", () => {
        const { container } = render(<PatientDashboard />)

        expect(container).toMatchSnapshot()
    })
})