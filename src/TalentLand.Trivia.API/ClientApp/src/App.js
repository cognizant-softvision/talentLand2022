import React from "react";
import Trivia from './pages/Trivia';
import Winners from './pages/Winners';
import {
    BrowserRouter,
    Routes,
    Route,
} from "react-router-dom";

const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Trivia />} />
                <Route path="/Winners" element={<Winners />} />
            </Routes>
        </BrowserRouter>
    );
};

export default App;
