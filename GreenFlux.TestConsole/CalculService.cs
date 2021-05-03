using GreenFlux.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenFlux.TestConsole
{
    public class ConnectorRemovalSuggestionService
    {
        private readonly IReadOnlyList<ConnectorRemovalSuggestion> _allConnectorSuggestions;
        private readonly Stack<ConnectorRemovalSuggestion> _usedConnectors;
        private readonly IList<IList<ConnectorRemovalSuggestion>> _suggestions;

        private ConnectorRemovalSuggestionService(IReadOnlyList<ConnectorRemovalSuggestion> allConnectorSuggestions)
        {
            _allConnectorSuggestions = allConnectorSuggestions;
            _usedConnectors = new Stack<ConnectorRemovalSuggestion>();
            _suggestions = new List<IList<ConnectorRemovalSuggestion>>();
        }

        public static IList<IList<ConnectorRemovalSuggestion>> SuggestConnectors(List<Connector> connectorsIn, float excessiveCurrent)
        {

            var connectors = connectorsIn
                .Select(sc => new ConnectorRemovalSuggestion(sc.ChargeStationId, sc.CK_Id, sc.MaxCurrent))
                .OrderByDescending(crs => crs.ConnectorCurrent)
                .ToList();

            var suggestionEngine = new ConnectorRemovalSuggestionService(connectors);

            suggestionEngine.BuildSuggestions(excessiveCurrent);

            var suggestionGroup = suggestionEngine._suggestions
                .GroupBy(s => s.Count)
                .OrderBy(g => g.Key)
                .FirstOrDefault();

            return suggestionGroup == null
                ? Array.Empty<IList<ConnectorRemovalSuggestion>>()
                : suggestionGroup.ToList() as IList<IList<ConnectorRemovalSuggestion>>;
        }

        private void BuildSuggestions(float current, int index = 0)
        {
            if (current == 0)
            {
                _suggestions.Add(new List<ConnectorRemovalSuggestion>(_usedConnectors));
                return;
            }

            if (index >= _allConnectorSuggestions.Count || HasSuggestionBetterThanCurrent())
            {
                return;
            }

            var connector = _allConnectorSuggestions[index];
            if (connector.ConnectorCurrent <= current)
            {
                _usedConnectors.Push(connector);
                BuildSuggestions(current - connector.ConnectorCurrent, index + 1);
                _usedConnectors.Pop();
            }

            BuildSuggestions(current, index + 1);
        }

        private bool HasSuggestionBetterThanCurrent()
        {
            return _suggestions.Count > 0 && _suggestions[0].Count < _usedConnectors.Count;
        }
    }



    public struct ConnectorRemovalSuggestion
    {
        public long StationId;
        public byte ConnectorId;
        public float ConnectorCurrent;

        public ConnectorRemovalSuggestion(long stationId, byte connectorId, float connectorCurrent)
        {
            StationId = stationId;
            ConnectorId = connectorId;
            ConnectorCurrent = connectorCurrent;
        }
    }


}
