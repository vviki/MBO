using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MarvelBO.ApiModel;
using Mono.Options;
using Newtonsoft.Json;
using RestSharp;


namespace MarvelBO.ApiClientConsole
{
    class ApiClient
    {
        static RestClient client;
        public static void Main(string[] args)
        {
            bool showHelp = false;
            bool listCreators = false;
            bool listNotes = false;
            bool compareCreators = false;
            bool addNote = false;
            bool updateNote = false;
            bool deleteNote = false;
            int firstCreator = 0;
            int secondCreator = 0;
            string orderBy = null;
            FilterOption<int> id = null;
            string namePart = null;
            string notePart = null;
            FilterOption<int> numberOfSeries = null;
            FilterOption<int> numberOfComics = null;
            FilterOption<DateTime> modifiedDate = null;
            string host = "http://localhost:50296/";
            string noteText = null;
            int noteId = 0;

            var p = new OptionSet() {
                "Usage: ",
                "Example:  -L -c=5 -i=\">4000\" -o=-note,comics,name,-date,-id -N" +
                " -C -a=8764 -b=8571 -A -d=11122 -x=\"New Note!!!\" -U -D",
                "Options:",
                { "h|Help",  "Show this message and exit.",
                    h => showHelp = h != null},
                { "H|Host=", "Set remote host address. Example: \n" +
                    " -H=\"http://ec2-52-15-204-100.us-east-2.compute.amazonaws.com\"",
                    H => { if(H != null) host = H; } },
                { "L|List", "List Creators.",
                    L => listCreators = L != null },
                { "N|Notes", "List Notes.",
                    N => listNotes = N != null },
                { "o|order=", "Order by string, possible any coma " +
                    "separated combination of values and '-' for descending. \n" +
                    "Values for Creators: id, name, date, comics, series, note. \n" +
                    "Values for Notes: id, name, note. \n" +
                    "E.g.: -o=-note,comics,name,-date,-id",
                    o => orderBy = o },
                { "i|id=", "Filter by id.",
                    i => {id = SetFilter(i, int.Parse); } },
                { "n|name=", "Filter by name.",
                    n => namePart = n },
                { "t|note=", "Filter by note.",
                    t => notePart = t },
                { "s|series=", "Filter by series.",
                    s => {numberOfSeries = SetFilter(s, int.Parse); } },
                { "c|comics=", "Filter by comics.",
                    c => {numberOfComics = SetFilter(c, int.Parse); } },
                { "m|modified=", "Filter by modified date, e.g.: -m=\">2007-01-02\"",
                    m => {modifiedDate = SetFilter(m, DateTime.Parse); } },
                { "C|Compare", "Compare Creators.",
                    C => compareCreators = C != null },
                { "a|first=", "First Creator Id.",
                    (int a) => firstCreator = a },
                { "b|second=", "Second Creator Id.",
                    (int b) => secondCreator = b },
                { "A|Add", "Add Note.",
                    A => addNote = A != null },
                { "U|Update", "Update Note.",
                    U => updateNote = U != null },
                { "D|Delete", "Delete Note.",
                    D => deleteNote = D != null },
                { "d|iD=", "Id for notes operations.",
                    (int d) => noteId = d },
                { "x|text=", "Text for notes operations.",
                    x => noteText = x },

            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--Help' for more information.");
                return;
            }
            if (showHelp)
            {
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            client = new RestClient
            {
                BaseUrl = new Uri(host),
                Timeout = 30000,
            };

            if (listCreators)
            {
                var request = new CreatorsRequest()
                {
                    Id = id,
                    ModifiedDate = modifiedDate,
                    NamePart = namePart,
                    NotePart = notePart,
                    NumberOfComics = numberOfComics,
                    NumberOfSeries = numberOfSeries,
                    OrderBy = ParseOrder(orderBy),
                };
                ExecuteRequest<List<Creator>>(
                    "api/Creators", Method.POST, request, true);
            }

            if (listNotes)
            {
                var request = new NotesRequest()
                {
                    Id = id,
                    NamePart = namePart,
                    ContentPart = notePart,
                    OrderBy = ParseOrder(orderBy),
                };
                ExecuteRequest<List<Note>>(
                    "api/Notes", Method.POST, request, true);
            }

            if (compareCreators)
            {
                var request = new CreatorsComparisonRequest()
                {
                    FirstId = firstCreator,
                    SecondId = secondCreator,
                };
                ExecuteRequest<CreatorsComparison>(
                    "api/CreatorsComparison", Method.POST, request, true);
            }

            if (addNote)
            {
                var request = new NoteOperationRequest()
                {
                    Id = noteId,
                    Content = noteText,
                };
                ExecuteRequest<NoteOperationResponse>(
                    "api/NoteOperations", Method.POST, request, false);
            }

            if (updateNote)
            {
                var request = new NoteOperationRequest()
                {
                    Id = noteId,
                    Content = noteText,
                };
                ExecuteRequest<NoteOperationResponse>(
                    "api/NoteOperations", Method.PUT, request, false);
            }

            if (deleteNote)
            {
                var request = new NoteOperationRequest()
                {
                    Id = noteId,
                };
                ExecuteRequest<NoteOperationResponse>(
                    "api/NoteOperations", Method.DELETE, request, false);
            }
        }

        static void ExecuteRequest<TResponse>(
            string resource, Method method, object request, bool isMarvel)
            where TResponse : new()
        {
            var restRequest = new RestRequest(resource, method);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(request));
            var response = client.Execute<TResponse>(restRequest);
            var responseCollection = response.Data as IEnumerable;
            if (responseCollection != null)
            {
                foreach (var item in responseCollection)
                    Console.Out.WriteLine(item.ToString());
            }
            else
            {
                if(response.Data == null)
                {
                    Console.Out.WriteLine("Some error with getting data: \n" + 
                        response.Content);
                    return;
                }
                Console.Out.WriteLine(response.Data.ToString());
            }
            if (isMarvel)
            {
                Console.Out.WriteLine("Data provided by Marvel. © 2014 Marvel\n");
            }
        }
        static List<OrderOption> ParseOrder(string orderBy)
        {
            DataField dataField;

            var order = new List<OrderOption>();

            if (orderBy == null)
                return order;

            foreach (var fieldName in orderBy.Split(','))
            {
                if (fieldName.StartsWith('-')
                    && Enum.TryParse(fieldName.Substring(1), out dataField))
                {
                    order.Add(new OrderOption()
                    {
                        Field = dataField,
                        Descending = true,
                    });
                }
                else if (Enum.TryParse(fieldName, out dataField))
                {
                    order.Add(new OrderOption()
                    {
                        Field = dataField,
                        Descending = false,
                    });
                }
            }
            return order;
        }

        static FilterOption<FIELD_TYPE> SetFilter<FIELD_TYPE>(
            string filter, 
            Func<string, FIELD_TYPE> filterParser
            )
            where FIELD_TYPE : struct, IComparable
        {
            if (filter == null)
                return null;
            FilterOption<FIELD_TYPE> filterOption = new FilterOption<FIELD_TYPE>();

            if (filter.StartsWith('>'))
            {
                filterOption.Type = FilterType.greater;
                filterOption.Value = filterParser(filter.Substring(1));
            }
            else if(filter.StartsWith('<'))
            {
                filterOption.Type = FilterType.lesser;
                filterOption.Value = filterParser(filter.Substring(1));
            }
            else
            {
                filterOption.Type = FilterType.equal;
                filterOption.Value = filterParser(filter);
            }
            return filterOption;
        }
    }
}