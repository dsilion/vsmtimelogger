import React, { useState, useEffect } from "react";
import { projectService } from "../api/projects.service";
import { Link } from 'react-router-dom';
import { ProjectModel } from "./Project.model";


const ProjectList = ()=> {
    const [projects, setProject] = useState<ProjectModel[]>([]);
    const [sorting, setSorting] = useState<{ key: string; ascending: boolean }>({ key: 'dueDate', ascending: true })

    useEffect(() => {
        projectService().fetchAll().then((result) => { 
                setProject(result);
         })
    }, [])

    useEffect(() => {
        const projectsCopy = [...projects];
        const projectsSorted = projectsCopy.sort((a, b) => {
            return a[sorting.key].localeCompare(b[sorting.key]);
          });
      
          setProject(sorting.ascending ? projectsSorted : projectsSorted.reverse());
    }, [sorting]);
   
    const applySorting = (key:string, ascending:boolean) => {
        setSorting({ key: key, ascending: ascending });
    }

    return (
        <>
            <div className="flex items-center my-6">
                <h1>List of projects </h1>
            </div>
            <table className="table-fixed w-full text-sm text-left text-gray-500 dark:text-gray-400">
                <thead className="text-xs uppercase text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th className="border px-4 py-2">Project Name</th>
                        <th className="border px-4 py-2" onClick={() => applySorting('dueDate', !sorting.ascending)}>Due Date</th>
                        <th className="border px-4 py-2">Action</th>
                    </tr>
                </thead>
                <tbody>
                    
                {projects.map((project:ProjectModel) => (
                    <tr key={project.id} className="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                        <td className="border px-4 py-2">{project.name}</td>
                        <td className="border px-4 py-2">{new Date(project.dueDate).toLocaleDateString()}</td>
                        <td className="border px-4 py-2">
                        <Link to={`/ProjectTimelineList/${project.id}`} 
                        className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Timelines</Link></td>
                    </tr>
                    ))}
                </tbody>
            </table>
        </>
    );
}
export default ProjectList;
