import Home from "."
import render from "../../helpers/renderComponent"

describe("Home", () => {
    it("should render page with loading spinner", () => {
        const { container } = render(<Home />)

        expect(container).toMatchSnapshot()
    })
})