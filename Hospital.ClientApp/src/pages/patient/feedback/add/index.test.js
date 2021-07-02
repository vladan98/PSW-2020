import PostFeedback from "."
import render from "../../../../helpers/renderComponent"

describe("PostFeedback", () => {
    it("should render post feedback form", () => {
        const { container } = render(<PostFeedback />)

        expect(container).toMatchSnapshot()
    })
})