using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopOut.Player.Players
{
    public interface IVideo
    {
        string Title { get; set; }
        string Url { get; set; }
    }
}
