using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using MauiApp1.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MauiApp1
{

/// <summary>
/// Represents the main page for displaying operational map details.
/// </summary>
    public partial class OperationMapPage : ContentPage
    {
         private List<OperationPin> _pins;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationMapPage"/> class.
    /// </summary>
    public OperationMapPage()
    {
        InitializeComponent();
        LoadPinsFromDatabase();
    }

    /// <summary>
    /// Loads pins from the SQLite database and places them on the map.
    /// </summary>
    private void LoadPinsFromDatabase()
    {
        // Assuming you have a method in DatabaseOperations.cs to get all pins
        _pins = DatabaseOperations.GetAllPins();

        foreach (var pin in _pins)
        {
            var mapPin = new Pin
            {
                Label = pin.Label,  // Change 'Name' to 'Label'
                Type = (pin.PinType == "SecurityAlert" || pin.PinType == "OperationalTeam") ? PinType.Place : PinType.SavedPin,
                Position = new Position(pin.Latitude, pin.Longitude)

            };

            OperationalMap.Pins.Add(mapPin);
        }
    }

    /// <summary>
    /// Toggles the visibility of security alert pins on the map.
    /// </summary>
    /// <param name="isVisible">Indicates whether the pins should be visible.</param>
    private void ToggleSecurityAlerts(bool isVisible)
    {
        var securityAlertPins = _pins.Where(p => p.Type == "SecurityAlert");
        foreach (var pin in securityAlertPins)
        {
            // Toggle visibility logic
        }
    }

    /// <summary>
    /// Toggles the visibility of operational team pins on the map.
    /// </summary>
    /// <param name="isVisible">Indicates whether the pins should be visible.</param>
    private void ToggleOperationalTeams(bool isVisible)
    {
        var operationalTeamPins = _pins.Where(p => p.Type == "OperationalTeam");
        foreach (var pin in operationalTeamPins)
        {
            // Toggle visibility logic
        }
    }
    /// <summary>
    /// Zooms in on the map by reducing the visible region.
    /// </summary>
    private void ZoomIn()
    {
        if (OperationalMap.VisibleRegion == null) return;

        var center = OperationalMap.VisibleRegion.Center;
        var halfLatDelta = OperationalMap.VisibleRegion.LatitudeDegrees / 4;
        var halfLongDelta = OperationalMap.VisibleRegion.LongitudeDegrees / 4;

        var newRegion = new MapSpan(center, halfLatDelta * 2, halfLongDelta * 2);
        OperationalMap.MoveToRegion(newRegion);
    }

    /// <summary>
    /// Zooms out from the map by expanding the visible region.
    /// </summary>
    private void ZoomOut()
    {
        if (OperationalMap.VisibleRegion == null) return;

        var center = OperationalMap.VisibleRegion.Center;
        var doubleLatDelta = OperationalMap.VisibleRegion.LatitudeDegrees * 2;
        var doubleLongDelta = OperationalMap.VisibleRegion.LongitudeDegrees * 2;

        var newRegion = new MapSpan(center, doubleLatDelta, doubleLongDelta);
        OperationalMap.MoveToRegion(newRegion);
    }

    /// <summary>
    /// Handles the Zoom In button clicked event.
    /// </summary>
    private void OnZoomInClicked(object sender, EventArgs e)
    {
        ZoomIn();
    }

    /// <summary>
    /// Handles the Zoom Out button clicked event.
    /// </summary>
    private void OnZoomOutClicked(object sender, EventArgs e)
    {
        ZoomOut();
    }
	}
}