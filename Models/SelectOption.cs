namespace AssignmentFlightsApp.Models
{
    // options for selecting flights
    public class SelectOption
    {
        public string ArrivalAirport { get; set; }
        public string DepartureAirport { get; set; }
        public string Weekday { get; set; }
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string Time { get; set; }
        public double CostPerSeat { get; set; }
        public string Name { get; set; }
        public string Citizenship { get; set; }
        public string ReservationCode { get; set; }
        public string TravelerName { get; set; }
    }

    // reservation for a flight
    public class Reservation
    {
        public string ReservationCode { get; set; }
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string Weekday { get; set; }
        public string Time { get; set; }
        public double CostPerSeat { get; set; }
        public string Name { get; set; }
        public string Citizenship { get; set; }
        public bool IsActive { get; set; } = true; // Default to true

        // Constructor with a Flight object
        public Reservation(string reservationCode, string flightCode, string airline, Flight flight, string name, string citizenship)
        {
            ReservationCode = reservationCode;
            FlightCode = flight.FlightCode;
            Airline = flight.Airline;
            Weekday = flight.Weekday;
            Time = flight.Time;
            CostPerSeat = flight.CostPerSeat;
            // Passenger details
            Name = name;
            Citizenship = citizenship;
            IsActive = true; // Assuming a new reservation is always active initially
        }

        // Constructor without a Flight object
        public Reservation(string reservationCode, string flightCode, string airline, string name, string citizenship)
        {
            ReservationCode = reservationCode;
            FlightCode = flightCode;
            Airline = airline;
            Name = name;
            Citizenship = citizenship;
        }
    }
}