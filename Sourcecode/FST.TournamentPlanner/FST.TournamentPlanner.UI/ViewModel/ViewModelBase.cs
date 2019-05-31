using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class ViewModelBase<TEntity> : GalaSoft.MvvmLight.ViewModelBase
    {
        protected TEntity _model;

        public ViewModelBase(TEntity model)
        {
            _model = model;
        }
    }
}
