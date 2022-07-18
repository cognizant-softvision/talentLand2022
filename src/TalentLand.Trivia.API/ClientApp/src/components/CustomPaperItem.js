import { Grid } from "@mui/material";
import createStyles from '@mui/styles/createStyles'
import makeStyles from '@mui/styles/makeStyles'

const useStyles = makeStyles((theme) =>
    createStyles({
        item: {
            paddingBottom: 20
        },
    })
)

const CustomPaperItem = ({ children }) => {
    const classes = useStyles();

    return (
        <Grid item className={classes.item}>
            {children}
        </Grid>
    )
};

export default CustomPaperItem;