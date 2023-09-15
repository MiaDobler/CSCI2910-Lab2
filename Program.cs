using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Runtime.CompilerServices;

namespace CSCI2910_Lab1
{

    //DO ERROR HANDLING

    internal class Program
    {
        static void Main(string[] args)
        {        
            List<VideoGame> videogames = new List<VideoGame>();

            string projectRootFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString();
            string filePath = projectRootFolder + "/videogames.csv";
            
            using (var sr = new StreamReader(filePath)) //from Will's code example
            {
                sr.ReadLine(); 

                while (!sr.EndOfStream)  
                {
                    string line = sr.ReadLine();

                    string[] lineData = line.Split(',');

                    //data is Name, Platform, Year, Genre, Publisher, NA_Sales, EU_Sales, JP_Sales, Other_Sales, Global_Sales order based on header row
                    videogames.Add(new VideoGame(lineData[0], lineData[1], Convert.ToInt16(lineData[2]), lineData[3], lineData[4], Convert.ToDouble(lineData[5]), Convert.ToDouble(lineData[6]), Convert.ToDouble(lineData[7]), Convert.ToDouble(lineData[8]), Convert.ToDouble(lineData[9])));
                }
            }
            videogames.Sort();



            // ----------------- METHODS ----------------- //

            //publisher data user thing
            Console.Write("\nPLease enter a publisher: ");
            string userPublisher = Console.ReadLine();

            PublisherData(userPublisher, videogames);

            void PublisherData(string userPublisher, List<VideoGame> games2)
            {                  
                    IEnumerable<string> publishers = games2.Select(x => x.Publisher);
                    List<string> publishers2 = publishers.Distinct().ToList();

                    IEnumerable<VideoGame> publisherCompared = new List<VideoGame>();
                    publisherCompared = games2.Where(game  => game.Publisher == userPublisher);

                    foreach (var item in publisherCompared)
                    {
                        Console.WriteLine(item);
                    }

                double publisherPercentageCalc = (Convert.ToDouble(publisherCompared.Count()) / Convert.ToDouble(games2.Count()) * 100);
                double publisherPercentage = Math.Round(publisherPercentageCalc, 2);

                Console.Write($"\nOut of {games2.Count()} games, {publisherCompared.Count()} are devloped by {userPublisher}, which is {publisherPercentage}%.\n");
            }



            //genre data user thing

            Console.Write("\nPLease enter a genre: ");
            string userGenre = Console.ReadLine();

            GenreData(userGenre, videogames);

            void GenreData(string userGenre, List<VideoGame> games3)
            {
                IEnumerable<string> genres = games3.Select(x => x.Genre);
                List<string> genres2 = genres.Distinct().ToList();

                IEnumerable<VideoGame> genreCompared = new List<VideoGame>();
                genreCompared = games3.Where(game => game.Genre == userGenre);

                foreach (var item in genreCompared)
                {
                    Console.WriteLine(item);
                }

                double genrePercentageCalc = (Convert.ToDouble(genreCompared.Count()) / Convert.ToDouble(games3.Count()) * 100);
                double genrePercentage = Math.Round(genrePercentageCalc, 2);

                Console.Write($"\nOut of {games3.Count()} games, {genreCompared.Count()} are {userGenre} games, which is {genrePercentage}%.\n");
            }

        }        

    }
}