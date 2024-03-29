﻿using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class UserCard: BaseEntity
    {
        public Guid ?UserId { get; set; }
        public Guid ?CardId { get; set; }
        public virtual Card? Card { get; set; }
        public virtual User? User { get; set; }
    }
}
