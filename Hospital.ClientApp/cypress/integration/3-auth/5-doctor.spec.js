/// <reference types="cypress" />

context('Doctor', () => {

    it('should login as doctor', () => {
        cy.visit('http://localhost:3000/login')

        cy.get('[data-cy=login-username-input]').type("hirurg")
        cy.get('[data-cy=login-password-input]').type("hirurg")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should be succesfuly logged in', () => {
        cy.url().should("include", "/doctor/dashboard")

        cy.get('body').contains("View And Assign Recipes")
        cy.get('body').contains("Add Referral")

    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })
})
