using inceputproiectMds.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models.DTOs
{
    public class ReviewSendDTO
    {
        
        public Guid ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int Stars { get; set; }
        public Guid ProductId { get; set; }
        public Guid? UserId { get; set; }
        public virtual UserDTO? User { get; set; } //un review este pus de un singur user


        public ReviewSendDTO(Review review)
        {
            ReviewId = review.ReviewId;
            Content = review.Content;
            Date = review.Date;
            Stars = review.Stars;
            ProductId = review.ProductId;
            UserId = review.UserId;
            User = new(){
                UserName=review.User.UserName,
                Email=review.User.Email
            };
            
        }
        public ReviewSendDTO() {}

    }
}
