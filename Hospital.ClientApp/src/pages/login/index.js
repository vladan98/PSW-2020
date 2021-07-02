import React from 'react';
import { Grid, Container, Typography, Button, CssBaseline, TextField } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import { toast } from 'react-toastify';
import { AuthService } from '../../service/auth';
import { useForm } from 'react-hook-form';
import { useHistory } from 'react-router-dom';
import { useUserContext } from '../../context/UserContext';


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


const Login = () => {
    const classes = useStyles();
    const history = useHistory();
    const { register, handleSubmit } = useForm();
    const { setUser } = useUserContext()

    const authService = new AuthService();

    const onSubmit = async (data) => {
        try {
            await authService.login(data.username, data.password)
            const user = authService.getUser()
            if (user && user.id === -1) {
                toast.error("User is blocked.")
                return
            }
            else {
                setUser(user)
                history.push("/dashboard")
            }
        } catch (error) {
            toast.error("Bad credentials.")
        }
    };

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Typography component="h2" variant="h6">
                    Sign in
                </Typography>
                <form className={classes.form} onSubmit={handleSubmit(onSubmit)} noValidate>
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
                        Sign In
                    </Button>
                    <Grid container>
                        <Grid item>
                            <Link
                                to="/register">
                                {"Don't have an account? Sign Up"}
                            </Link>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default Login