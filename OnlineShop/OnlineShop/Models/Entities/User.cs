using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class User: BaseEntity
    {
        public String Email { get; set; }   
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
