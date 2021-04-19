namespace API.Models
{
    public class Rating
    {
        public int RatingId{get;set;}
        public int RatingScore{get;set;}

        public int ProductID{get;set;}
        public Product Product{get;set;}
        public string UserName{get;set;}
    }
}