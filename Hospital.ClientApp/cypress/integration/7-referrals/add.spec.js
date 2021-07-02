/// <reference types="cypress" />

context('Add Referral', () => {

    it('should login as doctor', () => {
        cy.visit('http://localhost:3000/login')

        cy.get('[data-cy=login-username-input]').type("hirurg")
        cy.get('[data-cy=login-password-input]').type("hirurg")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should list patients and docotrs and add referral', () => {
        cy.url().should("include", "/doctor/dashboard")

        cy.get('[data-cy=add-referral-btn]').click()

        cy.get('[data-cy=patient-referral-input]').click()
        cy.get('[data-cy=patient-referral-input-0]').click()

        cy.get('[data-cy=doctor-referral-input]').click()
        cy.get('[data-cy=doctor-referral-input-0]').click()

        cy.get('[data-cy=add-referral-submit]').click()
        cy.get('body').contains("Referral created")


    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })
})
