using GreenFlux.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.Service.Tools
{
    public class Suggester : ISuggester
    {

        public List<ISuggestion> GetSuggestions(List<Connector> connectors, float target)
        {
            List<ISuggestion> suggestions = new List<ISuggestion>();

            //keep only connectors with MaxCurrent less than target
            connectors = connectors.Where(c => c.MaxCurrent <= target).ToList(); //TODO : optimize by using splitArray by index

            //Order : biggest first
            connectors.Sort(new ConnectorMaxCurrentComparer());

            //Find SingleElement solutions
            suggestions = FindSingleElementSuggestions(connectors, target);
            if (suggestions.Count > 0)
                return suggestions;

            //If there is no singleElement solutions try to find MultipleElementSolutions
            suggestions = FindMultipleElementSuggestions(connectors, target);


            return suggestions;

        }


        private static List<ISuggestion> FindSingleElementSuggestions(List<Connector> connectors, float target)
        {
            var suggestions = new List<ISuggestion>();

            for (int i = 0; i < connectors.Count; i++)
            {
                Connector currentElement = connectors[i];

                if (currentElement.MaxCurrent == target)
                {
                    suggestions.Add(new SingleElementSuggestion(currentElement));

                    //if this is not the last element, Then check next element
                    //if next element isn't equal to target, exit from loop
                    if (i < connectors.Count-1 && connectors[i + 1].MaxCurrent != target)
                        return suggestions;
                }
            }

            return suggestions;
        }

        private static List<ISuggestion> FindMultipleElementSuggestions(List<Connector> connectors, float target)
        {
            //Fill Suggestions
            List<ISuggestion> suggestions = new List<ISuggestion>();

   
            int elementsToTake = 0;

        Research:

            int leftHandLeftPartIndex = 0;
            while (leftHandLeftPartIndex < connectors.Count) 
            {
                int leftHandRightPartIndex = leftHandLeftPartIndex + elementsToTake;
                while (leftHandRightPartIndex < connectors.Count - 1)
                {
                    //Build LeftHandElements
                    List<Connector> leftHandElemens = new List<Connector>();
                    leftHandElemens.AddRange(connectors.GetRange(leftHandLeftPartIndex, elementsToTake));
                    leftHandElemens.Add(connectors[leftHandRightPartIndex]);
                   
                    //Calculate sum of leftHandElements
                    float leftHandSum = leftHandElemens.Sum(c => c.MaxCurrent);

                    //Start from the element after LefHandRightPart till the last element
                    for (int i = leftHandRightPartIndex + 1; i < connectors.Count; i++)
                    {
                        var rightHand = connectors[i];

                        if (leftHandSum + rightHand.MaxCurrent == target)
                        {
                            //This is new solution
                            leftHandElemens.Add(rightHand);

                            //If previous solution better than this, then stop searching
                            if (suggestions.Count > 0)
                            {
                                //get previous solution
                                var previousSolution = ((MultipleElementSuggestion)suggestions[suggestions.Count - 1]);
                                if (previousSolution.Elements.Count < leftHandElemens.Count)
                                    goto StopSearching;
                            }
                      
                            //Add it to suggestions
                            suggestions.Add(new MultipleElementSuggestion { Elements = leftHandElemens });
                        }
                    }

                    leftHandRightPartIndex++;
                }

                leftHandLeftPartIndex++;

                if ( elementsToTake >= connectors.Count)
                    break;

                if (suggestions.Count == 0)
                {
                    elementsToTake++;
                    goto Research;
                }

            }


            StopSearching:

            return suggestions;
        }



    }
}
