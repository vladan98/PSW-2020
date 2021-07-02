/// <reference types="cypress" />
import faker from "faker"

context('Post feedback', () => {

    const randomTitle = faker.lorem.word()
    const randomContent = faker.lorem.sentence()

    it('should login as patient', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-username-input]').type("pacijent")
        cy.get('[data-cy=login-password-input]').type("pacijent")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should be logged in', () => {
        cy.url().should("include", "/dashboard")
    })

    it('should post feedback', () => {
        cy.get('[data-cy=leave-feedback-btn]').click()

        cy.get('[data-cy=leave-feedback-title-input]').type(randomTitle)
        cy.get('[data-cy=leave-feedback-content-input]').type(randomContent)

        cy.get('[data-cy=leave-feedback-submit]').click()

        cy.get('body').contains("Feedback sent")
    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })

})