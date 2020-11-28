using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyForm
{
    class DiaryData
    {
        public DateTime Date;
        public string Content;
        public string Title;

        public List<string> Tags = new List<string>();
    }
}
