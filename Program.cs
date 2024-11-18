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
                WriteLine("1. Lägg till en skådespelare");
                WriteLine("2. Lista skådespelare");
                WriteLine("3. Lägg till en filmbolag");
                WriteLine("4. Lägg till en film");
                WriteLine("5. Exit");
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
                        AddFilm();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        shouldNotExit = false;
                        break;
                }
                Clear();
            }
        }
        private static void AddFilm()
        {
            bool isCorrect = false;
            do
            {
                Write("Titel: ");
                string Titel = ReadLine();
                Write("Beskrivning: ");
                string Beskrivning = ReadLine();
                Write("År: ");
                string År = ReadLine();
                Write("Genre: ");
                string Genre = ReadLine();
                Write("Regissör: ");
                string Regissör = ReadLine();
                Write("Filmbolag: ");
                string name = ReadLine();
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
                    if (context.Film.Any(x => x.Titel == Titel))
                    {
                        Clear();
                        WriteLine("Film finns redan");
                        Thread.Sleep(2000); // 2 sec 
                    }
                    else if (!context.FilmCompany.Any(x => x.Name == name))
                    {
                        Clear();
                        WriteLine("Ogiltigt filmbolag");
                        Thread.Sleep(2000); // 2 sec
                    }
                    else
                    {
                        Clear();
                        Film film = new Film(Titel, Beskrivning, År, Genre, Regissör);
                        //FilmCompany filmCompany = new FilmCompany(Filmbolag);
                        FilmCompany filmCompany = context.FilmCompany.FirstOrDefault(x => x.Name == name);

                        filmCompany.Movies.Add(film);
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
            Write("Namn: ");
            string name = ReadLine();
            //FilmCompany filmCompany = context.FilmCompany
            //    .FirstOrDefault(FilmCompany => FilmCompany.Name == name);
            Clear();
            if (context.FilmCompany.Any(x => x.Name == name))
            {
                WriteLine("Filmbolag finns redan");
            }
            else
            {
                FilmCompany filmCompany = new FilmCompany(name);
                context.FilmCompany.Add(filmCompany);
                context.SaveChanges();
                WriteLine("Filmbolag tillagd");
            }
            Thread.Sleep(2000);
        }
        private static void DisplayActor()
        {
            {
                List<Actor> actorList = context.Actor
                    .Include(Actor => Actor.Address)
                    .ToList();

                Write("Namn".PadRight(20, ' '));
                WriteLine("Adress");
                WriteLine("-----------------------------------------------");

                foreach (Actor actor in actorList)
                {
                    Write($"{actor.firstName}, {actor.lastName}".PadRight(20, ' '));

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
                        if (context.Actor.Any(x => x.socialSecurityNumber == socialSecurityNumber))
                        {
                            WriteLine("Skådespelare finns redan");
                        }
                        else
                        {
                            Address address = new Address(street, city, postcode);
                            Actor actor = new Actor(firstName, lastName, socialSecurityNumber, address);
                            context.Actor.Add(actor);
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