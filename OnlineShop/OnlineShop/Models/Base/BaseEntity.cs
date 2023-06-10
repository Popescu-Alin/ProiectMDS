using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Base
{
    public class BaseEntity: IBaseEntity
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
