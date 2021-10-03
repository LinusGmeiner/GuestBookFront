using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookFront.Model
{
    public class GuestBookEntry
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Visitor { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }

    public class GuestBookList
    {
        public IList<GuestBookEntry> Entries { get; set; }
    }
}
