namespace AssignmentFlightsApp.Models
{
    // Represents a flight within the flight booking application.
    // Contains information about a single flight including its code, operating airline, departure and arrival locations,
    // day of operation, time, available seats, and the cost per seat.
    public class Flight
    {
        // Unique code identifying the flight, typically provided by the airline.
        public string FlightCode { get; set; }

        // The name of the airline operating the flight.
        public string Airline { get; set; }

        // Departure airport code or location.
        public string From { get; set; }

        // Arrival airport code or location.
        public string To { get; set; }

        // Day of the week the flight operates.
        public string Weekday { get; set; }

        // Scheduled departure time.
        public string Time { get; set; }

        // Total number of seats available on the flight.
        public int Seats { get; set; }

        // Cost of a single seat on the flight.
        public double CostPerSeat { get; set; }

        // Optional reservation code associated with the flight, mainly used when a flight is linked to a specific reservation.
        // This property is set internally within the application logic.
        public string? ReservationCode { get; internal set; }

        // Constructor to initialize a new Flight object with provided details.
        public Flight(string flightCode, string airline, string from, string to, string weekday, string time, int seats, double costPerSeat)
        {
            FlightCode = flightCode;
            Airline = airline;
            From = from;
            To = to;
            Weekday = weekday;
            Time = time;
            Seats = seats;
            CostPerSeat = costPerSeat;
        }
    }
}
