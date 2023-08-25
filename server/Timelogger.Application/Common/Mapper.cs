using System.Collections.Generic;
using System.Linq;

namespace Timelogger.Application.Common;

public class Mapper : IMapper
{
    private readonly AutoMapper.IMapper _mapper;

    public Mapper(AutoMapper.IMapper mapper)
        => _mapper = mapper;

    public TDestination Map<TSource, TDestination>(TSource source)
        => _mapper.Map<TSource, TDestination>(source);

    public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        => source.Select(s => _mapper.Map<TSource, TDestination>(s));
}