import SearchFilter from "."
import render from "../../../../../helpers/renderComponent"

describe("SearchFilter", () => {
    it("should render page with search form", () => {
        const { container } = render(<SearchFilter register={() => { }} referrals={[]} />)

        expect(container).toMatchSnapshot()
    })
})