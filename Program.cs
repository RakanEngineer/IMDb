using IMDb.Data;
using IMDb.Models;
using System;
using System.Threading;
using static System.Console;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;

namespace IMDb
{
    internal class Program
    {
        static IMDbContext context = new IMDbContext();
        static void Main(string[] args)
        {
            bool shouldNotExit = true;
            while (shouldNotExit)
            {
                WriteLine("1. Lägg till skådespelare");
                WriteLine("2. Lista skådespelare");
                WriteLine("3. Lägg till filmbolag");
                WriteLine("4. Lägg till film");
                WriteLine("5. Lägg till skådespelare till film");
                WriteLine("6. Lista filmer");
                
                WriteLine("7. Exit");
                ConsoleKeyInfo keyPressed = ReadKey(true);
                Clear();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        AddActor();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        DisplayActor();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        AddFilmCompany();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        AddMovie();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        AddActorToMovie();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        ListMovies();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        shouldNotExit = false;
                        break;
                }
                Clear();
            }
        }

        private static void ListMovies()
        {
            WriteLine("Lista filmer");
            var movies = context.Movies.Include(m => m.MovieActors).ThenInclude(ma => ma.Actor).ToList();
            foreach (var movie in movies)
            {
                Console.WriteLine($"\nTitle: {movie.Title}");
                Console.WriteLine($"Director: {movie.Director}");
                Console.WriteLine($"Year: {movie.ReleaseYear}");
                Console.WriteLine("Actors:");
                foreach (var actor in movie.MovieActors)
                {
                    Console.WriteLine($"  {actor.Actor.FirstName} {actor.Actor.LastName}");
                }
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Tryck Esc för att återgå till huvudmenyn.");
                while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
                //ConsoleKeyInfo keyPressed;
                //bool escapePressed = false;
                //do
                //{
                //    keyPressed = ReadKey(true);
                //    if (keyPressed.Key == ConsoleKey.Escape)
                //    {
                //        escapePressed = true;
                //    }
                //} while (!escapePressed);
            }
        }

        private static void AddActorToMovie()
        {
            Console.WriteLine("\nEnter Actor's Social Security Number:");
            var ssn = Console.ReadLine();
            Console.WriteLine("Enter Movie Title:");
            var title = Console.ReadLine();

            var actor = context.Actors.FirstOrDefault(a => a.SocialSecurityNumber == ssn);
            var movie = context.Movies.FirstOrDefault(m => m.Title == title);

            if (actor == null)
            {
                Console.WriteLine("Actor not found.");
                return;
            }

            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            //if (movie.MovieActors.Contains(actor))
            //{
            //    Console.WriteLine("Actor is already associated with this movie.");
            //    return;
            //}
            var movieActor = new MovieActor
            {
                ActorId = actor.Id,
                MovieId = movie.Id
            };
            movie.MovieActors.Add(movieActor);
            context.SaveChanges();
            Console.WriteLine("Actor added to movie.");
        }

