namespace FlightDocument.Utility
{
    //Static details
    public class SD
    {
        public static string DocumentAPIBase {  get; set; }
        public enum ApiType
        {
            GET, 
            POST, 
            PUT, 
            DELETE
        }
    }
}
