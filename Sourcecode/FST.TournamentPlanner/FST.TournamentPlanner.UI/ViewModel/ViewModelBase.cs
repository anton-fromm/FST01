using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class ViewModelBase<TEntity> : GalaSoft.MvvmLight.ViewModelBase
    {
        internal TEntity Model;

        public ViewModelBase(TEntity model)
        {
            Model = model;
        }
    }
}
