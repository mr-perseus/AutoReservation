namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        
        public List<Auto> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Autos.ToList();
                }
            }
        }

    }
}