import React, { useEffect, useState } from 'react';
import { Grid, FormControl, CircularProgress, InputLabel, MenuItem, Select, Container, Typography, Button, CssBaseline, TextField } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import { toast } from 'react-toastify';
import { useForm } from 'react-hook-form';
import { useHistory } from 'react-router-dom';
import { PatientService } from '../../service/patient';
import { DoctorService } from '../../service/doctor';


const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));


const Register = () => {
    const classes = useStyles();
    const history = useHistory();
    const { register, handleSubmit } = useForm();
    const [loading, setLoading] = useState(false);
    const [doctors, setDoctors] = useState([]);

    const patientService = new PatientService();

    const onSubmit = async (data) => {
        try {
            const responseText = await patientService.register(data)
            if (responseText) {
                toast.error(responseText)
                history.push("/login")
            }
        } catch (error) {
            toast.error("Bad credentials.")
        }
    };



    useEffect(() => {
        const doctorService = new DoctorService();

        const loadFeedback = async () => {
            setLoading(true)
            try {
                const doctors = await doctorService.getDoctors();
                setDoctors(doctors)
            } catch (e) {
                toast.error("Network error.")
            }
            setLoading(false)
        }
        loadFeedback();

    }, [])

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Typography component="h2" variant="h6">
                    Register
                </Typography>
                <form className={classes.form} onSubmit={handleSubmit(onSubmit)} noValidate>
                    <TextField
                        {...register("firstName")}
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        label="First Name"
                        autoFocus
                    />
                    <TextField
                        {...register("lastName")}
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        label="Last Name"
                        autoFocus
                    />
                    {!loading ? doctors.length > 0 &&
                        <FormControl variant="outlined" margin="normal" fullWidth >
                            <InputLabel >Choosen Doctor</InputLabel>
                            <Select
                                defaultValue={doctors[0].id}
                                {...register("chosenDoctorId")}
                                label="Choosen Doctor"
                            >
                                {doctors.map((d, i) => <MenuItem
                                    key={d.id} value={d.id}>{d.firstName + " " + d.lastName}</MenuItem>)}
                            </Select>
                        </FormControl> :
                        <CircularProgress />}
                    <FormControl variant="outlined" margin="normal" fullWidth >
                        <InputLabel >Gender</InputLabel>
                        <Select
                            defaultValue={"0"}
                            {...register("gender")}
                            label="Gender"
                        >
                            <MenuItem value={"0"}>Male</MenuItem>
                            <MenuItem value={"1"}>Female</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        {...register("username")}
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        label="Username"
                        autoFocus
                    />
                    <TextField
                        {...register("password")}
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        label="Password"
                        autoFocus
                        type="password"
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                    >
                        Register
                    </Button>
                    <Grid container>
                        <Grid item>
                            <Link
                                to="/login">
                                {"Already have an account? Sign In"}
                            </Link>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default Register