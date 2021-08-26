using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace WriteAndRead
{
    class Program
    {
        static void Main(string[] args)
        {
            //Serialization
            var data = GetData();

            var dataWrite = new DataWrite();
            DataWrite.Write(data);

            //Desserialization

            Deserialize.XmlDeserialize();
            Deserialize.JsonDeserialize();
            Deserialize.BinaryDeserialize();

            Console.ReadKey();
        }

        public class DataWrite
        {
            public static Action<MovieStore> RecordData;
            public DataWrite()
            {
                RecordData += ConsoleWrite.XmlWrite;
                RecordData += ConsoleWrite.JsonWrite;
                RecordData += FileWrite.XmlWrite;
                RecordData += FileWrite.JsonWrite;
                RecordData += FileWrite.BinaryWrite;
            }

            public static void Write(MovieStore data)
            {
                RecordData?.Invoke(data);
            }
        }

        public static class ConsoleWrite
        {
            public static void XmlWrite(MovieStore data)
            {
                var xmlSerializer = new XmlSerializer(typeof(MovieStore));
                using (var stringWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(stringWriter, data);

                    System.Console.WriteLine();
                    System.Console.WriteLine("Data serialized in XML ==============================================================================");
                    System.Console.WriteLine();
                    Console.WriteLine(stringWriter);
                    System.Console.WriteLine();
                }
            }

            public static void JsonWrite(MovieStore data)
            {
                string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

                System.Console.WriteLine("Data serialized in JSON ==============================================================================");
                System.Console.WriteLine();
                Console.WriteLine(jsonString);
                System.Console.WriteLine();
            }
        }
        public static class FileWrite
        {
            public static void XmlWrite(MovieStore data)
            {
                var xmlSerializer = new XmlSerializer(typeof(MovieStore));
                using (var fileStream = new FileStream("Store.xml", FileMode.Create, FileAccess.Write))
                {
                    xmlSerializer.Serialize(fileStream, data);
                }
            }

            public static void JsonWrite(MovieStore data)
            {
                using (var streamWriter = new StreamWriter("Store.json"))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(data));
                }
            }
            
            public static void BinaryWrite(MovieStore data)
            {
                var binaryFormatter = new BinaryFormatter();

                using (var fileStream = new FileStream("Store.bin", FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(fileStream, data);
                }
            }
        }

        public static class Deserialize
        {
            public static void XmlDeserialize()
            {
                var xmlSerializer = new XmlSerializer(typeof(MovieStore));
                MovieStore result;

                using (var fileStream = new FileStream("Store.xml", FileMode.Open, FileAccess.Read))
                {
                    result = (MovieStore)xmlSerializer.Deserialize(fileStream);
                }

                System.Console.WriteLine();
                System.Console.WriteLine("Data deserialized from XML ==============================================================================");
                System.Console.WriteLine();

                foreach (var movie in result.Movies)
                {
                    System.Console.WriteLine(movie.Title);
                }
            }

            public static void JsonDeserialize()
            {
                MovieStore result;

                using (var streamReader = new StreamReader("Store.json"))
                {
                    var jsonString = streamReader.ReadLine();
                    result = JsonConvert.DeserializeObject<MovieStore>(jsonString);
                }

                System.Console.WriteLine();
                System.Console.WriteLine("Data deserialized from JSON ==============================================================================");
                System.Console.WriteLine();

                foreach (var movie in result.Movies)
                {
                    System.Console.WriteLine(movie.Title);
                }
            }

            public static void BinaryDeserialize()
            {
                var binaryFormatter = new BinaryFormatter();
                MovieStore result;

                using (var fileStream = new FileStream("Store.bin", FileMode.Open, FileAccess.Read))
                {
                    result = (MovieStore)binaryFormatter.Deserialize(fileStream);
                }

                System.Console.WriteLine();
                System.Console.WriteLine("Data deserialized from Binary ==============================================================================");
                System.Console.WriteLine();

                foreach (var movie in result.Movies)
                {
                    System.Console.WriteLine(movie.Title);
                }
            }
        }

        private static MovieStore GetData()
        {
            return new MovieStore
            {
                Directors = new List<Director>
                {
                    new Director { Name = "James Cameron", NumberOfMovies = 5 },
                    new Director { Name = "Francis Lawrence", NumberOfMovies = 3 },
                    new Director { Name = "Zack Snyder", NumberOfMovies = 6 },
                    new Director { Name = "Joss Whedon", NumberOfMovies = 7 },
                    new Director { Name = "Martin Scorsese", NumberOfMovies = 4 },
                    new Director { Name = "Christopher Nolan", NumberOfMovies = 8 },
                    new Director { Name = "Scott Derrickson", NumberOfMovies = 4 },
                    new Director { Name = "Gareth Edwards", NumberOfMovies = 3 },
                    new Director { Name = "Justin Kurzel", NumberOfMovies = 6 }
                },
                Movies = new List<Movie> {
                    new Movie {
                        Director = new Director { Name = "James Cameron", NumberOfMovies = 5 },
                        Title = "Avatar",
                        Year = "2009"
                    },
                    new Movie {
                        Director = new Director { Name = "Francis Lawrence", NumberOfMovies = 3 },
                        Title = "I Am Legend",
                        Year = "2007"
                    },
                    new Movie {
                        Director = new Director { Name = "Zack Snyder", NumberOfMovies = 6 },
                        Title = "300",
                        Year = "2006"
                    },
                    new Movie {
                        Director = new Director { Name = "Joss Whedon", NumberOfMovies = 7 },
                        Title = "The Avengers",
                        Year = "2012"
                    },
                    new Movie {
                        Director = new Director { Name = "Martin Scorsese", NumberOfMovies = 4 },
                        Title = "The Wolf of Wall Street",
                        Year = "2013"
                    },
                    new Movie {
                        Director = new Director { Name = "Christopher Nolan", NumberOfMovies = 8 },
                        Title = "Interstellar",
                        Year = "2014"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Game of Thrones",
                        Year = "2011–"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Vikings",
                        Year = "2013–"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Gotham",
                        Year = "2014–"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Power",
                        Year = "2014–"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Narcos",
                        Year = "2015–"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Breaking Bad",
                        Year = "2008–2013"
                    },
                    new Movie {
                        Director = new Director { Name = "Scott Derrickson", NumberOfMovies = 4 },
                        Title = "Doctor Strange",
                        Year = "2016"
                    },
                    new Movie {
                        Director = new Director { Name = "Gareth Edwards", NumberOfMovies = 3 },
                        Title = "Rogue One: A Star Wars Story",
                        Year = "2016"
                    },
                    new Movie {
                        Director = new Director { Name = "Justin Kurzel", NumberOfMovies = 6 },
                        Title = "Assassin's Creed",
                        Year = "2016"
                    },
                    new Movie {
                        Director = new Director { Name = "N/A" },
                        Title = "Luke Cage",
                        Year = "2016–"
                    }
                }
            };
        }
    }
}
