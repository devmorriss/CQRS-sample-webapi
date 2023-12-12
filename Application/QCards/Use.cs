using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.QCards
{
    public class Use
    {
        public class Command : IRequest<Result<QCard>>
        {
            public int Id;
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
                var myCardCount = _myDatabase.MyCards.Count;
                if (myCardCount < 1)
                    return Task.FromResult(Result<QCard>.Failure("You cannot ride since you do not have a card"));

                var card = _myDatabase.MyCards.FirstOrDefault(x => x.Id == request.Id);
                var today = DateTime.Now;

                if (card is null)
                    return Task.FromResult(Result<QCard>.Failure("Failed to use card"));

                if (card.ExpiryDate < today)
                    return Task.FromResult(Result<QCard>.Failure("Card is expired"));

                if (card.IsExit && (card.Amount <= card.FareAmount))
                    return Task.FromResult(Result<QCard>.Failure("Card has insufficient amount, reload now"));

                if (card.IsExit && (card.Amount >= card.FareAmount))
                    card.Amount -= card.FareAmount;

                card.IsExit = !card.IsExit;

                return Task.FromResult(Result<QCard>.Success(card));

            }
        }

    }
}