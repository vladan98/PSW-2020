import { render } from "@testing-library/react"
import { BrowserRouter } from "react-router-dom"

const renderComponent = Component =>
    render(
        <>
            <BrowserRouter>
                {Component}
            </BrowserRouter>
        </>
    )

export default renderComponent