using System;
using System.Collections.Generic;
using System.Text;

namespace GETTER.Application.Common.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
