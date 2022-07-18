import { Grid, Paper } from "@mui/material";
import createStyles from '@mui/styles/createStyles'
import makeStyles from '@mui/styles/makeStyles'
import Logo from './Logo'

const useStyles = makeStyles((theme) =>
    createStyles({
        paper: {
            textAlign: 'center',
            alignItems: 'center',
            margin: '3%',
            padding: '3%',
            minHeight: '500px',
        },
        gridContainer: {
            textAlign: 'center',
            alignItems: 'center',
        },
    })
)

const CustomPaper = ({ children }) => {
    const classes = useStyles();

    return (
        <>
            <Logo />
            <Paper elevation={8} className={classes.paper} sx={{ display: 'flex' }} square={false}>
                <Grid container justify="center" direction="column" className={classes.gridContainer}>
                    {children}
                </Grid>
            </Paper>
        </>
    )
};

export default CustomPaper;