using System.Collections.Generic;

namespace ProviderQuality.Console
{
    public class Program
    {
        public IList<Award> Awards
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Updating award metrics...!");

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Gov Quality Plus", ExpiresIn = 10, Quality = 20},
                    new Award {Name = "Blue First", ExpiresIn = 2, Quality = 0},
                    new Award {Name = "ACME Partner Facility", ExpiresIn = 5, Quality = 7},
                    new Award {Name = "Blue Distinction Plus", ExpiresIn = 0, Quality = 80},
                    new Award {Name = "Blue Compare", ExpiresIn = 15, Quality = 20},
                    new Award {Name = "Top Connected Providres", ExpiresIn = 3, Quality = 6},
                    new Award {Name = "Blue Star", ExpiresIn = 3, Quality = 6}
                }

            };

            app.UpdateQuality();

            System.Console.Write("Award metrics have been updated. Press any key to continue.");
            System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            foreach (Award award in Awards)
            {
                switch (award.Name)
                {
                    case "Blue First":
                        award.DecreaseExpirationDate();
                        if (award.Quality < 50 && award.Quality > 0)
                        {
                            award.Quality++;
                        }
                        else if (award.Quality >= 50)
                        {
                            award.Quality = 50;
                        }
                        else
                        {
                            award.Quality = 0;
                            award.Quality++;
                        }
                        break;
                    //Instructions indicate that Blue Distinction Plus awards are unaffected by quality decay but 
                    //does not mention that Blue Distinction Plus awards do not expire. Legacy code indicates that expiration date is irrelevant 
                    //in regards to Blue Distinction Plus awards. Blue Distinction Plus awards will decrease their expiration date each day for consistency
                    // Jason Faulkner - 7/9/19
                    case "Blue Distinction Plus":
                        award.DecreaseExpirationDate();
                        break;
                    case "Blue Compare":
                        award.DecreaseExpirationDate();
                        if (award.ExpiresIn > 10)
                        {
                            award.ChangeQuality(1);
                        }
                        else if (award.ExpiresIn <= 10 && award.ExpiresIn > 5)
                        {
                            award.ChangeQuality(2);
                        }
                        else if (award.ExpiresIn < 5 && award.ExpiresIn > 0)
                        {
                            award.ChangeQuality(3);
                        }
                        else
                        {
                            award.Quality = 0;
                        }
                        break;
                    case "Blue Star":
                        award.DecreaseExpirationDate();
                        award.ChangeQuality(-2);
                        break;
                    default:
                        award.DecreaseExpirationDate();
                        award.ChangeQuality(-1);
                        break;

                }
            }
        }
    }

}
