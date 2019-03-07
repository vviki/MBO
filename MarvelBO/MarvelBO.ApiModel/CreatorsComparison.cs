using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class CreatorsComparison
    {
        public int IdOfFirst { get; set; }

        public int IdOfSecond { get; set; }

        public string FullNameOfFirst { get; set; }

        public string FullNameOfSecond { get; set; }

        public DateTime ModifiedDateOfFirst { get; set; }

        public DateTime ModifiedDateOfSecond { get; set; }

        public string NoteOfFirst { get; set; }

        public string NoteOfSecond { get; set; }

        public int NumberOfCommonSeries { get; set; }

        public int NumberOfCommonComics { get; set; }

        public CreatorsComparisonStatus ComparisonStatus { get; set; }

        public override string ToString()
        {
            switch (ComparisonStatus)
            {
                case CreatorsComparisonStatus.FirstCreatorDoesNotExist:
                    return "First creator does not exist!";
                case CreatorsComparisonStatus.SecondCreatorDoesNotExist:
                    return "Second creator does not exist!";
                case CreatorsComparisonStatus.BothCreatorsDoNotExist:
                    return "Both creators do not exist!";
                default:
                    return String.Format(
                    "Id of first: {0},\n" +
                    "Id of second: {1},\n" +
                    "Full name of first: {2},\n" +
                    "Full name of second: {3},\n" +
                    "Modified date of first: {4},\n" +
                    "Modified date of second: {5},\n" +
                    "Note of first: {6},\n" +
                    "Note of second: {7},\n" +
                    "Number of common series: {8},\n" +
                    "Number of common comics: {9}.",
                    IdOfFirst,
                    IdOfSecond,
                    FullNameOfFirst,
                    FullNameOfSecond,
                    ModifiedDateOfFirst,
                    ModifiedDateOfSecond,
                    NoteOfFirst,
                    NoteOfSecond,
                    NumberOfCommonSeries,
                    NumberOfCommonComics);
            }
        }
    }
}
