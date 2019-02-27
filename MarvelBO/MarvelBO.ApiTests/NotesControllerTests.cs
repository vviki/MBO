using MarvelBO.Api.Controllers;
using System.Linq;
using Xunit;
using Moq;
using MarvelBO.Notes;
using System.Collections.Generic;
using MarvelBO.ApiModel;
using MarvelBO.Creators;

namespace MarvelBO.ApiTests
{
    public class NotesControllerTests
    {
        [Fact]
        public void NamesOfCreatorsAssignedCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new NotesRequest());

            Assert.Equal("Adam", response.First(creator => creator.Id == 1).CreatorName);

            Assert.Equal("Alan", response.First(creator => creator.Id == 2).CreatorName);
        }

        [Fact]
        public void NotesOrderedCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new NotesRequest()
            {
                OrderBy = new List<OrderOption> {
                    new OrderOption()
                    {
                        Field = DataField.name,
                        Descending = true,
                    },
                    new OrderOption()
                    {
                        Field = DataField.id,
                        Descending = true,
                    },
                } //"-name,-id",
            });

            Assert.Equal("Alan", response.First().CreatorName);
        }

        [Fact]
        public void NotesFilteredCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new NotesRequest()
            {
                NamePart = "la",
            });

            Assert.Equal("Alan", response.First().CreatorName);
            Assert.Single(response);
        }

        NotesController prepareController()
        {
            var notesManager = new Mock<INotesManager>();
            notesManager.Setup(m => m.ListNotes()).Returns(
                new List<Note>()
                {
                    new Note()
                    {
                        Id = 1,
                    },
                    new Note()
                    {
                        Id = 2,
                    },
                });

            var creatorsManager = new Mock<ICreatorsManager>();
            creatorsManager.Setup(m => m.GetCreator(1)).Returns(
                  new Creator()
                  {
                      Id = 1,
                      FullName = "Adam"
                  });
            creatorsManager.Setup(m => m.GetCreator(2)).Returns(
                  new Creator()
                  {
                      Id = 2,
                      FullName = "Alan"
                  });

            return new NotesController(creatorsManager.Object, notesManager.Object);

        }
    }
}
