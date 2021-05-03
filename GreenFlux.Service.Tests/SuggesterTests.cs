using GreenFlux.Model;
using GreenFlux.Service.Tools;
using System;
using System.Collections.Generic;
using Xunit;

namespace GreenFlux.Service.Tests
{
    public class SuggesterTests
    {

        public void GetSuggestions_Tests(float targetCurrent, int suggestionsExpected)
        {
            var connectorsList = new List<Connector>
            {
                new Connector() {MaxCurrent = 44, ChargeStationId = 1},
                new Connector() {MaxCurrent = 10, ChargeStationId = 1},
                new Connector() {MaxCurrent = 8.9f, ChargeStationId = 2},
                new Connector() {MaxCurrent = 1.1f, ChargeStationId = 2},
                new Connector() {MaxCurrent = 20, ChargeStationId = 3},
                new Connector() {MaxCurrent = 30, ChargeStationId = 3},
                new Connector() {MaxCurrent = 10, ChargeStationId = 4},
            };

            var suggester = new Suggester();
            var suggestions = suggester.GetSuggestions(connectorsList , targetCurrent);


            Assert.Equal(suggestionsExpected , suggestions.Count);

        }
    }
}
