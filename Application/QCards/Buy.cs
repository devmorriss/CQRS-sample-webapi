using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.QCards
{
    public class Buy
    {
        public class Command : IRequest<Result<QCard>>
        {
            public string? Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<QCard>>
        {
            private readonly MyDatabase _myDatabase;
            public Handler(MyDatabase myDatabase)
            {
                _myDatabase = myDatabase;
            }
            public Task<Result<QCard>> Handle(Command request, CancellationToken cancellationToken)
            {
                QCard cardToReturn;

                if (request.Id is null)
                {
                    var standardCard = new QCard();
                    cardToReturn = standardCard;
                }
                else
                {
                    var seniorCard = new DiscountedQCard();
                    cardToReturn = seniorCard;
                }

                Console.WriteLine(cardToReturn);
                _myDatabase.MyCards.Add(cardToReturn);
                return Task.FromResult(Result<QCard>.Success(cardToReturn));
            }
        }
    }
}