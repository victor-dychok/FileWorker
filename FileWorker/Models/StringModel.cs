using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker.Models
{
    public class StringModel
    {
        public int Id { get; set; }
        
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Date { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string LatinLetters { get; set; }


        [Column(TypeName = "NVARCHAR")]
        [StringLength(10)]
        public string KirilicLetters { get; set; }

        public int IntegerNumber { get; set; }

        public double RealNumber { get; set; }
    }
}
