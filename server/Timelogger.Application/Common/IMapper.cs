using System.Collections.Generic;

namespace Timelogger.Application.Common;

public interface IMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
}