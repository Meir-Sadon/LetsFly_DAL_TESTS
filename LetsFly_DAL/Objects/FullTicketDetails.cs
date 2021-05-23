using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL.UserAndPoco
{
    public class FullTicketDetails
    {

        public long Id { get; set; }
        public long FlightId { get; set; }
        public string CompanyName { get; set; }
        public string OriginCountryName { get; set; }
        public string DestinationCountryName { get; set; }

        private DateTime _departureTime;
        public DateTime DepartureTime
        {
            get
            {
                return _departureTime;
            }
            set
            {
                if (_departureTime != DateTime.MinValue && _departureTime >= _landingTime) throw new DepartureTimeTooLateException("Departue Time Must Be Earlier Than Landing Time");
                _departureTime = value;
            }
        }

        private DateTime _landingTime;
        public DateTime LandingTime
        {
            get
            {
                return _landingTime;
            }
            set
            {
                if (_landingTime != DateTime.MinValue && _departureTime >= _landingTime) throw new DepartureTimeTooLateException("Departue Time Must Be Earlier Than Landing Time");
                _landingTime = value;

            }
        }

        public long RemainingTickets { get; set; }

        //Empty Constractor For Read From Sql.
        public FullTicketDetails()
        {
        }

        //Constractor Without Id.
        public FullTicketDetails(long flightId, string companyName, string originCountryName, string destinationCountryName, DateTime departureTime, DateTime landingTime, long remainingTickets)
        {
            FlightId = flightId;
            CompanyName = companyName;
            OriginCountryName = originCountryName;
            DestinationCountryName = destinationCountryName;
            if (departureTime >= landingTime)
                throw new DepartureTimeTooLateException("Departue Time Must Be Earlier Than Landing Time");
            _departureTime = departureTime;
            _landingTime = landingTime;
            if (remainingTickets < 1)
                throw new ArgumentOutOfRangeException("It's Impossible To Create Flight Without Tickets");
            RemainingTickets = remainingTickets;
        }

        // This Function Override The Real Operator == And Check If This.Id And Other.Id Are Equals.
        static public bool operator ==(FullTicketDetails me, FullTicketDetails other)
        {
            if (ReferenceEquals(me, other) || ReferenceEquals(me, null) && ReferenceEquals(other, null))
                return true;
            return false;
        }

        // This Function Override The Real Operator != And Check If This.Id And Other.Id Are NOT Equals.
        static public bool operator !=(FullTicketDetails me, FullTicketDetails other)
        {
            return !(me == other);
        }

        // This Function Override The Real Function Equals And Compair Between This.Id And Other.Id.
        public override bool Equals(object obj)
        {
            FullTicketDetails otherFlight = obj as FullTicketDetails;
            return (this.Id == otherFlight.Id);
        }

        // This Function Override The Real HashCode And Return this Id.
        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Ticket Number: {Id} (Flight Number: {FlightId}). Belong To Company: {CompanyName}. From: {OriginCountryName}. To: {DestinationCountryName}. Departure Time: {DepartureTime}. Landing Time: {LandingTime}. Remaining Tickets: {RemainingTickets}.";
        }
    }
}
