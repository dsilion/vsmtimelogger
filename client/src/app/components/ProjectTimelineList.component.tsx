import React, { useState, useEffect } from "react";
import { projectService } from "../api/projects.service";
import { Link, useParams } from 'react-router-dom';
import { ProjectTimelineModel } from "./ProjectTimeline.model";
import { ProjectModel } from "./Project.model";

const ProjectTimelineList=() => {
    const { id } = useParams<{id: string}>();
    const [project, setProject] = useState<ProjectModel>();

    useEffect(() => {
        projectService().fetchById(id).then((result) => { 
             setProject(result);  })
    }, [])

    const timeSpent =(start:Date, end:Date)=>{
        return (new Date(end).getTime()-new Date(start).getTime())/(1000 * 60)
    }
    
    return (
        <>
        <div className="flex items-center my-6">
                <h1>List of timeline for project <strong>{project?.name}</strong></h1>
            </div>

        <table className="table-fixed w-full text-sm text-left text-gray-500 dark:text-gray-400">
            <thead className="text-xs uppercase text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                <tr>
                    <th className="border px-4 py-2">Start Date Time</th>
                    <th className="border px-4 py-2">End Date Time</th>
                    <th className="border px-4 py-2">Time Spent (minutes)</th>
               </tr>
            </thead>
            <tbody>
            {project?.projectTimelines.map((projectTimeline:ProjectTimelineModel) => (
                <tr key={projectTimeline.id} className="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                    <td className="border px-4 py-2">{new Date(projectTimeline.startDatetime).toLocaleString()}</td>
                    <td className="border px-4 py-2">{new Date(projectTimeline.endDatetime).toLocaleString()}</td>
                    <td className="border px-4 py-2">{timeSpent(projectTimeline.startDatetime, projectTimeline.endDatetime)}</td>
                </tr>
             ))}
            </tbody>
        </table>
        <div className="flex items-center my-6">
            <Link to={`/ProjectTimelineAdd/${id}`} 
                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Add Timeline</Link>
                        &nbsp;
            <Link to={`/`} 
                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Return to projects</Link>
        </div>
        </>
    );
}

export default ProjectTimelineList;
