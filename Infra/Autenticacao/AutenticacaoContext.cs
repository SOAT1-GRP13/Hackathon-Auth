﻿using Domain.Autenticacao;
using Domain.Autenticacao.Enums;
using Domain.Base.Data;
using Domain.Base.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace Infra.Autenticacao;

public class AutenticacaoContext : DbContext, IUnitOfWork
{
    protected readonly IConfiguration _configuration;
    public AutenticacaoContext(DbContextOptions<AutenticacaoContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<AcessoUsuario> AcessoUsuario => Set<AcessoUsuario>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (!string.IsNullOrEmpty(env) && env == "Test")
            optionsBuilder.UseInMemoryDatabase("auth");
        else
            optionsBuilder.UseNpgsql(_configuration.GetSection("ConnectionString").Value);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        var types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces()
            .Any(i => i.IsGenericType &&
                      i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                      types.Contains(i.GenericTypeArguments[0])));

        //Adicionando usuario padrão
        //Senha Teste@123
        modelBuilder.Entity<AcessoUsuario>()
        .HasData(
            new AcessoUsuario(
                "2200101",
                "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C",
                "jose.silva@gmail.com",
                "José Silva",
                Roles.Usuario),
            new AcessoUsuario(
                "2300102",
                "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C",
                "renata.barros@gmail.com",
                "Renata Barros",
                Roles.Usuario),
            new AcessoUsuario(
                "2400103",
                "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C",
                "felipe.okagawa@gmail.com",
                "Felipe Okagawa",
                Roles.Usuario)
        );

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        return await base.SaveChangesAsync() > 0;
    }
}
