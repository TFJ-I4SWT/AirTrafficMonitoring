﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Domain
{
    public class Airspace
    {
        private ITrack _track;

        public bool DefineAirspace(ITrack track)
        {
            if (_track.XCoordinate >= 10000 || _track.XCoordinate <= 90000 && _track.YCoordinate >= 10000 ||
                _track.YCoordinate >= 90000)
            {
                if (_track.Altitude >= 500 && _track.Altitude <= 20000)
                {
                    Console.WriteLine("Tag: " + _track.Tag + " XCoordinate: " + _track.XCoordinate + " YCoordinate: " + _track.YCoordinate + " Altitude: " + _track.Altitude + " Timestamp: " + _track.Timestamp);
                    //Overvej at lave en display-klasse
                    return true;
                }
                return false;

            }
            return false;


        }
    }
}
