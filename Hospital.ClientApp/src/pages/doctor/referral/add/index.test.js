import Referral from "."
import render from "../../../../helpers/renderComponent"

describe("Referral", () => {
    it("should referral form", () => {
        const { container } = render(<Referral />)

        expect(container).toMatchSnapshot()
    })
})