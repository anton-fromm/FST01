using System;
using System.Collections.Generic;
using System.Text;

namespace FST.G1.TournamentPlanner.DB.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
