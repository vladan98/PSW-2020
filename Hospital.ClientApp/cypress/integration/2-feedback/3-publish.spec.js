/// <reference types="cypress" />
import faker from "faker"

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

    it('should publish feedback', () => {
        cy.get('[data-cy=publish-feedback-btn]').click()

        cy.get('[data-cy=feedback-list-item-1]').click()
        cy.get('body').contains("Updated")
    })

    it('should confirm that feedback is posted', () => {
        cy.get('[data-cy=header-homepage]').click()

        cy.contains("Terible place, never coming again!").should('exist')
    })

    it('should unpublish feedback', () => {
        cy.get('[data-cy=header-dashboard]').click()
        cy.get('[data-cy=publish-feedback-btn]').click()

        cy.get('[data-cy=feedback-list-item-1]').click()

        cy.get('body').contains("Updated")
    })

    it('should confirm that feedback is removed from homepage', () => {
        cy.get('[data-cy=header-homepage]').click()

        cy.contains("Terible place, never coming again!").should('not.exist')
    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })

})
