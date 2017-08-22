using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Matches
{
   public interface IMatchRepository
    {
        List<Match> Matches { get; }
    }
}
