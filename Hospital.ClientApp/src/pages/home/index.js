import React, { useEffect, useState } from 'react';
import { Grid, CircularProgress, Typography, Container, Card, CardContent } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { FeedbackService } from '../../service/feedback';
import { toast } from 'react-toastify';

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(6),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    feedback: {
        justifyItems: "center",
        width: '70%',
        marginTop: theme.spacing(3),
    },
    card: {
        marginTop: theme.spacing(3),
    },
    center: {
        paddingLeft: "auto",
        paddingRight: "auto",
    }
}));


const Home = () => {
    const classes = useStyles();
    const [loading, setLoading] = useState(false);
    const [feedback, setFeedback] = useState([]);


    useEffect(() => {
        const feedbackService = new FeedbackService();

        const sendRequest = async () => {
            setLoading(true)
            try {
                const feedback = await feedbackService.getPublishedFeedback();
                setFeedback(feedback)
            } catch (e) {
                toast.error("Network error.")
            }
            setLoading(false)
        }
        sendRequest();

    }, [])

    return (
        <Container component="main" maxWidth="md">
            <div className={classes.paper}>
                <Typography component="h1" variant="h5">
                    Welcome to Hospital service
                </Typography>

                <Typography component="h2" variant="h6">
                    Check out feedback from our patients
                </Typography>
                <Grid container className={classes.feedback}>
                    <Grid item align="center" xs={12} >
                        {!loading ?
                            feedback.length > 0 ? feedback.map(f => (
                                <Grid item xs={12} key={f.id} data-cy={`feedback-item-${f.id}`} >
                                    <Card className={classes.card}>
                                        <CardContent>
                                            <Typography gutterBottom variant="h5" component="h2">
                                                {f.title}
                                            </Typography>
                                            <Typography variant="body2" color="textSecondary" component="p">
                                                {f.content}
                                            </Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                            )) :
                                <Typography align="center" component="h3" variant="h6">
                                    There is no published feedback yet.
                                </Typography>
                            :
                            <CircularProgress />
                        }
                    </Grid>
                </Grid>
            </div>
        </Container>
    );
}

export default Home