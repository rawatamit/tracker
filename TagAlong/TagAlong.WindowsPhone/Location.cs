using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Threading;

namespace TagAlong
{
    public class Location
    {
    }

    // this class provides functionality
    // to access the current location of a person
    public class CurrentLocation : Location
    {
        private Geolocator _geolocator = null;
        private CancellationTokenSource _cts = null;
        private bool _satelliteInfo = false;
        private string _latitude = null;
        private string _longitude = null;
        public CurrentLocation()
        {
            _geolocator = new Geolocator();
        }

        public bool GetSatelliteInfo()
        {
            return _satelliteInfo;
        }

        public string GetLatitude()
        {
            return _latitude;
        }

        public string GetLongitude()
        {
            return _latitude;
        }

        async public void GetGeolocation(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;
                Geoposition position = await _geolocator.GetGeopositionAsync().AsTask(token);
                _latitude = position.Coordinate.Point.Position.Latitude.ToString();
                _longitude = position.Coordinate.Point.Position.Longitude.ToString();

                // check if the satellite is enabled
                if (position.Coordinate.PositionSource == PositionSource.Satellite)
                {
                    _satelliteInfo = true;
                }
                else
                {
                    _satelliteInfo = false;
                }
            }
            catch (Exception)
            {
                _latitude = null;
                _longitude = null;
                _satelliteInfo = false;
            }
            finally
            {
                _cts = null;
            }
        }
    }
}
