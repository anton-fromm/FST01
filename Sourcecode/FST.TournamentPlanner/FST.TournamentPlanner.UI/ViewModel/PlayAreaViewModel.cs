using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class PlayAreaViewModel : ViewModelBase<Model.Models.PlayArea>
    {
        public PlayAreaViewModel(Model.Models.PlayArea playArea) : base(playArea)
        {

        }

        public int Id
        {
            get
            {
                return _model.Id.Value;
            }
        }

        public string Name
        {
            get
            {
                return _model.Name;
            }
        }

        public string Description
        {
            get
            {
                return _model.Description;
            }
        }
    }
}
