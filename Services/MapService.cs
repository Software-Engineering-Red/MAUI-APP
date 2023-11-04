using MauiApp1.Models;
using Microsoft.Maui.Controls.Maps;
using System.Collections.Generic;
using System.Linq;

namespace MauiApp1.Services
{
    public class MapService
    {
        private readonly Map _map;
        private readonly DatabaseOperations _dbOperations;

        public MapService(Map map)
        {
            _map = map;
            _dbOperations = new DatabaseOperations(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "operation_map.db"));
            LoadPins();
        }

        private void LoadPins()
        {
            var pinsFromDb = _dbOperations.GetAllPins();
            foreach (var pin in pinsFromDb)
            {
                AddPinToMap(pin);
            }
        }

        public void AddPinToMap(CustomPin pin)
        {
            var mapPin = new Pin
            {
                Label = pin.Name,
                Position = new Position(pin.Latitude, pin.Longitude),
                Address = pin.StatusOrPurpose,  // Assuming StatusOrPurpose contains address-like info
                Type = PinType.SavedPin
            };
            // Set the type based on PinType if needed, you can adjust the logic here.

            _map.Pins.Add(mapPin);
        }

        // Add methods to toggle visibility, handle zoom, etc.
    }
}
