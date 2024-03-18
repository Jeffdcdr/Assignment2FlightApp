using AssignmentFlightsApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssignmentFlightsApp.Components.Pages
{
    public partial class FlightComponent : ComponentBase
    {
        // List of all flights and matching flights
        private List<Flight> flights = new List<Flight>();
        private List<Flight> matchingFlights = new List<Flight>();

        // HashSet to store unique attributes
        private HashSet<string> departureAirport = new HashSet<string>() { "Any" };
        private HashSet<string> arrivalAirport = new HashSet<string>() { "Any" };
        private HashSet<string> weekdays = new HashSet<string> { "Any", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private HashSet<string> FlightCode = new HashSet<string>() { "Flight" };
        private HashSet<string> Airline = new HashSet<string>() { "Airline" };
        private HashSet<string> Time = new HashSet<string>() { "Time" };

        private HashSet<double> CostPerSeat = new HashSet<double>();

        private SelectOption selectOption = new SelectOption(); // Object to store selected search options

        private string selectedRow = string.Empty;
        private bool flightSearchPerformed = false;
        private bool reservationSearchPerformed = false;

        // This method is called when the component is initialized 
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Long version of getting the lines of data from the flights.csv file
            string resDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/Data");
            string fileName = "flights.csv";
            string filePath = Path.Combine(resDirectory, fileName);

            try
            {
                // This reads all lines from the file which is flights.csv
                string[] lines = File.ReadAllLines(filePath);

                // Iterate through each line
                foreach (string line in lines)
                {
                    // Put the line into an string of array called 'words' then split them using a comma
                    string[] words = line.Trim().Split(',');

                    // then create a new Flight object from the words and add it to the flights list
                    Flight flight = new Flight(words[0], words[1], words[2], words[3], words[4], words[5], int.Parse(words[6]), double.Parse(words[7]));
                    flights.Add(flight);

                    // Add unique values to respective hash sets for filtering options
                    departureAirport.Add(words[2]);
                    arrivalAirport.Add(words[3]);
                    FlightCode.Add(words[0]);
                    Airline.Add(words[1]);
                    Time.Add(words[5]);
                    CostPerSeat.Add(double.Parse(words[7]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
        }

        // Method to search for flights based on selected criteria
        private void SearchFlights()
        {
            // Set flight search performed flag to true
            flightSearchPerformed = true;

            // Filter flights based on selected options
            matchingFlights = flights.Where(f =>
                (selectOption.DepartureAirport == "Any" || f.From == selectOption.DepartureAirport) &&
                (selectOption.ArrivalAirport == "Any" || f.To == selectOption.ArrivalAirport) &&
                (selectOption.Weekday == "Any" || f.Weekday == selectOption.Weekday)).ToList();

            // If there are matching flights, select the first one
            if (matchingFlights.Any())
            {
                var firstMatch = matchingFlights.First();
                selectedFlightCode = firstMatch.FlightCode; // This assumes you have a two-way binding on selectedFlightCode
                                                            // Now, the other fields should automatically update because they are computed properties based on the selected flight code
            }
            else
            {
                selectedFlightCode = null; // Reset the selection
            }

            // Trigger UI update
            StateHasChanged();
        }

        // Method to handle selection of a flight
        private void OnSelectedFlight(Flight flight)
        {
            selectedRow = flight.FlightCode;
        }
    }
}