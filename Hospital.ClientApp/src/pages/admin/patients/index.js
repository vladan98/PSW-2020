import React, { useEffect, useMemo, useState } from 'react';
import { Grid, Typography, TableRow, TableCell, TableBody, CircularProgress, TableHead, Table, TableContainer, Container, Button, Paper } from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';
import { useUserContext } from '../../../context/UserContext';
import { toast } from 'react-toastify';
import { PatientService } from '../../../service/patient';


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

const PatientsList = () => {
    const classes = useStyles();
    const { user } = useUserContext()
    const [loading, setLoading] = useState(false);
    const [patients, setPatients] = useState([]);

    const patientService = useMemo(() => new PatientService(user), [user])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const patients = await patientService.getMalicious();
                setPatients(patients)
            } catch (e) {
                toast.error("Failed fetching patients.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [patientService])

    const blockPatient = (id) => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const response = await patientService.blockPatient(id)
                const patients = await patientService.getMalicious();
                setPatients(patients)
                toast.success(response)
            } catch (e) {
                toast.error("You are not allowed to cancel this appointment.")
            }
            setLoading(false)
        }
        sendRequest();
    }

    return (
        <Container component="main" maxWidth="lg">
            <div className={classes.paper}>
                <Grid item xs={12} align="center" >
                    {!loading ?
                        patients && patients.length > 0 ?
                            <TableContainer component={Paper}>
                                <Table className={classes.table} size="small" aria-label="a dense table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell align="center">First Name</TableCell>
                                            <TableCell align="center">Last Name</TableCell>
                                            <TableCell align="center">Action</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {patients.map(patient => (
                                            <TableRow key={patient.id}>
                                                <TableCell align="center">{patient.firstName}</TableCell>
                                                <TableCell align="center">{patient.lastName}</TableCell>
                                                {<TableCell align="center">
                                                    {!patient.blocked ?
                                                        <Button data-cy={`patients-list-item-${patient.id}`} onClick={() => blockPatient(patient.id)} color="primary">
                                                            Block
                                                        </Button> :
                                                        "Already blocked"}
                                                </TableCell>}
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                            :
                            <Typography align="center" component="h3" variant="h6">
                                No malicious patiens at the moment.
                            </Typography>
                        :
                        <CircularProgress />
                    }

                </Grid>
            </div>
        </Container>
    );
}

export default PatientsList