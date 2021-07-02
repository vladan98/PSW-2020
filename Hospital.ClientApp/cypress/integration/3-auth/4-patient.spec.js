/// <reference types="cypress" />
import faker from "faker"

context('Patient', () => {
    let randomUsername = faker.internet.userName()
    let randomPassword = faker.internet.password()
    let randomFirstName = faker.name.firstName()
    let randomLastName = faker.name.lastName()

    it('should fail login with blocked user', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-username-input]').type("blocked")
        cy.get('[data-cy=login-password-input]').type("blocked")

        cy.get('[data-cy=login-submit]').click()
        cy.get('body').contains("User is blocked")

    })

    it('should login as patient', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-username-input]').type("pacijent")
        cy.get('[data-cy=login-password-input]').type("pacijent")

        cy.get('[data-cy=login-submit]').click()

    })

    it('should be logged in', () => {
        cy.url().should("include", "/dashboard")
    })

    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })

    it('should go to register page and register patient', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-to-register-redirect]').click()

        cy.get('[data-cy=register-firstName-input]').type(randomFirstName)
        cy.get('[data-cy=register-lastName-input]').type(randomLastName)
        cy.get('[data-cy=register-chosenDoctorId-input]').click()
        cy.get('[data-cy=register-chosenDoctorId-input-0]').click()
        cy.get('[data-cy=register-gender-input]').click()
        cy.get('[data-cy=register-gender-input-0]').click()
        cy.get('[data-cy=register-username-input]').type(randomUsername)
        cy.get('[data-cy=register-password-input]').type(randomPassword)

        cy.get('[data-cy=register-submit]').click()

        cy.url().should("include", "/login")
    })

    it('should login with new patient', () => {
        cy.get('[data-cy=login-username-input]').type(randomUsername)
        cy.get('[data-cy=login-password-input]').type(randomPassword)

        cy.get('[data-cy=login-submit]').click()
    })

    it('should be succesfuly logged in', () => {
        cy.url().should("include", "/patient/dashboard")

        cy.get('body').contains("Schedule Appointment")
        cy.get('body').contains("View Appointments")
        cy.get('body').contains("Leave Feedback")

    })


    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })
})
