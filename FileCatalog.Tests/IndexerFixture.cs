using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Kayone.TestFoundation;
using NUnit.Framework;
using PetaPoco;
using Moq;

namespace FileCatalog.Tests
{
    [TestFixture]
    public class IndexerFixture : TestCore<Indexer>
    {

        private List<CatalogedFile> files;


        public void Given5Files()
        {
            files = Builder<CatalogedFile>.CreateListOfSize(5)
                .All().With(c => c.FileName = "test.txt")
                .Build().ToList();
        }

        [Test]
        public void should_add_same_number_of_files_to_db()
        {
            Given5Files();
            Subject.IndexTextFiles(files.ToList());


            Mocker.GetMock<IDatabase>().Verify(db => db.Insert(It.IsAny<CatalogedFile>()), Times.Exactly(5));


            ExceptionVerification.ExpectedWarns(5);
        }

        [Test]
        public void should_only_index_txt_files()
        {
            var testFiles = Builder<CatalogedFile>
                .CreateListOfSize(10)
                .Random(5)
                .With(c => c.FileName = "sometext.txt")
                .Build();

            Subject.IndexTextFiles(testFiles.ToList());


            Mocker.GetMock<IDatabase>().Verify(db => db.Insert(It.Is<CatalogedFile>(file => !file.FileName.EndsWith(".txt"))),
                Times.Never());
        }

    }
}
