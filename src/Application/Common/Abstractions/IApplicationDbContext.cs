﻿using CleanArchitechture.Domain.Admin;
using CleanArchitechture.Domain.Common;

namespace CleanArchitechture.Application.Common.Abstractions;

public interface IApplicationDbContext
{
    //DbSet<RefreshToken> RefreshTokens { get; }

    #region Admin

    DbSet<AppMenu> AppMenus { get; }
    #endregion

    #region Common Setup
    DbSet<Lookup> Lookups { get; }

    DbSet<LookupDetail> LookupDetails { get; }

    #endregion

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
