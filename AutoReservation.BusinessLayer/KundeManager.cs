namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Kunden.toList();
                }
            }
        }
    }
}