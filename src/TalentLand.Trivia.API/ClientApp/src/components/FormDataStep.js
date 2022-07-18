import { useState } from 'react';
import CustomPaperItem from './CustomPaperItem';
import { Typography, TextField, Button } from "@mui/material";
import createStyles from '@mui/styles/createStyles';
import makeStyles from '@mui/styles/makeStyles';
import { validator } from "../utils/Validator";

const useStyles = makeStyles((theme) =>
    createStyles({
        textField: {
            width: '100%',
        },
        button: {
            color: 'primary',
            width: 280,
            height: 50
        },
    })
)

const FormDataStep = ({ onSubmit, data, setData }) => {
    const classes = useStyles();
    const [errors, setErrors] = useState({});

    let isValidForm =
        Object.values(errors).filter(error => typeof error !== "undefined")
            .length === 0;

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setData({
            ...data,
            user: {
                ...data.user,
                [name]: value,
            }
        });
    };

    const handleBlur = e => {
        const { name: fieldName } = e.target;
        const faildFiels = validator(data.user, fieldName);
        setErrors(() => ({
            ...errors,
            [fieldName]: Object.values(faildFiels)[0]
        }));
    };

    return (
        <form onSubmit={onSubmit}>
            <CustomPaperItem>
                <Typography
                    className={classes.textField} variant="h2"
                >
                    Trivia
                </Typography>
            </CustomPaperItem>
            <CustomPaperItem>
                <Typography
                    className={classes.textField}
                >
                    Por favor registra tus datos para iniciar.
                </Typography>
            </CustomPaperItem>
            <CustomPaperItem>
                <TextField
                    required
                    className={classes.textField}
                    id="name-input"
                    name="name"
                    label="Nombre"
                    type="text"
                    value={data.name}
                    onChange={handleInputChange}
                    error={errors.name ? true : false}
                    helperText={errors.name}
                    onBlur={handleBlur}
                />
            </CustomPaperItem>
            <CustomPaperItem>
                <TextField
                    required
                    className={classes.textField}
                    id="email-input"
                    name="email"
                    label="Email"
                    type="text"
                    value={data.email}
                    onChange={handleInputChange}
                    error={errors.email ? true : false}
                    helperText={errors.email}
                    onBlur={handleBlur}
                />
            </CustomPaperItem>
            <CustomPaperItem>
                <TextField
                    className={classes.textField}
                    id="university-input"
                    name="university"
                    label="Universidad"
                    type="text"
                    value={data.university}
                    onChange={handleInputChange}
                    onBlur={handleBlur}
                />
            </CustomPaperItem>
            <CustomPaperItem>
                <TextField
                    className={classes.textField}
                    id="company-input"
                    name="company"
                    label="Empresa"
                    type="text"
                    value={data.company}
                    onChange={handleInputChange}
                    onBlur={handleBlur}
                />
            </CustomPaperItem>
            <Button variant="contained" className={classes.button} type="submit"
                disabled={!isValidForm}>
                Comenzar con la trivia
            </Button>
        </form>
    )
};

export default FormDataStep;