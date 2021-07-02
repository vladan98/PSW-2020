import React, { useEffect, useMemo, useState } from 'react';
import { Grid, Typography, TableRow, TableCell, TableBody, CircularProgress, InputLabel, MenuItem, Select, TableHead, Table, TableContainer, Container, Button, Paper, FormControl } from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';
import { useUserContext } from '../../../context/UserContext';
import { toast } from 'react-toastify';
import { RecipesService } from '../../../service/recipes';
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


const Recipes = () => {
    const classes = useStyles();
    const { user } = useUserContext()
    const [selectedPatientId, setSelectedPatientId] = useState();
    const [selectedReciepId, setSelectedReciepId] = useState();
    const [loading, setLoading] = useState(false);
    const [recipes, setRecipes] = useState([]);
    const [patients, setPatients] = useState([]);

    const recipesService = useMemo(() => new RecipesService(user), [user])
    const patientService = useMemo(() => new PatientService(user), [user])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const response = await recipesService.getAllRecipes();
                setRecipes(response.recipes)
            } catch (e) {
                toast.error("Failed fetching recipes.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [recipesService])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const patients = await patientService.getAll();
                setPatients(patients)
                setSelectedPatientId(patients[0].id)
            } catch (e) {
                toast.error("Failed fetching patients.")
            }
        }
        sendRequest();

    }, [patientService])

    const asignRecipe = async (data) => {
        console.log("data", data)
        if (!selectedReciepId) {
            toast.error("Select recipe to asign.")
        }
        else {
            setLoading(true)
            try {
                await recipesService.assignRecipe(selectedReciepId, selectedPatientId);
                toast.success("Recipe assigned.")
            } catch (e) {
                toast.error("Assigning recipe went wrong.")
            }
            setLoading(false)
        }
    }

    return (
        <Container component="main" maxWidth="lg">
            <div className={classes.paper}>
                <Grid item xs={12} align="center" >
                    {!loading ?
                        (recipes && recipes.length > 0 ?
                            <>
                                <Typography component="h2" variant="h6">
                                    Choose Available Recipes
                                </Typography>
                                <TableContainer component={Paper}>
                                    <Table className={classes.table} size="small" aria-label="a dense table">
                                        <TableHead>
                                            <TableRow>
                                                <TableCell align="center">Recipe Id</TableCell>
                                                <TableCell align="center">Medication</TableCell>
                                                <TableCell align="center">Action</TableCell>
                                            </TableRow>
                                        </TableHead>
                                        <TableBody>
                                            {recipes.map((recipe, idx) => (
                                                <TableRow key={recipe.id}>
                                                    <TableCell align="center" component="th" scope="row">
                                                        {recipe.id}
                                                    </TableCell>
                                                    <TableCell align="center" component="th" scope="row">
                                                        {recipe.medication}
                                                    </TableCell>
                                                    <TableCell align="center">
                                                        {selectedReciepId === recipe.id ?
                                                            "Selected" :
                                                            <Button
                                                                data-cy={`recipe-list-item-${idx}`}
                                                                variant="contained"
                                                                onClick={() => {
                                                                    setSelectedReciepId(recipe.id)
                                                                    toast.success("Recipe selected.")
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
                                No available recieps.
                            </Typography>
                        ) :
                        <CircularProgress />
                    }

                </Grid>
                <Grid container justify="center" align="center" spacing={2}>

                    <Grid item>
                        {!loading ? (patients && patients.length > 0 &&
                            <FormControl variant="outlined" margin="normal" fullWidth >
                                <InputLabel>Selected Patient</InputLabel>
                                <Select
                                    data-cy="patient-recipe-input"
                                    value={selectedPatientId}
                                    onChange={(e) => setSelectedPatientId(e.target.value)}
                                    label="Select Patient"
                                >
                                    {patients.map((patient, idx) => (
                                        <MenuItem data-cy={`patient-recipe-input-${idx}`} key={patient.id} value={patient.id}>{patient.firstName + patient.lastName}</MenuItem>
                                    ))}
                                </Select>
                            </FormControl>) :
                            <CircularProgress />}
                    </Grid>
                </Grid>
                <Button data-cy="asign-recipe-submit" variant="contained" onClick={asignRecipe} >Asign Recipe</Button>
            </div>
        </Container>
    );
}

export default Recipes