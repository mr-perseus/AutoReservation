namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        public List<Reservation> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {

                    Category cat = new Category
                    { };
                        
                    return context.Reservations.ToList();
                }
            }
        }

        
    }
}