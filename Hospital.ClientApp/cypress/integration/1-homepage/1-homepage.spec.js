/// <reference types="cypress" />

context('Homepage', () => {

    it('should load page', () => {
        cy.visit('http://localhost:3000')
        cy.get('body').contains("Welcome to Hospital service")

    })

    it('should contain list of feedback', () => {
        cy.get('body').contains("Maria helped me lot!")
        cy.get('body').contains("It can get too cold sometimes")
    })
})
