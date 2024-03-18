using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AssignmentFlightsApp.Models
{
    internal class ReservationManager
    {


        // Defines the path to the reservation.csv file, which is expected to be located in the Resources/Files directory
        private static string Reservation_TXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\reservation.csv");

        // A static Random instance used for generating random numbers, 
        private static Random random = new Random();

        
        // This list is used to hold the current state of all reservations made within the application
        private static List<Reservation> reservations = new List<Reservation>();



        public List<Reservation> FindReservations(string reservationCode, string airline, string name)
        {
            // Initialize a list to hold filtered reservations
            var filteredReservations = new List<Reservation>();

            // retrieves all reservations
            var allReservations = GetReservations();

            // Check if no criteria are specified, return all reservations
            if (string.IsNullOrWhiteSpace(reservationCode) && string.IsNullOrWhiteSpace(airline) && string.IsNullOrWhiteSpace(name))
            {
                return allReservations;
            }

            // Iterate through all reservations
            foreach (var reservation in allReservations)
            {
                // Check if reservation matches any of the specified criteria
                bool matchesReservationCode = !string.IsNullOrWhiteSpace(reservationCode) && reservation.ReservationCode.Contains(reservationCode, StringComparison.OrdinalIgnoreCase);
                bool matchesAirline = !string.IsNullOrWhiteSpace(airline) && reservation.Airline.Contains(airline, StringComparison.OrdinalIgnoreCase);
                bool matchesName = !string.IsNullOrWhiteSpace(name) && reservation.Name.Contains(name, StringComparison.OrdinalIgnoreCase);

                // If any criteria match, add reservation to filtered list
                if (matchesReservationCode || matchesAirline || matchesName)
                {
                    filteredReservations.Add(reservation);
                }
            }

            // Return filtered reservations
            return filteredReservations;
        }

        public string GenerateResCode()
        {
            return GenerateReservationCode();
        }
        public string GenerateReservationCode()
        {
            string reservationCode;

            // Generate a unique reservation code
            do
            {
                char letter = (char)('A' + random.Next(26));
                string numbers = random.Next(1000, 10000).ToString();
                reservationCode = letter + numbers;
            } while (IsCodeGenerated(reservationCode, Reservation_TXT));

            return reservationCode;
        }

        private static bool IsCodeGenerated(string reservationCode, string Reservation_TXT)
        {
            if (!File.Exists(reservationCode))
            {
                return false;
            }

            List<string> existingCode = File.ReadAllLines(Reservation_TXT).ToList();

            return existingCode.Contains(reservationCode);
        }

        public static List<Reservation> GetReservations()
        {
            List<Reservation> res = new List<Reservation>();
            foreach (string line in File.ReadLines(Reservation_TXT))
            {
                string[] parts = line.Split(",");
                string reservationCode = parts[0];
                string flightCode = parts[1];
                string airline = parts[2];
                double cost = double.Parse(parts[3]);
                string name = parts[4];
                string citizenship = parts[5];

                // Create a new Reservation object and add it to the list
                Reservation newReservation = new Reservation(reservationCode, flightCode, airline, name, citizenship);
                res.Add(newReservation);
            }
            reservations = res;
            return res;
        }

        public void AddReservation(Reservation res)
        {
            File.AppendAllText(Reservation_TXT, $"{res.ReservationCode},{res.FlightCode},{res.Airline},{res.CostPerSeat},{res.Name},{res.Citizenship}\n");
        }

    }
}