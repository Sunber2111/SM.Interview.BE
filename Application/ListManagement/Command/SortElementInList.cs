using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.ListManagement.Constants;
using Common.Errors;
using Common.StatusAction;
using MediatR;

namespace Application.ListManagement.Command
{
    public class SortElementInList
    {
        public class Command : IRequest<ActionSucessWithData<List<int>>>
        {
            public List<int> ListOfNumbers { get; set; }
        }

        public class Handler : IRequestHandler<Command, ActionSucessWithData<List<int>>>
        {

            private void ValidationData(List<int> listOfNumbers)
            {
                if (listOfNumbers.Count < ListManagementConstants.MinOfList)
                {
                    throw new AppError($"Min of list must be more than or equal {ListManagementConstants.MinOfList}");
                }
            }

            public async Task<ActionSucessWithData<List<int>>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await Task.Run(() =>
                {
                    ValidationData(request.ListOfNumbers);

                    const int numOfMiddle = 10, numOfEnd = 10;

                    int middleIndexReplace = (request.ListOfNumbers.Count - numOfMiddle) / 2;

                    var responseData = new List<int>();

                    if (request.ListOfNumbers.Count % 2 == 0)
                    {
                        middleIndexReplace -= 1;
                    }

                    request.ListOfNumbers.Sort();

                    if (request.ListOfNumbers.Count - numOfEnd - numOfMiddle >= numOfEnd)
                    {
                        middleIndexReplace += 1;

                        responseData.AddRange(request.ListOfNumbers.Take(middleIndexReplace));

                        responseData.AddRange(request.ListOfNumbers.TakeLast(numOfMiddle));

                        responseData.AddRange(request.ListOfNumbers.Skip(middleIndexReplace).Take(request.ListOfNumbers.Count - numOfMiddle - middleIndexReplace));
                    }
                    else
                    {
                        responseData.AddRange(request.ListOfNumbers.Take(request.ListOfNumbers.Count - numOfEnd - numOfMiddle));

                        responseData.AddRange(request.ListOfNumbers.TakeLast(numOfMiddle));

                        responseData.AddRange(request.ListOfNumbers.Skip(request.ListOfNumbers.Count - numOfEnd - numOfMiddle).Take(numOfEnd));
                    }


                    return new ActionSucessWithData<List<int>>(responseData);
                });
            }
        }
    }
}
