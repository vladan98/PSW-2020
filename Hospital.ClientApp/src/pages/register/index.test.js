import RegisterPage from "."
import render from "../../helpers/renderComponent"

describe("RegisterPage", () => {
    it("renders page", () => {
        const { container } = render(<RegisterPage />)

        expect(container).toMatchSnapshot()
    })
})