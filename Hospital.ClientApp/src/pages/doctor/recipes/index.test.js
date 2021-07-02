import Recipes from "."
import render from "../../../helpers/renderComponent"

describe("Recipes", () => {
    it("should loading spinner and recipes form", () => {
        const { container } = render(<Recipes />)

        expect(container).toMatchSnapshot()
    })
})