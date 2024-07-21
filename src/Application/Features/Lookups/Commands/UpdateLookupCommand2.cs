﻿using Application.Constants;
using CleanArchitechture.Application.Common.Abstractions.Caching;
using CleanArchitechture.Application.Common.Events;
using CleanArchitechture.Domain.Common;

namespace CleanArchitechture.Application.Features.Lookups.Commands;

public record UpdateLookupCommand2(
    Guid Id,
    string Name,
    string Code,
    string Description,
    bool Status,
    Guid? ParentId) : ICommand;

internal sealed class UpdateLookupCommandHandler2(
    IApplicationDbContext dbContext,
    IPublisher publisher) 
    : ICommandHandler<UpdateLookupCommand2>
{
    public async Task<Result> Handle(UpdateLookupCommand2 request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Lookups.FindAsync(request.Id, cancellationToken);

        if (entity is null) return Result.Failure(Error.NotFound(nameof(entity), ErrorMessages.EntityNotFound));

        bool oldStatus = entity.Status;

        entity.Name = request.Name;
        entity.Code = request.Code;
        entity.Description = request.Description;
        entity.Status = request.Status;
        entity.ParentId = request.ParentId;

        entity.AddDomainEvent(new LookupUpdatedEvent(entity));

        await dbContext.SaveChangesAsync(cancellationToken);

        await publisher.Publish(
new CacheInvalidationEvent { CacheKey = CacheKeys.Lookup });

        if (oldStatus != request.Status)
        {
            await publisher.Publish(
    new CacheInvalidationEvent { CacheKey = CacheKeys.LookupDetail });
        }

        return Result.Success();
        //return Result.Success(CommonMessage.SAVED_SUCCESSFULLY);
    }
}
