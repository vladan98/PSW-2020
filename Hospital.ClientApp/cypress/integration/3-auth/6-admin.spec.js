/// <reference types="cypress" />

context('Admin', () => {

    it('should login as admin', () => {
        cy.visit('http://localhost:3000/login')

        cy.get('[data-cy=login-username-input]').type("admin")
        cy.get('[data-cy=login-password-input]').type("admin")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should be succesfuly logged in', () => {
        cy.url().should("include", "/admin/dashboard")

        cy.get('body').contains("View Malicious patients")
        cy.get('body').contains("View feedback")

    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })
})
