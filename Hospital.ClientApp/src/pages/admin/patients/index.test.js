import Patients from "."
import render from "../../../helpers/renderComponent"

describe("Patients", () => {
    it("should render patients page", () => {
        const { container } = render(<Patients />)

        expect(container).toMatchSnapshot()
    })
})