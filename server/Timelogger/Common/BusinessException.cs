using System;

namespace Timelogger.Common;

public class BusinessException : Exception
{
    public BusinessException(string code)
        : base(code)
    {
    }
}