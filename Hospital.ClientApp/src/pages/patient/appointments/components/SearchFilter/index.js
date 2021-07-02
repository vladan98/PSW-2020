import moment from "moment"
import React from 'react';
import { Grid, FormControl, Typography, InputLabel, MenuItem, Select, Button, TextField } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';

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
    }
}));


const SearchFilter = ({ register }) => {
    const classes = useStyles();

    return (
        <>
            <Typography component="h2" variant="h6">
                Search Appointments
            </Typography>
            <Grid container justify="center" spacing={2}>
                <Grid item>
                    <TextField
                        label="From"
                        type="date"
                        data-cy="schedule-startDate-input"
                        {...register("startDate")}
                        defaultValue={moment().format("YYYY-MM-DD")}
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        autoFocus
                    />
                </Grid>
                <Grid item>
                    <TextField
                        label="To"
                        data-cy="schedule-endDate-input"
                        type="date"
                        {...register("endDate")}
                        defaultValue={moment().add(3, "days").format("YYYY-MM-DD")}
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        autoFocus
                    />
                </Grid>
                <Grid item>
                    <FormControl variant="outlined" margin="normal" fullWidth >
                        <InputLabel>Type</InputLabel>
                        <Select
                            defaultValue={"0"}
                            data-cy="schedule-typeOfAppointment-input"
                            label="Type"
                            {...register("typeOfAppointment")}
                        >
                            <MenuItem data-cy="schedule-typeOfAppointment-input-0" value={"0"}>Examination</MenuItem>
                            <MenuItem data-cy="schedule-typeOfAppointment-input-1" value={"1"}>Surgery</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item>
                    <FormControl variant="outlined" margin="normal" fullWidth >
                        <InputLabel >Priority</InputLabel>
                        <Select
                            data-cy="schedule-priority-input"
                            defaultValue={"0"}
                            label="Priority"
                            {...register("priority")}
                        >
                            <MenuItem data-cy="schedule-priority-input-0" value={"0"}>None</MenuItem>
                            <MenuItem data-cy="schedule-priority-input-1" value={"1"}>Doctor</MenuItem>
                            <MenuItem data-cy="schedule-priority-input-2" value={"2"}>Date</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item>
                    <Button
                        data-cy="schedule-submit"
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                        margin="normal"
                    >
                        Search
                    </Button>
                </Grid>
            </Grid>
        </>
    );
}

export default SearchFilter