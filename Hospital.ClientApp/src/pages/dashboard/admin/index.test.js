import AdminDashboard from "."
import render from "../../../helpers/renderComponent"

describe("AdminDashboard", () => {
    it("should render admin dashboard", () => {
        const { container } = render(<AdminDashboard />)

        expect(container).toMatchSnapshot()
    })
})