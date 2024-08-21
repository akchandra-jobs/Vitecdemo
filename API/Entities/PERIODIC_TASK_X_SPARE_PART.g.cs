using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vitecdemo.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a periodic_task_x_spare_part entity with essential details
    /// </summary>
    [Table("PERIODIC_TASK_X_SPARE_PART", Schema = "dbo")]
    public class PERIODIC_TASK_X_SPARE_PART
    {
        /// <summary>
        /// Initializes a new instance of the PERIODIC_TASK_X_SPARE_PART class.
        /// </summary>
        public PERIODIC_TASK_X_SPARE_PART()
        {
            UPDATED_DATE = DateTime.UtcNow;
            CREATION_DATE = DateTime.UtcNow;
        }

        /// <summary>
        /// Primary key for the PERIODIC_TASK_X_SPARE_PART 
        /// </summary>
        [Key]
        [Required]
        public Guid GUID { get; set; }

        /// <summary>
        /// Required field QUANTITY of the PERIODIC_TASK_X_SPARE_PART 
        /// </summary>
        [Required]
        public double QUANTITY { get; set; }

        /// <summary>
        /// Required field PRICE of the PERIODIC_TASK_X_SPARE_PART 
        /// </summary>
        [Required]
        public double PRICE { get; set; }

        /// <summary>
        /// UPDATED_DATE of the PERIODIC_TASK_X_SPARE_PART 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UPDATED_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the PERIODIC_TASK_X_SPARE_PART belongs 
        /// </summary>
        public Guid? GUID_USER_UPDATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_UPDATED_BY")]
        public USR? GUID_USER_UPDATED_BY_USR { get; set; }

        /// <summary>
        /// CREATION_DATE of the PERIODIC_TASK_X_SPARE_PART 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CREATION_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the PERIODIC_TASK_X_SPARE_PART belongs 
        /// </summary>
        public Guid? GUID_USER_CREATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_CREATED_BY")]
        public USR? GUID_USER_CREATED_BY_USR { get; set; }
        /// <summary>
        /// Foreign key referencing the DATA_OWNER to which the PERIODIC_TASK_X_SPARE_PART belongs 
        /// </summary>
        public Guid? GUID_DATA_OWNER { get; set; }

        /// <summary>
        /// Navigation property representing the associated DATA_OWNER
        /// </summary>
        [ForeignKey("GUID_DATA_OWNER")]
        public DATA_OWNER? GUID_DATA_OWNER_DATA_OWNER { get; set; }
        /// <summary>
        /// Foreign key referencing the PERIODIC_TASK to which the PERIODIC_TASK_X_SPARE_PART belongs 
        /// </summary>
        public Guid? GUID_PERIODIC_TASK { get; set; }

        /// <summary>
        /// Navigation property representing the associated PERIODIC_TASK
        /// </summary>
        [ForeignKey("GUID_PERIODIC_TASK")]
        public PERIODIC_TASK? GUID_PERIODIC_TASK_PERIODIC_TASK { get; set; }
        /// <summary>
        /// Foreign key referencing the SPARE_PART to which the PERIODIC_TASK_X_SPARE_PART belongs 
        /// </summary>
        public Guid? GUID_SPARE_PART { get; set; }

        /// <summary>
        /// Navigation property representing the associated SPARE_PART
        /// </summary>
        [ForeignKey("GUID_SPARE_PART")]
        public SPARE_PART? GUID_SPARE_PART_SPARE_PART { get; set; }
    }
}