import * as React from "react";
import "./style.css";
import { Route, Switch } from "react-router-dom";
import ProjectList from "./components/ProjectList.component";
import ProjectTimelineList from "./components/ProjectTimelineList.component";
import ProjectTimelineAdd from "./components/ProjectTimelineAdd.component";


export default function App() {
    return (
        <>
            <header className="bg-gray-900 text-white flex items-center h-12 w-full">
                <div className="container mx-auto">
                    <a className="navbar-brand" href="/">
                        Timelogger
                    </a>
                </div>
            </header>

            <main>
                <div className="container mx-auto">
                    <Switch>
                        <Route component={ProjectList} path="/" exact={true} />
                        <Route component={ProjectTimelineList} path="/ProjectTimelineList/:id" />
                        <Route component={ProjectTimelineAdd} path="/ProjectTimelineAdd/:id" />
                    </Switch>
                </div>
            </main>
        </>
    );
}
