﻿using RestSharp;
using System;
using System.Collections.Generic;

namespace HTTP_Web_Services_GET_lecture
{
    class Program
    {
        static readonly string API_URL = "http://localhost:3000/";
        static readonly RestClient client = new RestClient();

        static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            Console.WriteLine("Welcome to Online Hotels! Please make a selection:");
            MenuSelection();
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Menu:");
                Console.WriteLine("1: List Hotels");
                Console.WriteLine("2: List Reviews");
                Console.WriteLine("3: Show Details for Hotel ID 1");
                Console.WriteLine("4: List Reviews for Hotel ID 1");
                Console.WriteLine("5: List Hotels with star rating 3");
                Console.WriteLine("6: Public API Query");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Only input a number.");
                }
                else if (menuSelection == 1)
                {
                    List<Hotel> hotels = GetHotels(); //GetHotels is going to call the API to get the list of hotels
                    PrintHotels(hotels);
                }
                else if (menuSelection == 2)
                {
                    PrintReviews(GetReviews());
                }
                else if (menuSelection == 3)
                {
                    Hotel hotel = GetHotelById(1);
                    PrintHotel(hotel);
                }
                else if (menuSelection == 4)
                {
                    List<Review> reviews = GetReviewsForHotelId(1);
                    PrintReviews(reviews);
                }
                else if (menuSelection == 5)
                {
                    List<Hotel> hotels = GetHotelsWithStarRating(3);
                    PrintHotels(hotels);
                }
                else if (menuSelection == 6)
                {
                    City city = GetCityFromPublicAPI();
                    PrintCity(city);
                }
                else if (menuSelection==7)
                {
                    string fact = GetRandomCatFact();
                    Console.WriteLine(fact);
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }

        private static string GetRandomCatFact()
        {
            //https://catfact.ninja/fact
            RestRequest request = new RestRequest("https://catfact.ninja/fact");
            IRestResponse<CatFact> response = client.Get<CatFact>(request);
            return response.Data.fact;
        }



        //API methods:
        private static List<Hotel> GetHotels()
        {
            //we need to get the list of hotels from http://localhost:3000/hotels
            RestRequest request = new RestRequest(API_URL + "hotels");
            IRestResponse<List<Hotel>> response = client.Get<List<Hotel>>(request);           
            return response.Data; 
        }

        private static List<Review> GetReviews()
        {
            //we need to get the list of hotels from http://localhost:3000/hotels
            RestRequest request = new RestRequest(API_URL + "reviews");
            IRestResponse<List<Review>> response = client.Get<List<Review>>(request);
            return response.Data;
        }


        private static Hotel GetHotelById(int id)
        {
            //we need to get the list of hotels from http://localhost:3000/hotels/:id
            RestRequest request = new RestRequest(API_URL + "hotels/"+id);
            IRestResponse<Hotel> response = client.Get<Hotel>(request);
            return response.Data;
        }


        private static List<Review> GetReviewsForHotelId(int id)
        {
            //we need to get the list of hotels from http://localhost:3000/hotels/:id/reviews
            RestRequest request = new RestRequest(API_URL + "hotels/" + id+"/reviews");
            IRestResponse<List<Review>> response = client.Get<List<Review>>(request);
            return response.Data;
        }

        private static List<Hotel> GetHotelsWithStarRating(int rating)
        {
            //we need to get the list of hotels from http://localhost:3000/hotels?stars=3
            RestRequest request = new RestRequest(API_URL + "hotels?stars="+rating);
            IRestResponse<List<Hotel>> response = client.Get<List<Hotel>>(request);
            return response.Data;
        }

        private static City GetCityFromPublicAPI()
        {
            //we need to get the city from https://api.teleport.org/api/cities/geonameid%3A4508722/
            RestRequest request = new RestRequest("https://api.teleport.org/api/cities/geonameid%3A4508722/");
            IRestResponse <City> response = client.Get<City>(request);
            return response.Data;
        }

        //Print methods:

        private static void PrintHotels(List<Hotel> hotels)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Hotels");
            Console.WriteLine("--------------------------------------------");
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine(hotel.Id + ": " + hotel.Name);
            }
        }

        private static void PrintHotel(Hotel hotel)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Hotel Details");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Id: " + hotel.Id);
            Console.WriteLine(" Name: " + hotel.Name);
            Console.WriteLine(" Stars: " + hotel.Stars);
            Console.WriteLine(" Rooms Available: " + hotel.RoomsAvailable);
            Console.WriteLine(" Cover Image: " + hotel.CoverImage);
        }
 
        private static void PrintReviews(List<Review> reviews)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Review Details");
            Console.WriteLine("--------------------------------------------");
            foreach (Review review in reviews)
            {
                Console.WriteLine(" Hotel ID: " + review.HotelID);
                Console.WriteLine(" Title: " + review.Title);
                Console.WriteLine(" Review: " + review.ReviewText);
                Console.WriteLine(" Author: " + review.Author);
                Console.WriteLine(" Stars: " + review.Stars);
                Console.WriteLine("---");
            }
        }
        private static void PrintCity(City city)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("City Details");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Full Name: " + city.Full_name);
            Console.WriteLine(" Geoname Id: " + city.Geoname_id);
        }


    }
}
