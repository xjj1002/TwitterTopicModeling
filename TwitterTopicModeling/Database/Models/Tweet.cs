using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


//this namespace and class are used to instacite a tweet and then save it to the database
namespace TwitterTopicModeling.Database.Models
{
    using System;


    public class Tweet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ExternalId { get; set; }
        public string Text { get; set; }
        public TwitterUser TwitterUser { get; set; }

        public string CreatedAt {get; set;}
        

    }
}