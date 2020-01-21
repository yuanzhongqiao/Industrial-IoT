/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

package com.microsoft.azure.iiot.opc.registry.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Discoverer registration query.
 */
public class DiscovererQueryApiModel {
    /**
     * Site of the discoverer.
     */
    @JsonProperty(value = "siteId")
    private String siteId;

    /**
     * Possible values include: 'Off', 'Local', 'Network', 'Fast', 'Scan'.
     */
    @JsonProperty(value = "discovery")
    private DiscoveryMode discovery;

    /**
     * Included connected or disconnected.
     */
    @JsonProperty(value = "connected")
    private Boolean connected;

    /**
     * Get site of the discoverer.
     *
     * @return the siteId value
     */
    public String siteId() {
        return this.siteId;
    }

    /**
     * Set site of the discoverer.
     *
     * @param siteId the siteId value to set
     * @return the DiscovererQueryApiModel object itself.
     */
    public DiscovererQueryApiModel withSiteId(String siteId) {
        this.siteId = siteId;
        return this;
    }

    /**
     * Get possible values include: 'Off', 'Local', 'Network', 'Fast', 'Scan'.
     *
     * @return the discovery value
     */
    public DiscoveryMode discovery() {
        return this.discovery;
    }

    /**
     * Set possible values include: 'Off', 'Local', 'Network', 'Fast', 'Scan'.
     *
     * @param discovery the discovery value to set
     * @return the DiscovererQueryApiModel object itself.
     */
    public DiscovererQueryApiModel withDiscovery(DiscoveryMode discovery) {
        this.discovery = discovery;
        return this;
    }

    /**
     * Get included connected or disconnected.
     *
     * @return the connected value
     */
    public Boolean connected() {
        return this.connected;
    }

    /**
     * Set included connected or disconnected.
     *
     * @param connected the connected value to set
     * @return the DiscovererQueryApiModel object itself.
     */
    public DiscovererQueryApiModel withConnected(Boolean connected) {
        this.connected = connected;
        return this;
    }

}