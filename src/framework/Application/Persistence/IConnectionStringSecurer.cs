﻿namespace FSH.Framework.Application.Persistence;

public interface IConnectionStringSecurer
{
    string? MakeSecure(string? connectionString, string? dbProvider = null);
}