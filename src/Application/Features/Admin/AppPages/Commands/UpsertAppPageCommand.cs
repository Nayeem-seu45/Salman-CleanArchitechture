﻿using CleanArchitechture.Application.Features.Admin.AppPages.Queries;
using CleanArchitechture.Domain.Admin;

namespace CleanArchitechture.Application.Features.Admin.AppPages.Commands;

public record UpsertAppPageCommand: AppPageModel, IRequest<Guid>
{
}

internal sealed class UpsertAppPageCommandHandler(IApplicationDbContext dbContext, IMapper mapper) 
    : IRequestHandler<UpsertAppPageCommand, Guid>
{
    public async Task<Guid> Handle(UpsertAppPageCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.AppPages
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(ap => ap.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            entity = mapper.Map<AppPage>(request);
            await dbContext.AppPages.AddAsync(entity, cancellationToken);
        }
        else
        {
            mapper.Map(request, entity);
            dbContext.AppPages.Update(entity);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
