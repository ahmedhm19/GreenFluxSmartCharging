using GreenFlux.Model;
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

        private static List<ISuggestion> FindMultipleElementSuggestions(List<Connector> connectors, float originalTarget)
        {

            var target = new Target();
            target.SetValue(originalTarget, TargetSources.Calculated);


            List<Connector> solutionsLeftPart = new List<Connector>();
            List<List<Connector>> solutionsRightPart = new List<List<Connector>>();

            while (connectors.Count >= 1)
            {
                solutionsRightPart = GetPairs(connectors, target.Value);
                if (solutionsRightPart.Count > 0)
                    if (target.Source == TargetSources.Calculated)
                    {
                        break;
                    }
                    else
                    {
                        target.SetValue(Target.PreviousTarget.Value - target.Value, TargetSources.Calculated);
                    }
                else
                {
                    //Set new Target
                    var newTarget = connectors[0];
                    target.SetValue(newTarget.MaxCurrent, TargetSources.Connectors);
                    solutionsLeftPart.Add(newTarget);

                    //Remove connector used as target from connectors
                    connectors.RemoveAt(0);
                }
            }

            //Fill Suggestions
            List<ISuggestion> suggestions = new List<ISuggestion>();

            if (solutionsRightPart.Count == 0)
            {
                //If there is no RightPart solutions
                //then : sum of LeftPartSolutions must be equal to originalTarget

                if (solutionsLeftPart.Sum(e => e.MaxCurrent) == originalTarget)
                    suggestions.Add(new MultipleElementSuggestion { Elements = solutionsLeftPart });
            }
            else
            {
                foreach (var solutionRightPart in solutionsRightPart)
                {
                    var suggestion = new MultipleElementSuggestion { Elements = solutionRightPart };
                    suggestion.Elements.InsertRange(0, solutionsLeftPart);
                    suggestions.Add(suggestion);
                }
            }

            return suggestions;

        }






        private static List<List<Connector>> GetPairs(List<Connector> connectors, float target)
        {
            var pairs = new List<List<Connector>>();

            for (int i = 0; i < connectors.Count; i++)
            {
                var leftHand = connectors[i];

                var rightHands = GetRightHands(leftHand, target, connectors);

                if (rightHands.Count > 0)
                {
                    //Fill LeftHand with its rightHands
                    foreach (var rightHand in rightHands)
                    { pairs.Add(new List<Connector> { leftHand, rightHand }); }
                }
            }

            return pairs;
        }
        private static List<Connector> GetRightHands(Connector leftHand, float target, List<Connector> connectors)
        {
            //Note : Since connectors are ordered from biggest value to smallest,
            //so the lefthand is the biggest value

            List<Connector> outRightHands = new List<Connector>();

            float expectedRightHandValue = target - leftHand.MaxCurrent;

            //Start loop from the element after (leftHand) 
            for (int i = leftHand.Index + 1; i < connectors.Count; i++)
            {
                Connector rightHand = connectors[i];

                if (rightHand.MaxCurrent == expectedRightHandValue) 
                {
                    if (rightHand != leftHand)//skip self
                    {
                        rightHand.Index = i;//TODO to remove
                        outRightHands.Add(rightHand);
                    }
                }
                else if (rightHand.MaxCurrent <= expectedRightHandValue)
                {
                    break; // this to exit from loop when we find the last solution, so we don't have to keep searching in the rest of values
                }
            }

            return outRightHands;
        }




    }
}
