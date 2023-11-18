﻿using FluentValidation;
using MediatR;

namespace Notes.Application.Common.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators) {
        _validators = validators;
    }

    //public Task<TResponse> Handle(TRequest request, 
    //    CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) 
    //{ }

    public Task<TResponse> Handle(TRequest request, 
                                  RequestHandlerDelegate<TResponse> next, 
                                  CancellationToken cancellationToken) 
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(res => res.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (failures.Count != 0) {
            throw new ValidationException(failures);
        }
        return next();
    }
}