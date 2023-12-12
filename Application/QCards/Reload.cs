using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.QCards
{
    public class Reload
    {
        public class Command : IRequest<Result<Receipt>>
        {
            public int Id { get; set; }
            public int AmountToPay { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Receipt>>
        {
            private readonly MyDatabase _myDatabase;
            public Handler(MyDatabase myDatabase)
            {
                _myDatabase = myDatabase;

            }
            public Task<Result<Receipt>> Handle(Command request, CancellationToken cancellationToken)
            {
                var cardToLoad = _myDatabase.MyCards.SingleOrDefault(x => x.Id == request.Id);

                if (cardToLoad is null)
                    return Task.FromResult(Result<Receipt>.Failure("Error reading card"));

                if (request.AmountToPay < 100 || request.AmountToPay > 1000)
                    return Task.FromResult(Result<Receipt>.Failure("The amount is not acceptable, ensure that is it only in the range of 100 to 1000 PHP"));

                cardToLoad.Amount += request.AmountToPay;

                var receipt = new Receipt
                {
                    CardId = cardToLoad.Id,
                    Status = "SUCCESS",
                    Message = $"Thank you for reloading, you now have ${cardToLoad.Amount} PHP in your card #{cardToLoad.Id}"
                };

                return Task.FromResult(Result<Receipt>.Success(receipt));
            }
        }
    }
}