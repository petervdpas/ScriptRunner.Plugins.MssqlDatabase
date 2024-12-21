using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ScriptRunner.Plugins.Attributes;
using ScriptRunner.Plugins.Interfaces;
using ScriptRunner.Plugins.Utilities;

namespace ScriptRunner.Plugins.MssqlDatabase;

/// <summary>
///     A plugin that registers and provides several Azure utilities.
/// </summary>
[PluginMetadata(
    name: "MssqlDatabase Plugin",
    description: "Provides ScriptRunner with Mssql Database connectivity",
    author: "Peter van de Pas",
    version: "1.0.0",
    pluginSystemVersion: PluginSystemConstants.CurrentPluginSystemVersion,
    frameworkVersion: PluginSystemConstants.CurrentFrameworkVersion,
    services: ["MssqlDatabase"]
    )]
public class Plugin : BaseAsyncServicePlugin
{
    /// <summary>
    ///     Gets the name of the plugin.
    /// </summary>
    public override string Name => "MssqlDatabase Plugin";

    /// <summary>
    /// Asynchronously initializes the plugin using the provided configuration.
    /// </summary>
    /// <param name="configuration">A dictionary containing configuration key-value pairs for the plugin.</param>
    public override async Task InitializeAsync(IDictionary<string, object> configuration)
    {
        // Simulate async initialization (e.g., loading settings or validating configurations)
        await Task.Delay(100);
        Console.WriteLine(configuration.TryGetValue("DefaultConnectionString", out var defaultConnectionString)
            ? $"DefaultConnectionString value: {defaultConnectionString}"
            : "DefaultConnectionString not found in configuration.");
    }

    /// <summary>
    /// Asynchronously executes the plugin's main functionality.
    /// </summary>
    public override async Task ExecuteAsync()
    {
        // Example execution logic
        await Task.Delay(50);
        Console.WriteLine("MssqlDatabase Plugin executed.");
    }
    
    /// <summary>
    /// Asynchronously registers the plugin's services into the application's dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    public override async Task RegisterServicesAsync(IServiceCollection services)
    {
        // Simulate async service registration (e.g., initializing an external resource)
        await Task.Delay(50);
        
        services.AddSingleton<IDatabase, MssqlDatabase>();
    }
}