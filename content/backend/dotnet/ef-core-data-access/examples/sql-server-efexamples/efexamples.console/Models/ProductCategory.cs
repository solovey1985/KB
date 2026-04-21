using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace efexamples.console.Models
{
    [Table("ProductCategory", Schema = "SalesLT")]
    [Index("AK_ProductCategory_rowguid", IsUnique = true)]
    [Index("AK_ProductCategory_Name", IsUnique = true)]
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        [Required]
        [MaxLength(50)] // Assuming the dbo.Name type is a VARCHAR(50) or NVARCHAR(50). Adjust if different.
        public string Name { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
