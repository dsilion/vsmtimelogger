import * as React from "react";
import { BrowserRouter } from 'react-router-dom';
import * as ReactDOM from "react-dom";
import Application from "./app/App";

ReactDOM.render(<BrowserRouter><Application /></BrowserRouter>, document.getElementById("root"));
