import { useState, useEffect } from 'react';
import { Button, Radio, RadioGroup, FormLabel, FormControl, FormControlLabel, Typography } from '@mui/material';
import Timeout from './Timeout';
import createStyles from '@mui/styles/createStyles'
import makeStyles from '@mui/styles/makeStyles'

const useStyles = makeStyles((theme) =>
    createStyles({
        radioGroup: {
            marginTop: 10,
            marginBottom: 20,
        },
        formControl: {
            textAlign: 'center',
            alignItems: 'center',
            marginTop: 10,
            marginBottom: 20,
            paddingTop: 10,
            paddingBottom: 20
        },
        button: {
            color: 'primary',
            width: 280,
            height: 50,
        },
        timeoutButton: {
            marginTop: 10,
            marginBottom: 20,
            paddingTop: 10,
            paddingBottom: 20
        }
    })
)

const TriviaQuestionStep = ({ activeStep, question, nextStep }) => {
    const classes = useStyles();
    const [selectedAnswerId, setSelectedAnswerId] = useState(null);
    const [countdownTime, setCountdownTime] = useState(10);

    useEffect(() => {
        const interval = setInterval(() => {
            if (countdownTime > 0) {
                setCountdownTime(countdownTime - 1);
            }
        }, 1000);

        return () => {
            clearInterval(interval);
        }
    }, [countdownTime]);

    const handleChange = (event) => {
        setSelectedAnswerId(event.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        setCountdownTime(-1);
        nextStep(question.questionId, selectedAnswerId)
    }

    if (countdownTime === 0) {
        setCountdownTime(-1);
        nextStep(question.questionId, null)

        return (
            <Timeout />
        )
    }

    return (
        <form onSubmit={handleSubmit} className={classes.formControl}>
            <FormControl className={classes.formControl} >
                <FormLabel id="question-form-label">
                    <Typography variant="h6" color="textPrimary" >
                        {activeStep}. {question.question}
                    </Typography>
                </FormLabel>
                <RadioGroup
                    aria-labelledby="question-radio-buttons-group"
                    name="question-radio-group"
                    className={classes.radioGroup}
                >
                    {
                        question.answers.map(a => {
                            return (
                                <FormControlLabel key={a.answerId}
                                    value={a.answerId}
                                    control={<Radio onChange={handleChange} />}
                                    label={<Typography variant="body1"
                                        color="textSecondary">{a.answer}</Typography>} />
                            )
                        })
                    }
                </RadioGroup>
                <Button variant="contained" className={classes.button} type="submit"
                    disabled={selectedAnswerId === null}>
                    Mandar respuesta
                </Button>
                <Typography variant="body1" color="secondary" className={classes.timeoutButton}>
                    Debes de contestar en ... {countdownTime}
                </Typography>
            </FormControl>
        </form>
    )
}

export default TriviaQuestionStep;