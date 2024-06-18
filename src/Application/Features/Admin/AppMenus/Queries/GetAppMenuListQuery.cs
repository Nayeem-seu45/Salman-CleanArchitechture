﻿using System.Text.Json.Serialization;
using static CleanArchitechture.Application.Common.DapperQueries.SqlConstants;

namespace CleanArchitechture.Application.Features.Admin.AppMenus.Queries;

//[Authorize(Policy = Permissions.Admin.AppMenus.View)]
public record GetAppMenuListQuery
    : DataGridModel , ICacheableQuery<PaginatedResponse<AppMenuModel>>
{
    [JsonIgnore]
    public string CacheKey => $"AppMenu_{Offset}_{PageSize}";
}

internal sealed class GetAppMenuListQueryHandler(ISqlConnectionFactory sqlConnection) 
    : IQueryHandler<GetAppMenuListQuery, PaginatedResponse<AppMenuModel>>
{
    public async Task<Result<PaginatedResponse<AppMenuModel>>> Handle(GetAppMenuListQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnection.GetOpenConnection();

        var sql = $"""
            SELECT 
                M.Id AS {nameof(AppMenuModel.Id)}, 
                M.Label AS {nameof(AppMenuModel.Label)}, 
                M.Url AS {nameof(AppMenuModel.Url)}, 
                M.ParentId AS {nameof(AppMenuModel.ParentId)}, 
                P.Label AS {nameof(AppMenuModel.ParentName)} , 
                M.Description AS {nameof(AppMenuModel.Description)},
                IIF(M.IsActive = 1, 'Active', 'Inactive') AS {nameof(AppMenuModel.Active)},
                IIF(M.Visible = 1, 'Yes', 'No') AS {nameof(AppMenuModel.Visibility)}
            FROM dbo.AppMenus AS M
            LEFT JOIN dbo.AppMenus AS P ON P.Id = M.ParentId
            """;

        return await PaginatedResponse<AppMenuModel>
            .CreateAsync(connection, sql, request, dataFields: AppMenuModel.DataFields);
            
    }
}
