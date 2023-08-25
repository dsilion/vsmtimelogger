using AutoMapper;
using Timelogger.Application.Projects;
using Timelogger.Entities;

namespace Timelogger.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectTimeline, ProjectTimelineDto>();
    }
}