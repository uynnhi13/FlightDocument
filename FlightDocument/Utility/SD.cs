namespace FlightDocument.Utility
{
    //Static details
    public class SD
    {
        public static string DocumentAPIBase {  get; set; }
        public static string AuthAPIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleStaff = "STAFF";
        public const string TokenCookie = "JWToken";
        public enum ApiType
        {
            GET, 
            POST, 
            PUT, 
            DELETE
        }
    }
}
