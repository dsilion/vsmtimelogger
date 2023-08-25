import React, { useState } from 'react';
import { projectService } from "../api/projects.service";
import { Link, useParams } from 'react-router-dom';

const ProjectTimelineAdd = ()=> {
  const [timeline, setTimeline] = useState(() => {
    return {
      startDate:null,
      endDate:null
    };
  });
  
  const [message, setMessage] = useState({message:'', isError:false});

  const { id } = useParams<{id: string}>();

  const { startDate, endDate } = timeline;

  const parseFetchResponse = (response: Response) => response.json().then((text: any) => ({
    json: text,
    meta: response
  }));

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    projectService().addTimeline({
      projectId:id,
      startDate,
      endDate
    })
    .then(parseFetchResponse)
    .then(({ json, meta })=> {
      console.log(meta)
      console.log(json)

      if (meta.ok) {
        setMessage({message:'Succesfull registered timeline', isError:false});
      } else {
        setMessage({message:json.error, isError:true});
      }
    }).catch(() => {
      setMessage({message:"The  timeline registration could not be saved!", isError:true});
    });;
  };

  const handleInputChange = (event: { target: { name: any; value: any; }; }) => {
    const { name, value } = event.target;
    setMessage({message:'', isError:false});
    setTimeline((prevState) => ({
      ...prevState,
      [name]: value
    }));
  };
  

  return (
    <>
    <div className="flex items-center my-6">
       <h2>Record Timeline</h2>
    </div>
    {message.isError && message.message &&
      <div className="p-4 mb-4 text-sm text-red-800 rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400" role="alert">
        <span className="font-medium">!</span>   {message.message}
      </div>
    }
    
     {!message.isError && message.message &&
      <div className="p-4 mb-4 text-sm text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400" role="alert">
        <span className="font-medium">!</span>  {message.message}.
      </div>
    }

    <form onSubmit={handleSubmit}>
    <div className="w-full max-w-lg mb-6 ">
        <div className="mb-6">
        <label htmlFor="start-time" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Start Time:</label>
        <input
         className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
          type="datetime-local"
          id="start-time"
          name="startDate"
          onChange={handleInputChange}
        />
      </div>
      <div className="mb-6">
        <label htmlFor="end-time" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">End Time:</label>
        <input
        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
          type="datetime-local"
          id="end-time"
          name="endDate"
          onChange={handleInputChange}
        />
      </div>
      <button type="submit" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Save</button>
      <Link to={`/ProjectTimelineList/${id}`} 
                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Return to timelines</Link>
    </div>
    </form>
    </>
    );
  };

export default ProjectTimelineAdd;