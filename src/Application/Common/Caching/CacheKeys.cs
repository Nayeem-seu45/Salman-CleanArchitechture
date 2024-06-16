﻿namespace CleanArchitechture.Application.Common.Caching;

public static class CacheKeys
{
    public const string Lookup = nameof(Lookup);
    public const string Lookup_All_SelectList = nameof(Lookup_All_SelectList);

    public const string LookupDetail = nameof(LookupDetail);
    public const string LookupDetail_All_SelectList = nameof(LookupDetail_All_SelectList);

    #region Admin

    public const string AppUser = nameof(AppUser);
    public const string Role = nameof(Role);
    public const string Role_All_SelectList = nameof(Role_All_SelectList);

    #endregion
}
