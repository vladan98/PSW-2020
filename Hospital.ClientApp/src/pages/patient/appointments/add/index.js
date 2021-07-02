import React, { useEffect, useMemo, useState } from 'react';
import { Grid, Typography, TableRow, TableCell, TableBody, CircularProgress, TableHead, Table, TableContainer, Container, Button, Paper } from '@material-ui/core';
import { toast } from 'react-toastify';
import { makeStyles } from '@material-ui/core/styles';
import { useForm } from 'react-hook-form';

import SearchFilter from "../components/SearchFilter"
import { AppointmentService } from '../../../../service/appointment';
import { useUserContext } from '../../../../context/UserContext';
import { formatAppointmentTime } from '../../../../helpers/date';
import { ReferralService } from '../../../../service/referral';


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
    topGap: {
        marginBottom: theme.spacing(3),
    }
}));

const appointmentType = (type) => type === 1 ? "Surgery" : "Examination"
const doctorSpecialization = (type) => {
    if (type === 3) return "Surgeon"
    else if (type === 2) return "Pediatrician"
    else if (type === 1) return "Ophthalomogist"
    else return "General"
}

const ScheduleAppointment = () => {
    const classes = useStyles();
    const { user } = useUserContext()
    const { register, handleSubmit, watch } = useForm();
    const [displayReferrals, setDisplayReferrals] = useState(true);
    const [selectedReferralId, setSelectedReferralId] = useState(-1);
    const [loading, setLoading] = useState(false);
    const [appointments, setAppointments] = useState([]);
    const [referrals, setReferrals] = useState([]);

    const appointmentService = useMemo(() => new AppointmentService(user), [user])
    const referralService = useMemo(() => new ReferralService(user), [user])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const referrals = await referralService.getUserReferrals();
                setReferrals(referrals)
            } catch (e) {
                toast.error("Failed fetching referrals.")
            }
            setLoading(false)
        }
        sendRequest();
    }, [referralService])

    const search = (data) => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const appointments = await appointmentService.searchAppointments({
                    ...data,
                    selectedDoctorId: selectedReferralId === -1 ? -1 : referrals.find(r => r.id == selectedReferralId).doctorId
                });
                setAppointments(appointments)
            } catch (e) {
                toast.error("Error occured.")
            }
            setLoading(false)
        }
        sendRequest();
    }

    const schedule = (appointment) => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const response = await appointmentService.scheduleAppointment({
                    ...appointment,
                    referralId: selectedReferralId
                });
                toast.success(response)
                // reload appointments with same search parameters
                search(watch())
            } catch (e) {
                toast.error("Error occured.")
            }
            setLoading(false)
        }
        sendRequest();
    }

    return (
        <Container component="main" maxWidth="lg">
            <div className={classes.paper}>
                <Grid item xs={12} align="center" >
                    <Button
                        className={classes.topGap}
                        variant="contained"
                        onClick={() => setDisplayReferrals(old => !old)}
                    >
                        {displayReferrals ? "Hide referrals" : "Show referrals"}
                    </Button>
                    {!loading ?
                        displayReferrals && (referrals && referrals.length > 0 ?
                            <>
                                <Typography component="h2" variant="h6">
                                    Referrals
                                </Typography>
                                <TableContainer component={Paper}>
                                    <Table className={classes.table} size="small" aria-label="a dense table">
                                        <TableHead>
                                            <TableRow>
                                                <TableCell>Id</TableCell>
                                                <TableCell align="center">Doctor Name</TableCell>
                                                <TableCell align="center">Doctor Specialization</TableCell>
                                                <TableCell align="center">
                                                    <Button
                                                        data-cy="schedule-unselect-referral"
                                                        variant="contained"
                                                        onClick={() => {
                                                            setSelectedReferralId(0)
                                                            toast.success("Choosen doctor selected.")
                                                        }}>
                                                        Unselect referral
                                                    </Button>
                                                </TableCell>
                                            </TableRow>
                                        </TableHead>
                                        <TableBody>
                                            {referrals.map((referral) => (
                                                <TableRow key={referral.id}>
                                                    <TableCell component="th" scope="row">
                                                        {referral.id}
                                                    </TableCell>
                                                    <TableCell align="center">{referral.doctorFullName}</TableCell>
                                                    <TableCell align="center">{doctorSpecialization(referral.specialization)}</TableCell>
                                                    <TableCell data-cy={`schedule-status-referral-${referral.id}`} align="center">
                                                        {referral.used ? "Already used" :
                                                            selectedReferralId === referral.id ?
                                                                "Selected" :
                                                                <Button
                                                                    data-cy={`schedule-select-referral-${referral.id}`}
                                                                    variant="contained"
                                                                    onClick={() => {
                                                                        setSelectedReferralId(referral.id)
                                                                        toast.success("Referral selected.")
                                                                    }}>
                                                                    Select
                                                                </Button>}
                                                    </TableCell>
                                                </TableRow>
                                            ))}
                                        </TableBody>
                                    </Table>
                                </TableContainer>
                            </>
                            :
                            <Typography align="center" component="h3" variant="h6">
                                You have no referrals.
                            </Typography>
                        ) :
                        <CircularProgress />
                    }

                    <form className={classes.form} onSubmit={handleSubmit(search)} noValidate>
                        <SearchFilter register={register} />
                    </form>

                    {!loading ?
                        appointments && appointments.length > 0 ?
                            <TableContainer component={Paper}>
                                <Table className={classes.table} size="small" aria-label="a dense table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Description</TableCell>
                                            <TableCell align="center">Doctor</TableCell>
                                            <TableCell align="center">Start Time</TableCell>
                                            <TableCell align="center">End Time</TableCell>
                                            <TableCell align="center">Type</TableCell>
                                            <TableCell align="center">Action</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {appointments.map((appointment, id) => (
                                            <TableRow key={appointment.id}>
                                                <TableCell component="th" scope="row">
                                                    {appointment.description}
                                                </TableCell>
                                                <TableCell data-cy={`schedule-doctor-appointment-${id}`} align="center">{appointment.doctor.firstName + " " + appointment.doctor.lastName}</TableCell>
                                                <TableCell data-cy={`schedule-startDate-appointment-${id}`} align="center">{formatAppointmentTime(appointment.startTime)}</TableCell>
                                                <TableCell align="center">{formatAppointmentTime(appointment.endTime)}</TableCell>
                                                <TableCell align="center">{appointmentType(appointment.typeOfAppointment)}</TableCell>
                                                <TableCell align="center">
                                                    <Button
                                                        data-cy={`schedule-schedule-appointment-${id}`}
                                                        variant="contained"
                                                        onClick={() => {
                                                            schedule(appointment)
                                                        }}>
                                                        Schedule
                                                    </Button>
                                                </TableCell>
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                            :
                            <Typography className={classes.topGap} align="center" component="h3" variant="h6">
                                There is no appointments for selected filter.
                            </Typography>
                        :
                        <CircularProgress />
                    }

                </Grid>
            </div>
        </Container >
    );
}

export default ScheduleAppointment