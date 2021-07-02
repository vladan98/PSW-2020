import moment from "moment"

export const formatDateTime = (date) => {
    const parts = date.split("T")
    return `${parts[0]} ${parts[1]}`
}
export const formatAppointmentTime = (date) => moment(formatDateTime(date), "YYYY-MM-DD HH:mm:ss").format("DD. MMM YYYY. HH:mm")