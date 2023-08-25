import { ProjectTimelineModel } from "./ProjectTimeline.model";

export interface ProjectModel {
    id: string;
    name: string;
    dueDate: Date;
    projectTimelines: ProjectTimelineModel[];
}
