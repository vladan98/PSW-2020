/// <reference types="cypress" />

context('Schedule appointment with doctor priority', () => {

    let scheduledAppointmentStartDate = ''

    it('should login as patient', () => {
        cy.visit('http://localhost:3000/login')
        cy.get('[data-cy=login-username-input]').type("pacijent")
        cy.get('[data-cy=login-password-input]').type("pacijent")

        cy.get('[data-cy=login-submit]').click()

    })

    it('should be logged in', () => {
        cy.url().should("include", "/dashboard")
    })

    it('should fail finding appointemnts for 14.Jul without priority', () => {
        cy.get('[data-cy=schedule-appointment-btn]').click()
        cy.get('[data-cy=schedule-startDate-input]').type("2021-07-14")
        cy.get('[data-cy=schedule-endDate-input]').type("2021-07-15")
        cy.get('[data-cy=schedule-typeOfAppointment-input]').click()
        cy.get('[data-cy=schedule-typeOfAppointment-input-0]').click()

        cy.get('[data-cy=schedule-priority-input]').click()
        cy.get('[data-cy=schedule-priority-input-0]').click()

        cy.get('[data-cy=schedule-submit]').click()

        cy.get('body').contains("There is no appointments for selected filter.")
    })

    it('should find appointemnt for 12.Jul with doctor priority', () => {
        cy.get('[data-cy=schedule-startDate-input]').type("2021-07-14")
        cy.get('[data-cy=schedule-endDate-input]').type("2021-07-15")
        cy.get('[data-cy=schedule-typeOfAppointment-input]').click()
        cy.get('[data-cy=schedule-typeOfAppointment-input-0]').click()

        cy.get('[data-cy=schedule-priority-input]').click()
        cy.get('[data-cy=schedule-priority-input-1]').click()

        cy.get('[data-cy=schedule-submit]').click()
    })


    it('should schedule first available appointment from results', () => {

        cy.get('[data-cy=schedule-startDate-appointment-0]').should(($td) => {
            expect($td[0].innerText).to.include('12. Jul 2021.')
            scheduledAppointmentStartDate = $td[0].innerText
        })
        cy.get('[data-cy=schedule-schedule-appointment-0]').click()
        cy.get('body').contains("Appointment created.")

    })

    it('should confirm that appointment is created ', () => {

        cy.get('[data-cy=header-dashboard]').click()
        cy.get('[data-cy=appointments-list-btn]').click()
        cy.get('body').contains(scheduledAppointmentStartDate)

    })


    it('should logout', () => {
        cy.get('[data-cy=header-logout]').click()
        cy.url().should("include", "/")
    })

})