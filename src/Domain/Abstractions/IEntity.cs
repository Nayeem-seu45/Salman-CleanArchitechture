﻿namespace CleanArchitechture.Domain.Abstractions;

internal interface IEntity
{
}

internal interface IEntity<T> : IEntity
{
    T Id { get; set; }
}
