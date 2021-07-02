/// <reference types="cypress" />

context('Publish feedback', () => {

    it('should login as patient', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-username-input]').type("admin")
        cy.get('[data-cy=login-password-input]').type("admin")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should be logged in', () => {
        cy.url().should("include", "/dashboard")
    })

    it('should block user', () => {
        cy.get('[data-cy=malicious-patients-btn]').click()

        cy.get('[data-cy=patients-list-item-7]').click()

        cy.get('body').contains("Patient blocked.")
    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })

})
