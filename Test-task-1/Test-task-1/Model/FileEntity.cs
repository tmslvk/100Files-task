using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_task_1.Model
{
    public class FileEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LatinChars { get; set; }
        public string CyrillicChars { get; set; }
        public int EvenNumber { get; set; }
        public decimal FloatNumber { get; set; }
    }
}
