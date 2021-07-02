/// <reference types="cypress" />

context('Add Recipe', () => {

    it('should login as doctor', () => {
        cy.visit('http://localhost:3000/login')

        cy.get('[data-cy=login-username-input]').type("hirurg")
        cy.get('[data-cy=login-password-input]').type("hirurg")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should display recipes and add referral', () => {
        cy.url().should("include", "/doctor/dashboard")

        cy.get('[data-cy=view-assign-recipes-btn]').click()

        cy.get('[data-cy=recipe-list-item-0]').click()
        cy.get('body').contains("Recipe selected")

        cy.get('[data-cy=patient-recipe-input]').click()
        cy.get('[data-cy=patient-recipe-input-0]').click()

        cy.get('[data-cy=asign-recipe-submit]').click()
        cy.get('body').contains("Recipe assigned")


    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })
})
