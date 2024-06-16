﻿using System.Text.Json.Serialization;
using CleanArchitechture.Application.Common.Abstractions.Identity;

namespace CleanArchitechture.Application.Features.Admin.Roles.Commands;

public record UpdateRoleCommand(
     string Id,
     string name
    ) : ICacheInvalidatorCommand
{
    [JsonIgnore]
    public string CacheKey => CacheKeys.Role;
}

internal sealed class UpdateRoleCommandHandler(IIdentityRoleService roleService) 
    : ICommandHandler<UpdateRoleCommand>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.UpdateRoleAsync( request, cancellationToken );
    }
}
