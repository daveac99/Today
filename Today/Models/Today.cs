using System;

//see this tutorial https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/


namespace TodayList.Models
{
    public class Today
    {
        public int TodayId { get; set; }
        public bool Done { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DoneDate { get; set; }

    }

  
}
