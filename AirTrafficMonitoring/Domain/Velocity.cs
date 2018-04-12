﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Domain
{
    public class Velocity
    {
        private Track track1;
        private Track track2;

        public double DetermineVelocity()
        {
            var timediff = track2.Timestamp.Subtract(track1.Timestamp);
            var distance = Math.Sqrt(Math.Pow((track1.YCoordinate - track1.XCoordinate),2) +
                                     Math.Pow((track2.YCoordinate - track2.XCoordinate),2));
            var velocity = distance / Math.Abs(Convert.ToDouble(timediff));

            return velocity;

        }

    }
}