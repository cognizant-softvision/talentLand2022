import { useState, useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { CircularProgress, Typography } from "@mui/material";
import {
    CustomPaper, CustomPaperItem } from '../components';
import axios from 'axios';
import createStyles from '@mui/styles/createStyles';
import makeStyles from '@mui/styles/makeStyles';

const useStyles = makeStyles((theme) =>
    createStyles({
        div: {
            height: 400,
            width: '100%'
        },
        title: {
            paddingBottom: 20,
            marginBottom: 20
        },
    })
)

const columns = [
    { field: 'id', headerName: 'No. ', width: 100 },
    { field: 'name', headerName: 'Nombre', width: 300 },
    { field: 'numberOfRightAnswers', headerName: 'Resp. Correctas', width: 130 },
    { field: 'answeredDate', headerName: 'Tiempo de respuesta (s)', width: 250 },
];

const Winners = () => {
    const classes = useStyles();
    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    const getallUsers = async () => {
        setIsLoading(true);
        try {
            const users = await axios.get('/api/v1/user/getAllUsers?offset=0&limit=50');
            setData(users.data.data.users.map((m, index) => {
                return {
                    id: index + 1,
                    name: m.name,
                    numberOfRightAnswers: m.qa.numberOfRightAnswers,
                    answeredDate: m.qa.answeredDate
                }
            }));
        }
        catch {
            console.log("Ocurrio un error al obtener usuarios");
        }
        finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        getallUsers();
    }, []);

    return (
        <CustomPaper>
            {isLoading ?
                (<CustomPaperItem>
                    <CircularProgress size={200} />
                </CustomPaperItem>) :
                (<div className={classes.div}>                    
                    <Typography variant="h6" color="textSecondary" className={ classes.title }>
                        Ganadores de la Trivia
                    </Typography>
                    <DataGrid
                        rows={data}
                        columns={columns}
                        pageSize={5}
                        rowsPerPageOptions={[5]}
                        checkboxSelection
                    />
                </div>)
            }

        </CustomPaper>
    );
};

export default Winners;