        private static void AddMovie()
        {
            bool isCorrect = false;
            do
            {
                Write("Titel: ");
                string title = ReadLine();
                Write("Beskrivning: ");
                string description = ReadLine();
                Write("År: ");
                string releaseYear = ReadLine();
                Write("Genre: ");
                string Genre = ReadLine();
                Write("Regissör: ");
                string director = ReadLine();
                Write("Filmbolag: ");
                string filmCompanyName = ReadLine();
                WriteLine();
                WriteLine("Är detta korrekt? (J)a eller (N)ej");
                ConsoleKeyInfo keyPressed;
                bool isValidKey = false;
                do
                {
                    keyPressed = ReadKey(true);
                    isValidKey = keyPressed.Key == ConsoleKey.J ||
                                 keyPressed.Key == ConsoleKey.N;
                } while (!isValidKey);
                if (keyPressed.Key == ConsoleKey.J)
                {
                    isCorrect = true;
                    Clear();
                    var filmCompany = context.FilmCompanies.FirstOrDefault(fc => fc.Name == filmCompanyName);

                    if (context.Movies.Any(x => x.Title == title))
                    {
                        Clear();
                        WriteLine("Film finns redan");
                        Thread.Sleep(2000); // 2 sec 
                    }
                    else if (!context.FilmCompanies.Any(x => x.Name == filmCompanyName))
                    {
                        Clear();
                        WriteLine("Ogiltigt filmbolag");
                        Thread.Sleep(2000); // 2 sec
                    }
                    else
                    {
                        Clear();
                        Movie movie = new Movie(title, description, releaseYear, Genre, director, filmCompany.Id);
                        //FilmCompany filmCompany = new FilmCompany(Filmbolag);
                        //FilmCompany filmCompany = context.FilmCompanies.FirstOrDefault(x => x.Name == Title);

                        context.Movies.Add(movie);
                        context.SaveChanges();
                        WriteLine("Film tillagd");
                        Thread.Sleep(2000); // 2 sec
                    }
                }
                Clear();
            } while (!isCorrect);
        }
        private static void AddFilmCompany()
        {
            Write("FilmCompanyNamn: ");
            string name = ReadLine();
            //FilmCompany filmCompany = context.FilmCompany
            //    .FirstOrDefault(FilmCompany => FilmCompany.Name == name);
            Clear();
            if (context.FilmCompanies.Any(x => x.Name == name))
            {
                WriteLine("Filmbolag finns redan");
            }
            else
            {
                FilmCompany filmCompany = new FilmCompany(name);
                context.FilmCompanies.Add(filmCompany);
                context.SaveChanges();
                WriteLine("Filmbolag tillagd");
            }
            Thread.Sleep(2000);
        }
        private static void DisplayActor()
        {
            {
                List<Actor> actorList = context.Actors
                    .Include(Actor => Actor.Address)
                    .ToList();

                Write("Namn".PadRight(20, ' '));
                WriteLine("Adress");
                WriteLine("-----------------------------------------------");

                foreach (Actor actor in actorList)
                {
                    Write($"{actor.FirstName}, {actor.LastName}".PadRight(20, ' '));

                    Address address = actor.Address;

                    WriteLine($"{address.street}, {address.postcode} {address.city}");
                }

                ConsoleKeyInfo keyPressed;

                bool escapePressed = false;

                do
                {
                    keyPressed = ReadKey(true);

                    if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        escapePressed = true;
                    }

                } while (!escapePressed);
            }
        }

        private static void AddActor()
        {
            {
                bool isCorrect = false;
                do
                {
                    Write("Förnamn: ");
                    string firstName = ReadLine();
                    Write("Efternamn: ");
                    string lastName = ReadLine();
                    Write("Personnummer: ");
                    string socialSecurityNumber = ReadLine();
                    Write("Gata: ");
                    string street = ReadLine();
                    Write("Stad: ");
                    string city = ReadLine();
                    Write("Postnummer: ");
                    string postcode = ReadLine();
                    WriteLine();
                    WriteLine("Är detta korrekt? (J)a eller (N)ej");
                    ConsoleKeyInfo keyPressed;
                    bool isValidKey = false;
                    do
                    {
                        keyPressed = ReadKey(true);
                        isValidKey = keyPressed.Key == ConsoleKey.J ||
                                     keyPressed.Key == ConsoleKey.N;
                    } while (!isValidKey);
                    if (keyPressed.Key == ConsoleKey.J)
                    {
                        isCorrect = true;
                        Clear();
                        if (context.Actors.Any(x => x.SocialSecurityNumber == socialSecurityNumber))
                        {
                            WriteLine("Skådespelare finns redan");
                        }
                        else
                        {
                            Address address = new Address(street, city, postcode);
                            Actor actor = new Actor(firstName, lastName, socialSecurityNumber, address);
                            context.Actors.Add(actor);
                            context.SaveChanges();
                            WriteLine("Skådespelare tillagd");
                        }
                        Thread.Sleep(2000); // 2 sec
                    }
                    Clear();
                } while (!isCorrect);
            }
        }
    }
}