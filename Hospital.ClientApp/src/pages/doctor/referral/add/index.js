import React, { useEffect, useMemo, useState } from 'react';
import { Grid, CircularProgress, InputLabel, MenuItem, Select, Container, Button, FormControl } from '@material-ui/core';
import { toast } from 'react-toastify';
import { makeStyles } from '@material-ui/core/styles';

import { useUserContext } from '../../../../context/UserContext';
import { ReferralService } from '../../../../service/referral';
import { PatientService } from '../../../../service/patient';
import { DoctorService } from '../../../../service/doctor';
import { ReferralMapper } from '../../../../model/ReferralMapper';


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


const Referrals = () => {
    const classes = useStyles();
    const { user } = useUserContext()
    const [selectedDoctorId, setSelectedDoctorId] = useState("-1");
    const [selectedPatientId, setSelectedPatientId] = useState("-1");
    const [loading, setLoading] = useState(false);
    const [patients, setPatients] = useState([]);
    const [doctors, setDoctors] = useState([]);

    const referralService = useMemo(() => new ReferralService(user), [user])
    const patientService = useMemo(() => new PatientService(user), [user])
    const doctorService = useMemo(() => new DoctorService(user), [user])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const patients = await patientService.getAll();
                setPatients(patients)
            } catch (e) {
                toast.error("Failed fetching patients.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [patientService])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const doctors = await doctorService.getAllDoctors();
                setDoctors(doctors)
            } catch (e) {
                toast.error("Failed fetching doctors.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [doctorService])

    const asignReferral = async () => {
        setLoading(true)
        try {
            if (selectedDoctorId === "-1" || selectedPatientId === "-1") {
                toast.error("You have to select doctor and patient.")
                return
            }
            const specialization = doctors.find(d => d.id === parseInt(selectedDoctorId))?.specialization
            const response = await referralService.addReferral(selectedDoctorId, selectedPatientId, specialization);
            toast.success(response)
        } catch (e) {
            toast.error("Failed adding referral.")
        }
        setLoading(false)
    }

    const doctorSpecialization = (type) => {
        if (type === 3) return "Surgeon"
        else if (type === 2) return "Pediatrician"
        else if (type === 1) return "Ophthalomogist"
        else return "General"
    }

    return (
        <Container component="main" maxWidth="lg">
            <div className={classes.paper}>
                <form>
                    <Grid container justify="center" align="center" spacing={2}>

                        <Grid item>
                            {!loading ? (patients && patients.length > 0 &&
                                <FormControl variant="outlined" margin="normal" fullWidth >
                                    <InputLabel>Selected Patient</InputLabel>
                                    <Select
                                        data-cy="patient-referral-input"
                                        value={selectedPatientId}
                                        onChange={(e) => setSelectedPatientId(e.target.value)}
                                        defaultValue="-1"
                                        label="Select Patient"
                                    >
                                        <MenuItem value="-1" >Not Selected</MenuItem>
                                        {patients.map((patient, idx) => (
                                            <MenuItem data-cy={`patient-referral-input-${idx}`} key={patient.id} value={`${patient.id}`}>{patient.firstName + " " + patient.lastName}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>) :
                                <CircularProgress />}
                        </Grid>
                        <Grid item>
                            {!loading ? (doctors && doctors.length > 0 &&
                                <FormControl variant="outlined" margin="normal" fullWidth >
                                    <InputLabel>Selected Doctor</InputLabel>
                                    <Select
                                        data-cy="doctor-referral-input"
                                        value={selectedDoctorId}
                                        onChange={(e) => setSelectedDoctorId(e.target.value)}
                                        defaultValue="-1"
                                        label="Select Patient"
                                    >
                                        <MenuItem value="-1" >Not Selected</MenuItem>
                                        {doctors.map((doctor, idx) => (
                                            <MenuItem data-cy={`doctor-referral-input-${idx}`} key={doctor.id} value={`${doctor.id}`}>{doctor.firstName + " " + doctor.lastName + " - " + doctorSpecialization(doctor.specialization)}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>) :
                                <CircularProgress />}
                        </Grid>
                    </Grid>

                    <Grid container justify="center">
                        <Grid item>
                            <Button data-cy="add-referral-submit" variant="contained" onClick={asignReferral}>Asign Referral</Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default Referrals