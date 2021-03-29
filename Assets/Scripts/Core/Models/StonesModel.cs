using BlastGame.Core.Views;
using System.Collections.Generic;
using System.Linq;

namespace BlastGame.Core.Models
{
    public class StonesModel
    {
        public List<StoneView> Stones { get; } = new List<StoneView>();

        public IEnumerable<StoneView> GetStonesWithId(int id)
        {
            return Stones.Where(x => x.Id == id);
        }
    }
}