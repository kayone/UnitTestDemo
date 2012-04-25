using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using PetaPoco;

namespace FileCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection =
                new SqlConnection(@"Data Source=.\SQLExpress;Database=FileCatalog; Integrated Security=True");


            var database = new Database(connection, Database.DBType.SqlServer);

            
            var sourceDirectory = new DirectoryInfo(@"C:\Dropbox\Presentations\FileCatalog\Notepad++");


            var files = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories)
                  .Select(f => new CatalogedFile { FileName = f.Name, Path = f.FullName, Size = f.Length });


            connection.Open();

            foreach (var catalogedFile in files)
            {
                Console.WriteLine("Adding {0} to catalog.", catalogedFile.Path);
                database.Insert(catalogedFile);
            }

            connection.Close();


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }

    [TableName("Files")]
    public class CatalogedFile
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }                                       
}
