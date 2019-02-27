using MarvelBO.Api.Controllers;
using MarvelBO.ApiModel;
using MarvelBO.Creators;
using MarvelBO.Notes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MarvelBO.ApiTests
{
    public class CreatorsControllerTests
    {
        [Fact]
        public void NotesAssignedCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new CreatorsRequest());

            Assert.Equal("Note for Adam.", response.First(creator => creator.Id == 1).Note);

            Assert.Equal("Note for second Alan.", response.First(creator => creator.Id == 3).Note);

            Assert.Null(response.First(creator => creator.Id == 2).Note);
        }

        [Fact]
        public void CreatorsOrderedCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new CreatorsRequest()
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

            Assert.Equal("Alan", response.First().FullName);
            Assert.Equal(3, response.First().Id);
            Assert.Equal("Adam", response.Last().FullName);
        }

        [Fact]
        public void CreatorsFilteredByNameCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new CreatorsRequest()
            {
                NamePart = "la",
            });

            Assert.True(response.All( creator => creator.FullName == "Alan"));
        }

        [Fact]
        public void CreatorsFilteredByNoteCorrectly()
        {
            var controller = prepareController();

            var response = controller.Post(new CreatorsRequest()
            {
                NotePart = "second",
            });

            Assert.Equal("Alan", response.First().FullName);
            Assert.Single(response);
        }

        CreatorsController prepareController()
        {
            var notesManager = new Mock<INotesManager>();

            var forAdam = new Note()
            {
                Id = 1,
                Content = "Note for Adam."
            };

            notesManager.Setup(m => m.TryGetNote(1, out forAdam)).Returns(true);

            Note noNote = null;

            notesManager.Setup(m => m.TryGetNote(2, out noNote)).Returns(false);

            var forAlan = new Note()
            {
                Id = 3,
                Content = "Note for second Alan."
            };

            notesManager.Setup(m => m.TryGetNote(3, out forAlan)).Returns(true);

            var creatorsManager = new Mock<ICreatorsManager>();
            creatorsManager.Setup(m => m.ListCreators()).Returns(
                new List<Creator>()
                {
                  new Creator()
                  {
                      Id = 1,
                      FullName = "Adam"
                  },
                  new Creator()
                  {
                      Id = 2,
                      FullName = "Alan"
                  },
                  new Creator()
                  {
                      Id = 3,
                      FullName = "Alan"
                  },
                });

            return new CreatorsController(creatorsManager.Object, notesManager.Object);

        }
    }
}
