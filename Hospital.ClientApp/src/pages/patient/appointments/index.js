import React, { useEffect, useMemo, useState } from 'react';
import { Grid, Typography, TableRow, TableCell, TableBody, CircularProgress, TableHead, Table, TableContainer, Container, Button, Paper } from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';
import { AppointmentService } from '../../../service/appointment';
import { useUserContext } from '../../../context/UserContext';
import { toast } from 'react-toastify';
import { formatAppointmentTime } from '../../../helpers/date';


const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    card: {
        marginTop: theme.spacing(3),
    },
    table: {
        minWidth: 650,
    },
    switch: {
        marginBottom: theme.spacing(3),
    }
}));

const appointmentType = (type) => type === 1 ? "Surgery" : "Examination"

const AppointmentsList = () => {
    const classes = useStyles();
    const { user } = useUserContext()
    const [displayPastAppointments, setDisplayPastAppointments] = useState(false);
    const [loading, setLoading] = useState(false);
    const [appointments, setAppointments] = useState([]);

    const appointmentService = useMemo(() => new AppointmentService(user), [user])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const appointments = await appointmentService.getUserAppointments();
                setAppointments(appointments)
            } catch (e) {
                toast.error("Failed fetching appointments.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [appointmentService])

    const cancelAppointment = (id) => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                await appointmentService.cancelAppointment(id)
                const appointments = await appointmentService.getUserAppointments();
                setAppointments(appointments)
            } catch (e) {
                toast.error("You are not allowed to cancel this appointment.")
            }
            setLoading(false)
        }
        sendRequest();
    }

    const selectedAppointments = () => displayPastAppointments ? appointments.previousAppointments : appointments.futureAppointments

    return (
        <Container component="main" maxWidth="lg">
            <div className={classes.paper}>
                <Grid item xs={12} align="center" >
                    <Button
                        data-cy="toggle-list-btn"
                        className={classes.switch}
                        variant="contained"
                        onClick={() => setDisplayPastAppointments(old => !old)}
                    >
                        {displayPastAppointments ? "Show future appointments" : "Display past appointments"}
                    </Button>
                    {!loading ?
                        selectedAppointments() && selectedAppointments().length > 0 ?
                            <TableContainer component={Paper}>
                                <Table className={classes.table} size="small" aria-label="a dense table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Description</TableCell>
                                            <TableCell align="center">Doctor</TableCell>
                                            <TableCell align="center">Start Time</TableCell>
                                            <TableCell align="center">End Time</TableCell>
                                            <TableCell align="center">Type</TableCell>
                                            {!displayPastAppointments && <TableCell align="center"></TableCell>}
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {selectedAppointments().map(appointment => (
                                            <TableRow key={appointment.id}>
                                                <TableCell component="th" scope="row">
                                                    {appointment.description}
                                                </TableCell>
                                                <TableCell align="center">{appointment.doctor.firstName + " " + appointment.doctor.lastName}</TableCell>
                                                <TableCell align="center">{formatAppointmentTime(appointment.startTime)}</TableCell>
                                                <TableCell align="center">{formatAppointmentTime(appointment.endTime)}</TableCell>
                                                <TableCell align="center">{appointmentType(appointment.typeOfAppointment)}</TableCell>
                                                {!displayPastAppointments && <TableCell align="center">
                                                    {!appointment.canceled ?
                                                        <Button onClick={() => cancelAppointment(appointment.id)} color="primary">
                                                            Cancel
                                                        </Button> :
                                                        "Canceled"}
                                                </TableCell>}
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                            :
                            <Typography align="center" component="h3" variant="h6">
                                There is no appointments for selected filter.
                            </Typography>
                        :
                        <CircularProgress />
                    }

                </Grid>
            </div>
        </Container>
    );
}

export default AppointmentsList