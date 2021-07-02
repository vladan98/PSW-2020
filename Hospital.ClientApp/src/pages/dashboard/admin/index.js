import React from 'react';
import { Grid, Typography, CardActionArea, Container, Card, CardContent } from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';


const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    card: {
        marginTop: theme.spacing(3),
    }
}));


const AdminDashboard = () => {
    const classes = useStyles();
    const history = useHistory();

    return (
        <Container component="main" maxWidth="xs">
            <div className={classes.paper}>
                <Grid item xs={12} >
                    <Card className={classes.card}>
                        <CardActionArea onClick={() => history.push("/admin/patients")}>
                            <CardContent>
                                <Typography gutterBottom variant="h5" component="h2">
                                    View Malicious patients
                                </Typography>
                            </CardContent>
                        </CardActionArea>
                    </Card>
                    <Card className={classes.card}>
                        <CardActionArea onClick={() => history.push("/admin/feedback")}>
                            <CardContent>
                                <Typography gutterBottom variant="h5" component="h2">
                                    View feedback
                                </Typography>
                            </CardContent>
                        </CardActionArea>
                    </Card>
                </Grid>
            </div>
        </Container>
    );
}

export default AdminDashboard