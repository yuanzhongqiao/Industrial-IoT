﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace OpcPublisherAEE2ETests.TestExtensions
{
    using Config;
    using Microsoft.Extensions.Configuration;
    using System;
    using Xunit.Abstractions;
    using Microsoft.Azure.Management.Fluent;
    using System.Collections.Generic;

    /// <summary>
    /// Context to pass data between test cases
    /// </summary>
    public class IIoTPlatformTestContext : IDisposable, IDeviceConfig, IIoTHubConfig,
        IIoTEdgeConfig, ISshConfig, IOpcPlcConfig, IContainerRegistryConfig
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        public IIoTPlatformTestContext()
        {
            Configuration = GetConfiguration();
            RegistryHelper = new RegistryHelper(this);
            OutputHelper = null;
        }

        /// <summary>
        /// Save the identifier of OPC server endpoints
        /// </summary>
        public string OpcUaEndpointId { get; set; }

        /// <summary>
        /// Save the identfier of the opc ua application
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Folder path where PublishedNodes file is saved during the test
        /// </summary>
        public string PublishedNodesFileInternalFolder { get; set; }

        /// <summary>
        /// Helper to write output, need to be set from constructor of test class
        /// </summary>
        public ITestOutputHelper OutputHelper { get; set; }

        /// <summary>
        /// Gets or sets the OPC server url
        /// </summary>
        public string OpcServerUrl { get; set; }

        /// <summary>
        /// IoT Device Configuration
        /// </summary>
        public IDeviceConfig DeviceConfig { get { return this; } }

        /// <summary>
        /// IoT Hub Configuration
        /// </summary>
        public IIoTHubConfig IoTHubConfig { get { return this; } }

        /// <summary>
        /// IoT Edge Configuration
        /// </summary>
        public IIoTEdgeConfig IoTEdgeConfig { get { return this; } }

        /// <summary>
        /// SSH Configuration
        /// </summary>
        public ISshConfig SshConfig { get { return this; } }

        /// <summary>
        /// OpcPlc Configuration
        /// </summary>
        public IOpcPlcConfig OpcPlcConfig { get { return this; } }

        /// <summary>
        /// ContainerRegistry Configuration
        /// </summary>
        public IContainerRegistryConfig ContainerRegistryConfig { get { return this; } }

        /// <summary>
        /// Helper to work with Azure.Devices.RegistryManager
        /// </summary>
        public RegistryHelper RegistryHelper { get; }

        /// <summary>
        /// Azure Context for managament api
        /// </summary>
        public IAzure AzureContext { get; set; }

        /// <summary>
        /// Urls for the dynamic ACI containers
        /// </summary>
        public IReadOnlyList<string> PlcAciDynamicUrls { get; set; }

        /// <summary>
        /// Azure Storage Name
        /// </summary>
        public string AzureStorageName { get; set; }

        /// <summary>
        /// Azure Storage Key
        /// </summary>
        public string AzureStorageKey { get; set; }

        /// <summary>
        /// Image that are used for PLC ACI
        /// </summary>
        public string PLCImage { get; set; }

        /// <summary>
        /// Testing suffix for this environment
        /// </summary>
        public string TestingSuffix { get; set; }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Override for disposing
        /// </summary>
        /// <param name="disposing">Indicates if called from <see cref="Dispose"/></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                RegistryHelper.Dispose();
            }
        }

        /// <summary>
        /// Read configuration variable
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetStringOrDefault(string key, Func<string> defaultValue)
        {
            var value = Configuration.GetValue<string>(key);
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue?.Invoke() ?? string.Empty;
            }
            return value.Trim();
        }

        /// <summary>
        /// Get configuration that reads from:
        ///     - environment variables
        ///     - environment variables from user target
        ///     - environment variables from .env file
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddFromDotEnvFile()
                .Build();
        }

        public string DeviceId => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOT_EDGE_DEVICE_ID,
            () => throw new InvalidOperationException("IoT Edge device id is not provided."));

        public string IoTHubConnectionString => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_IOTHUB_CONNSTRING,
            () => throw new InvalidOperationException("IoT Hub connection string is not provided."));

        public string IoTHubEventHubConnectionString => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOTHUB_EVENTHUB_CONNECTIONSTRING,
            () => throw new InvalidOperationException("IoT Hub EventHub connection string is not provided."));

        public string EdgeVersion => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOT_EDGE_VERSION,
            () => "1.4");

        public string Username => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOT_EDGE_VM_USERNAME,
            () => throw new InvalidOperationException("Username of iot edge device is not provided."));

        public string PublicKey => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOT_EDGE_VM_PUBLICKEY,
            () => throw new InvalidOperationException("Public key of iot edge device is not provided."));

        public string PrivateKey => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOT_EDGE_VM_PRIVATEKEY,
            () => throw new InvalidOperationException("Private key of iot edge device is not provided."));

        public string Host => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.IOT_EDGE_DEVICE_DNSNAME,
            () => throw new InvalidOperationException("DNS name of iot edge device is not provided."));

        public string Urls => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PLC_SIMULATION_URLS,
            () => throw new InvalidOperationException("Semicolon separated list of URLs of OPC-PLCs is not provided."));

        public string TenantId => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_AUTH_TENANT,
            () => GetStringOrDefault("AZURE_TENANT_ID", () => throw new InvalidOperationException("Tenant Id is not provided.")));

        public string ResourceGroupName => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_RESOURCE_GROUP,
            () => throw new InvalidOperationException("Resource Group Name is not provided."));

        public string SubscriptionId => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_SUBSCRIPTION_ID, () => string.Empty);

        public string ContainerRegistryServer => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_DOCKER_SERVER,
            () => string.Empty);

        public string ContainerRegistryUser => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_DOCKER_USER,
            () => string.Empty);

        public string ContainerRegistryPassword => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_DOCKER_PASSWORD,
            () => string.Empty);

        public string ImagesNamespace => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_IMAGES_NAMESPACE,
            () => string.Empty);

        public string ImagesTag => GetStringOrDefault(TestConstants.EnvironmentVariablesNames.PCS_IMAGES_TAG,
            () => "latest");
    }
}
