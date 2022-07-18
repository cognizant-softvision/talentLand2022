import { useState } from "react";
import { CircularProgress } from "@mui/material";
import axios from 'axios';
import {
    CustomPaper, CustomPaperItem, FormDataStep, TriviaQuestionStep,
    ThanksStep, Timeout
} from '../components';

const Trivia = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [isTimeout, setIsTimeout] = useState(false);
    const [data, setData] = useState({
        activeStep: 0,
        instanceId: '',
        userId: '',
        user: {
            name: '',
            email: '',
            university: '',
            company: ''
        },
        question: {
            questionId: '',
            question: '',
            order: 0,
            answers: {
                answerId: '',
                answer: '',
                order: 0
            }
        }
    });

    const initOrchestration = async () => {
        try {
            setIsLoading(true);
            const orchestratorResponse = await axios.post(
                '/api/v1/trivia/startOrchestration', data.user);

            setData({
                ...data,
                activeStep: data.activeStep + 1,
                instanceId: orchestratorResponse.data.data.instanceId,
                userId: orchestratorResponse.data.data.userId,
                question: orchestratorResponse.data.data.question
            });
        }
        catch {
            console.log('Ocurrio un error al iniciar la orquestacion');
        }
        finally {
            setIsLoading(false);
        }
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        await initOrchestration();
    };

    const handleNextStep = async (questionId, selectedAnswerId) => {
        try {
            setIsLoading(true);

            const processAnswerResponse = await axios.post(
                '/api/v1/trivia/processAnswer', {
                instanceId: data.instanceId,
                userId: data.userId,
                questionId: questionId,
                answerId: selectedAnswerId,
                questionNumber: data.activeStep
            });

            setData({
                ...data,
                activeStep: data.activeStep + 1,
                question: processAnswerResponse.data.data.nextQuestion,
            });
        }
        catch {
            console.log(`Se termino el tiempo para la pregunta ${data.activeStep}`);
            setIsTimeout(true);
        }
        finally {
            setIsLoading(false);
        }
    };

    const getStep = (stepNumber) => {
        let step = null;
        switch (stepNumber) {
            case 0: step = <FormDataStep onSubmit={handleSubmit} data={data} setData={setData} />;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5: step = <TriviaQuestionStep activeStep={stepNumber} question={data.question}
                nextStep={handleNextStep} />
                break;
            default: step = <ThanksStep />
                break;
        }

        return step;
    };

    if (isTimeout) {
        return (
            <CustomPaper>
                <CustomPaperItem>
                    <Timeout />
                </CustomPaperItem>
            </CustomPaper>
        )
    }

    return (
        <CustomPaper>
            {isLoading ?
                (<CustomPaperItem>
                    <CircularProgress size={200} />
                </CustomPaperItem>) :
                getStep(data.activeStep)
            }
        </CustomPaper>
    );
}

export default Trivia;