﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Boundary;
using AirTrafficMonitoring.Controller;
using AirTrafficMonitoring.Domain;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace Unit.Test.ATM
{
    [TestFixture]
    public class SortingPlanesControllerTest
    {
        private List<ITrack> CurrentTracks;
        private IDisplay _display;
        private MonitoredPlanes monitoredPlanes;
        private List<ITrack> tracksToRemove;
        private IMonitoredPlanes seperationEvent;
        private ICalculator _calc;
        private ITrack newtrack;
        private ITrack oldtrack;
        private SortingPlanesController uut;

        [SetUp]
        public void SetUp()
        {
            newtrack = new Track() { Tag = "ABC123"};
            oldtrack = new Track() { Tag = "ABC123"};
            CurrentTracks = new List<ITrack>(){oldtrack};
            _display = Substitute.For<IDisplay>();
            monitoredPlanes = Substitute.For<MonitoredPlanes>();
            tracksToRemove = new List<ITrack>(){oldtrack};
            seperationEvent = Substitute.For<IMonitoredPlanes>();
            _calc = Substitute.For<ICalculator>();
            uut = new SortingPlanesController(CurrentTracks,_display,monitoredPlanes,seperationEvent,tracksToRemove, _calc);
        }

        [Test]
        public void MatchTracks_RemoveOldTrack()
        {
            uut.MatchTracks(newtrack);
            Assert.That(!CurrentTracks.Contains(oldtrack));
        }

        [Test]
        public void MatchTracks_VelocityCalc_NewTrack()
        {
            uut.MatchTracks(newtrack);
            _calc.Received().DetermineVelocity(newtrack,oldtrack);
            
        }

        [Test]
        public void MatchTracks_CompassCourseCalc_NewTrack()
        {
            uut.MatchTracks(newtrack);
            _calc.Received().Direction(newtrack,oldtrack);

        }
        [Test]
        public void MatchTracks_AddOldTrackToRemoveList_RemoveListIsCleared()
        {
            uut.MatchTracks(newtrack);
            Assert.IsEmpty(tracksToRemove);
        }

        [Test]
        public void MatchTracks_NewTrackIsShownInDisplay()
        {
            uut.MatchTracks(newtrack);
            _display.Received().ShowTrack(newtrack);
        }

        [Test]
        public void MatchTracks_MonitoredPlanesIsCalled_CurrentTracksList()
        {
            uut.MatchTracks(newtrack);
            monitoredPlanes.HandleSeperationEvents(CurrentTracks);
        }


        [Test]
        public void removeTrack_CurrentTracks_RemoveOldTrack()
        {
            uut.removeTrack(newtrack);
            Assert.That(!CurrentTracks.Contains(oldtrack));
        }






    }
}
