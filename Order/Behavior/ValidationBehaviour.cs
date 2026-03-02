using FluentValidation;
using MediatR;

namespace Order.Behavior
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                //Run all the validators one by one and return the validation results.
                var validationResults = await Task.WhenAll(_validators.Select(validations => validations.ValidateAsync(context)));
                //Check for failures
                var failures = validationResults.SelectMany(errors => errors.Errors).Where(failures => failures != null).ToList();
                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }

            }
                //On success case
                return await next();
        }
    }
}
