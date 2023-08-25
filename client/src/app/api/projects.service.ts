const BASE_URL = "https://localhost:44300/api/v1";

export const projectService = () => {
    return {
        fetchAll: async () => {
            const response = await fetch(`${BASE_URL}/projects`);
            return response.json();
        },
        fetchById: async (id:string) =>{
                const response = await fetch(`${BASE_URL}/projects/${id}/timelines`);
                return response.json();
        },
        
        addTimeline: async (timeline:any) =>{
            const requestOptions = {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(timeline)
            };
            const response = await fetch(`${BASE_URL}/projects/${timeline.projectId}/timelines`, requestOptions);
            return response;
        }
    }
}
