using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using PetaPoco;

namespace FileCatalog
{
    public class Indexer
    {
        private readonly IDatabase _database;


        private Logger logger = LogManager.GetCurrentClassLogger();


        public Indexer(IDatabase database)
        {
            _database = database;
        }


        public void IndexTextFiles(IEnumerable<CatalogedFile> files)
        {
            foreach (var catalogedFile in files.Where(f => f.FileName.EndsWith(".txt")))
            {
                Console.WriteLine("Adding {0} to catalog.", catalogedFile.Path);

                _database.Insert(catalogedFile);

                logger.Error("Failed to add file");
            }
        }
    }
}