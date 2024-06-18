﻿using System.Text.Json.Serialization;

namespace CleanArchitechture.Application.Features.Admin.AppMenus.Queries;
//[Authorize(Policy = Permissions.Admin.AppMenus.View)]
public record GetAppMenuByIdQuery(Guid Id) : ICacheableQuery<AppMenuModel?>
{
    [JsonIgnore]
    public string CacheKey => $"AppMenu_{Id}";
    [JsonIgnore]
    public TimeSpan? Expiration => null;
    public bool? AllowCache => false;

}

internal sealed class GetAppMenuByIdQueryHandler(ISqlConnectionFactory sqlConnection)
    : IQueryHandler<GetAppMenuByIdQuery, AppMenuModel?>
{
    public async Task<Result<AppMenuModel?>> Handle(GetAppMenuByIdQuery query, CancellationToken cancellationToken)
    {
        var connection = sqlConnection.GetOpenConnection();

        var sql = $"""
            SELECT 
                M.Id AS {nameof(AppMenuModel.Id)}, 
                M.Label AS {nameof(AppMenuModel.Label)}, 
                M.Url AS {nameof(AppMenuModel.Url)}, 
                M.ParentId AS {nameof(AppMenuModel.ParentId)}, 
                M.Description AS {nameof(AppMenuModel.Description)},
                M.IsActive AS {nameof(AppMenuModel.IsActive)},
                M.Visible AS {nameof(AppMenuModel.Visible)},
                M.OrderNo AS {nameof(AppMenuModel.OrderNo)},
                M.Tooltip AS {nameof(AppMenuModel.Tooltip)},
                M.Icon AS {nameof(AppMenuModel.Icon)}
            FROM dbo.AppMenus AS M
            WHERE M.Id = @Id
            """;

        return await connection.QueryFirstOrDefaultAsync<AppMenuModel>(sql, new { query.Id });
    }
}
