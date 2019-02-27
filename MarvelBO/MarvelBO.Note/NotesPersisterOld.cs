//using MarvelBO.ApiModel;
//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Text;

//namespace MarvelBO.Notes
//{
//    public class NotesPersisterOld : INotesPersister
//    {
//        public IEnumerable<Note> ListNotes()
//        {
//            return notes.Select(note => new Note() { Id = note.Key, Content = note.Value });
//        }

//        public Note Find(int creatorId)
//        {
//            return notes.Select(note => new Note() { Id = note.Key, Content = note.Value }).FirstOrDefault(note => note.Id == creatorId);
//        }
//        public void Add(Note note)
//        {
//            notes.Add(note.Id, note.Content);
//        }
//        public void Delete(int creatorId)
//        {
//            notes.Remove(creatorId);
//        }

//        static Dictionary<int, string> notes = new Dictionary<int, string>()
//        {
//            {
//                7968,
//                "fullNamemodifiedDate0001-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                6606,
//                "fullNameA.R.K.modifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                7969,
//                "fullNameACTORmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                1168,
//                "fullNameAll Thumbs CreativemodifiedDate2018-07-24T11:50:20numberOfSeries 14berOfComics 14 null"
//            },
//            {
//                7470,
//                "fullNameALSJOERDSMAmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                3965,
//                "fullNameAndreanimodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                4592,
//                "fullNameArnomodifiedDate2007-01-01T00:00:00numberOfSeries 2berOfComics 4 null"
//            },
//            {
//                3052,
//                "fullNameAvonmodifiedDate2007-01-02T00:00:00numberOfSeries 2berOfComics 2 null"
//            },
//            {
//                6535,
//                "fullNameB.K.modifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                12958,
//                "fullNameBalakmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                3188,
//                "fullNameBalcellsmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                12660,
//                "fullNameBattmodifiedDate0001-01-01T00:00:00numberOfSeries 4berOfComics 7 null"
//            },
//            {
//                6613,
//                "fullNameBeattymodifiedDate2007-01-02T00:00:00numberOfSeries 2berOfComics 2 null"
//            },
//            {
//                6919,
//                "fullNameBeckettmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                8098,
//                "fullNameBengalmodifiedDate2016-09-12T10:46:58numberOfSeries 8berOfComics 23 null"
//            },
//            {
//                9716,
//                "fullNameBenjaminmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 2 null"
//            },
//            {
//                3178,
//                "fullNameBernetmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                12701,
//                "fullNameBITmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                3095,
//                "fullNameBlankmodifiedDate2007-01-01T00:00:00numberOfSeries 2berOfComics 2 null"
//            },
//            {
//                7646,
//                "fullNameBlondmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 8 null"
//            },
//            {
//                8114,
//                "fullNameBlurmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                7596,
//                "fullNameBrownmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                13134,
//                "fullNameBruno Oliveira, Salvador EspinmodifiedDate0001-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                3634,
//                "fullNameBuzzmodifiedDate2007-01-02T00:00:00numberOfSeries 3berOfComics 3 null"
//            },
//            {
//                5979,
//                "fullNameCaesarmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                5138,
//                "fullNameCafumodifiedDate2007-01-01T00:00:00numberOfSeries 6berOfComics 6 null"
//            },
//            {
//                6642,
//                "fullNameCameronmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                13123,
//                "fullNameChad Bowers & Chris SimsmodifiedDate2017-04-14T00:37:26numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                12962,
//                "fullNameCharlamagne tha GodmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                1870,
//                "fullNameChiangmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                5977,
//                "fullNameChrisCrossmodifiedDate0001-01-01T00:00:00numberOfSeries 7berOfComics 41 null"
//            },
//            {
//                6319,
//                "fullNameCo.modifiedDate2007-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                4748,
//                "fullNameColorgraphixmodifiedDate2007-01-02T00:00:00numberOfSeries 3berOfComics 3 null"
//            },
//            {
//                807,
//                "fullNameComicraftmodifiedDate0001-01-01T00:00:00numberOfSeries 128berOfComics 504 null"
//            },
//            {
//                2585,
//                "fullNameCondoymodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 3 null"
//            },
//            {
//                3488,
//                "fullNameCoscriptermodifiedDate2007-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                10095,
//                "fullNameCRISSEmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                5898,
//                "fullNameDerekmodifiedDate2007-01-02T00:00:00numberOfSeries 2berOfComics 2 null"
//            },
//            {
//                4553,
//                "fullNameDickeymodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                6827,
//                "fullNameDiverse HandsmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                6408,
//                "fullNameDr. MartinmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                5789,
//                "fullNameDrewmodifiedDate0001-01-01T00:00:00numberOfSeries 2berOfComics 3 null"
//            },
//            {
//                12598,
//                "fullNameDubmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                13204,
//                "fullNameDustin Bates, Peter DavidmodifiedDate2017-09-08T11:27:17numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                8424,
//                "fullNameDynamitemodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                6097,
//                "fullNameEditormodifiedDate2007-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                6923,
//                "fullNameeksmodifiedDate2007-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                4077,
//                "fullNameEnigmamodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                6940,
//                "fullNameEptinesmodifiedDate2007-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                12864,
//                "fullNameFlavianomodifiedDate0001-01-01T00:00:00numberOfSeries 3berOfComics 3 null"
//            },
//            {
//                4030,
//                "fullNameFlipmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                3043,
//                "fullNameFrymodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                6564,
//                "fullNameFullermodifiedDate2007-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                5314,
//                "fullNameFunniesmodifiedDate2007-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                1676,
//                "fullNameGantzmodifiedDate2007-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                5962,
//                "fullNameGCWmodifiedDate2018-07-25T11:11:06numberOfSeries 3berOfComics 4 null"
//            },
//            {
//                12830,
//                "fullNameGeoffomodifiedDate2016-04-01T11:55:30numberOfSeries 5berOfComics 10 null"
//            },
//            {
//                8571,
//                "fullNameGuru-eFXmodifiedDate0001-01-01T00:00:00numberOfSeries 94berOfComics 287 null"
//            },
//            {
//                13210,
//                "fullNameGuru-eFX, Lee DuhigmodifiedDate2017-09-21T10:27:27numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                5721,
//                "fullNameGUSTAVOmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                9622,
//                "fullNameHau and YanmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                4783,
//                "fullNameHbmodifiedDate2007-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                7752,
//                "fullNameHenkelmodifiedDate2007-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                10096,
//                "fullNameHicksmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                6147,
//                "fullNameHiFi ColourmodifiedDate0001-01-01T00:00:00numberOfSeries 16berOfComics 21 null"
//            },
//            {
//                5731,
//                "fullNameHomermodifiedDate0001-01-01T00:00:00numberOfSeries 2berOfComics 5 null"
//            },
//            {
//                1076,
//                "fullNameHomsmodifiedDate2007-01-02T00:00:00numberOfSeries 3berOfComics 3 null"
//            },
//            {
//                12640,
//                "fullNameHoonmodifiedDate0001-01-01T00:00:00numberOfSeries 2berOfComics 3 null"
//            },
//            {
//                1496,
//                "fullNameIllomodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                7204,
//                "fullNameImpactomodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 10 null"
//            },
//            {
//                8625,
//                "fullNameIn-HousemodifiedDate0001-01-01T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                7910,
//                "fullNameINLIGHTmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                7927,
//                "fullNameIroBotmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 6 null"
//            },
//            {
//                4939,
//                "fullNameJ.c.modifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                2951,
//                "fullNameJaaskamodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                4736,
//                "fullNameJcmodifiedDate2007-01-02T00:00:00numberOfSeries 9berOfComics 16 null"
//            },
//            {
//                8724,
//                "fullNameJefferymodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                4472,
//                "fullNameJgmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                4196,
//                "fullNameJlmodifiedDate2007-01-02T00:00:00numberOfSeries 2berOfComics 3 null"
//            },
//            {
//                8764,
//                "fullNameJockmodifiedDate2013-01-12T09:36:33numberOfSeries 22berOfComics 51 null"
//            },
//            {
//                6915,
//                "fullNameJohnsonsmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 2 null"
//            },
//            {
//                5388,
//                "fullNameJrmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                1406,
//                "fullNameKanomodifiedDate2007-01-01T00:00:00numberOfSeries 23berOfComics 38 null"
//            },
//            {
//                2522,
//                "fullNameKfmodifiedDate2007-01-01T00:00:00numberOfSeries 2berOfComics 2 null"
//            },
//            {
//                10732,
//                "fullNameKNIGHT AGENCY, INC.modifiedDate0001-01-01T00:00:00numberOfSeries 4berOfComics 5 null"
//            },
//            {
//                6922,
//                "fullNameKylLsmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 2 null"
//            },
//            {
//                6051,
//                "fullNameLazmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                2663,
//                "fullNameLazarellimodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 3 null"
//            },
//            {
//                5353,
//                "fullNameLazarusmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                5744,
//                "fullNameLivmodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                3187,
//                "fullNameLombardiamodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                2258,
//                "fullNameLopezmodifiedDate2007-01-02T00:00:00numberOfSeries 2berOfComics 2 null"
//            },
//            {
//                5172,
//                "fullNameLowmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 0 null"
//            },
//            {
//                9015,
//                "fullNameMacMarcvemodifiedDate2007-01-02T00:00:00numberOfSeries 0berOfComics 0 null"
//            },
//            {
//                5953,
//                "fullNameMalibumodifiedDate0001-01-01T00:00:00numberOfSeries 4berOfComics 8 null"
//            },
//            {
//                2330,
//                "fullNameMalibumodifiedDate2018-07-23T11:37:56numberOfSeries 13berOfComics 41 null"
//            },
//            {
//                7838,
//                "fullNameMasmodifiedDate2007-01-02T00:00:00numberOfSeries 1berOfComics 2 null"
//            },
//            {
//                4139,
//                "fullNameMatthew PainemodifiedDate2007-01-02T00:00:00numberOfSeries 3berOfComics 7 null"
//            },
//            {
//                4687,
//                "fullNameMcGraymodifiedDate2007-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            },
//            {
//                12957,
//                "fullNameMethod ManmodifiedDate0001-01-01T00:00:00numberOfSeries 1berOfComics 1 null"
//            }
//        };
//    }
//}
