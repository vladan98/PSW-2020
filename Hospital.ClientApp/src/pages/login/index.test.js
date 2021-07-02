import Login from "."
import render from "../../helpers/renderComponent"

describe("Login", () => {
    it("should render login page with form", () => {
        const { container } = render(<Login />)

        expect(container).toMatchSnapshot()
    })
})