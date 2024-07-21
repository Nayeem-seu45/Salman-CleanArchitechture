﻿using Application.Constants;
using CleanArchitechture.Application.Common.Abstractions.Caching;

namespace CleanArchitechture.Application.Features.Admin.AppMenus.Commands;

public record DeleteAppMenuCommand(Guid Id) : ICacheInvalidatorCommand
{
    public string CacheKey => CacheKeys.AppMenu;
}

internal sealed class DeleteAppMenuCommandHandler(
    IApplicationDbContext dbContext) 
    : ICommandHandler<DeleteAppMenuCommand>
{
    public async Task<Result> Handle(DeleteAppMenuCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.AppMenus.FindAsync(request.Id, cancellationToken);

        if (entity is null) return Result.Failure(Error.NotFound(nameof(entity), ErrorMessages.EntityNotFound));

        dbContext.AppMenus.Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
