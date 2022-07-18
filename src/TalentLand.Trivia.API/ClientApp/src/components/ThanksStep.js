import { Typography } from "@mui/material";
import EmojiEmotionsIcon from '@mui/icons-material/EmojiEmotions';
import createStyles from '@mui/styles/createStyles'
import makeStyles from '@mui/styles/makeStyles'

const useStyles = makeStyles((theme) =>
    createStyles({
        emotionIcon: {          
            marginBottom: 10
        },
    })
)

const ThanksStep = () => {
    const classes = useStyles();

    return (
        <>
            <EmojiEmotionsIcon color="success" fontSize="large" className={classes.emotionIcon} />
            <Typography variant="body1"
                color="textSecondary"> Gracias por participar, buena suerte !!</Typography>
        </>
    )
};

export default ThanksStep;