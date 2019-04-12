using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("ItemTest", Schema = "test")]
    public class Item : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
