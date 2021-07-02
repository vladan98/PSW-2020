import React from 'react';
import { Container, Typography, Button, CssBaseline, TextField } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { toast } from 'react-toastify';
import { useForm } from 'react-hook-form';
import { useUserContext } from '../../../../context/UserContext';
import { FeedbackService } from '../../../../service/feedback';


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


const AddFeedback = () => {
    const classes = useStyles();
    const { register, handleSubmit } = useForm();
    const { user } = useUserContext()

    const feedbackService = new FeedbackService(user);

    const onSubmit = async (data) => {
        try {
            const response = await feedbackService.postFeedback(data.title, data.content)
            if (response.status === 200)
                toast.success("Feedback sent.")
        } catch (error) {
            toast.error("Bad credentials.")
        }
    };

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Typography component="h2" variant="h6">
                    Leave Feedback
                </Typography>
                <form className={classes.form} onSubmit={handleSubmit(onSubmit)} noValidate>
                    <TextField
                        data-cy="leave-feedback-title-input"
                        {...register("title")}
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        label="Title"
                        autoFocus
                    />
                    <TextField
                        data-cy="leave-feedback-content-input"
                        {...register("content")}
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        label="Content"
                        multiline
                        rows={6}
                    />
                    <Button
                        data-cy="leave-feedback-submit"
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                    >
                        Post
                    </Button>
                </form>
            </div>
        </Container>
    );
}

export default AddFeedback