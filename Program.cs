using static System.Console;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ContosoPets.models;
using System.Collections.Generic;
using System.Xml.Serialization;
using static System.Environment;
using static System.IO.Path;
using System;
using Newtonsoft.Json;
using ServiceStack.Text;
using CsvHelper;
using System.IO;
using System.Globalization;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Net.Sockets;


namespace ContosoPets
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryingCategories();
        }

        
        static void QueryingCategories()
        {
            using (var db = new NorthwindContext())
            {
                WriteLine("Categories and how many products they have:");
                // a query to get all categories and their related products 
                var cats = db.Categories.Include(c => c.Products)
                  .ToList();

                //This part is for XML Serialization

                
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(List<Category>));

                string path = Combine(CurrentDirectory, "category.xml");

                
                using (FileStream stream = File.Create(path))
                {
                    
                    xs.Serialize(stream, cats);
                }


                //This part is for JSON Serialization

                string fileName = "category.json";
                string jss = JsonConvert.SerializeObject(cats, Formatting.None, new JsonSerializerSettings()
                        {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                File.WriteAllText(fileName, jss);




                //WriteLine(File.ReadAllText(path));

                var prod = db.Products;
                var prodCsvPath = Path.Combine(CurrentDirectory, "product.csv");
                using (var streamWriter = new StreamWriter(prodCsvPath))
                {
                    using (var csvwriter = new CsvHelper.CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {


                        csvwriter.WriteRecords(prod);


                    }
                }


                var catCsvPath = Path.Combine(CurrentDirectory, "category.csv");
                using (var streamWriter = new StreamWriter(catCsvPath))
                {
                    using (var csvwriter = new CsvHelper.CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {

                        
                        csvwriter.WriteRecords(cats);


                    }
                }
                //WriteLine(File.ReadAllText(csvPath));


                WriteLine($"Rank 1: JSON format uses {new FileInfo(fileName).Length} bytes for serialization");
                WriteLine($"Rank 2: XML format uses {new FileInfo(path).Length} bytes for serialization");
                WriteLine($"Rank 3: CSV format uses {new FileInfo(catCsvPath).Length + new FileInfo(prodCsvPath).Length} bytes for serialization");

               
                

            }
        }
    }
}





//dotnet ef dbcontext Scaffold "Server=localhost,1433;Initial Catalog=Northwind;Persist Security Info=False;User ID=SA;Password=Dardha2@;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer -o models -c NorthwindContext -d