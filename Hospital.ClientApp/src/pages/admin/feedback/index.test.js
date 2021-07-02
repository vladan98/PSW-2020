import Feedback from "."
import render from "../../../helpers/renderComponent"

describe("Feedback", () => {
    it("should render feedback page", () => {
        const { container } = render(<Feedback />)

        expect(container).toMatchSnapshot()
    })
})