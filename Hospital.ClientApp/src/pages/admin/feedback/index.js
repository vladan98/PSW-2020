import React, { useEffect, useMemo, useState } from 'react';
import { Grid, Typography, TableRow, TableCell, TableBody, CircularProgress, TableHead, Table, TableContainer, Container, Button, Paper } from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';
import { useUserContext } from '../../../context/UserContext';
import { toast } from 'react-toastify';
import { FeedbackService } from '../../../service/feedback';


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

const FeedbackList = () => {
    const classes = useStyles();
    const { user } = useUserContext()
    const [loading, setLoading] = useState(false);
    const [feedback, setFeedback] = useState([]);

    const feedbackService = useMemo(() => new FeedbackService(user), [user])

    useEffect(() => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const feedback = await feedbackService.getAllFeedback();
                setFeedback(feedback)
            } catch (e) {
                toast.error("Failed fetching feedback.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [feedbackService])

    const publishFeedback = (id) => {
        const sendRequest = async () => {
            setLoading(true)
            try {
                const response = await feedbackService.updateFeedback(id)
                const feedback = await feedbackService.getAllFeedback();
                setFeedback(feedback)
                toast.success(response)
            } catch (e) {
                toast.error("You are not allowed to update feedback.")
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
                        feedback && feedback.length > 0 ?
                            <TableContainer component={Paper}>
                                <Table className={classes.table} size="small" aria-label="a dense table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell align="center">Title</TableCell>
                                            <TableCell align="center">Content</TableCell>
                                            <TableCell align="center">Action</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {feedback.map(singleFeedback => (
                                            <TableRow key={singleFeedback.id}>
                                                <TableCell align="center">{singleFeedback.title}</TableCell>
                                                <TableCell align="center">{singleFeedback.content}</TableCell>
                                                <TableCell align="center">
                                                    <Button
                                                        data-cy={`feedback-list-item-${singleFeedback.id}`}
                                                        onClick={() => publishFeedback(singleFeedback.id)}
                                                        color="primary"
                                                    >
                                                        {!singleFeedback.published ? "Publish" : " Unpublish"}
                                                    </Button>
                                                </TableCell>
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                            :
                            <Typography align="center" component="h3" variant="h6">
                                No feedback .
                            </Typography>
                        :
                        <CircularProgress />
                    }

                </Grid>
            </div>
        </Container>
    );
}

export default FeedbackList