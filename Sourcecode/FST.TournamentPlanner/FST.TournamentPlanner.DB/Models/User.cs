using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("UserTest", Schema = "test")]

    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Name2 { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
