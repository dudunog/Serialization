using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WriteAndRead
{
    [Serializable]
    public class MovieStore
    {
        public List<Director> Directors = new List<Director>();
        public List<Movie> Movies = new List<Movie>();
    }

    [Serializable]
    public class Director
    {
        public string Name { get; set; }
        [XmlIgnore] //Ignora a propriedade abaixo no arquivo XML gerado
        public int NumberOfMovies;
    }

    [Serializable]
    public class Movie
    {
        public Director Director { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
    }
}
