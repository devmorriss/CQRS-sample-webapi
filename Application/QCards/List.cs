using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.QCards
{
    public class List
    {
        public class Query : IRequest<Result<List<QCard>>> { }
        public class Handler : IRequestHandler<Query, Result<List<QCard>>>
        {
            private readonly MyDatabase _myDatabase;
            public Handler(MyDatabase myDatabase)
            {
                _myDatabase = myDatabase;
            }

            public Task<Result<List<QCard>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(Result<List<QCard>>.Success(_myDatabase.MyCards));
            }
        }
    }
}