using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Base
{
    public interface IEntityBase
    {
        [Key]
        int ID { get; set; }
    }
}
