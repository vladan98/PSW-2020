/// <reference types="cypress" />

context('Appointment list', () => {

    it('should login as patient', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-username-input]').type("pacijent")
        cy.get('[data-cy=login-password-input]').type("pacijent")

        cy.get('[data-cy=login-submit]').click()
    })

    it('should be logged in', () => {
        cy.url().should("include", "/dashboard")
    })

    it('should display future appointments', () => {
        cy.get('[data-cy=appointments-list-btn]').click()
        cy.get('body').contains("10. Jul 2021. 15:00")
    })

    it('should display future appointments', () => {
        cy.get('[data-cy=toggle-list-btn]').click()
        cy.get('body').contains("11. May 2021. 14:00")
    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })

})