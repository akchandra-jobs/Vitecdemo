using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vitecdemo.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a periodic_task_x_standard_text entity with essential details
    /// </summary>
    [Table("PERIODIC_TASK_X_STANDARD_TEXT", Schema = "dbo")]
    public class PERIODIC_TASK_X_STANDARD_TEXT
    {
        /// <summary>
        /// Initializes a new instance of the PERIODIC_TASK_X_STANDARD_TEXT class.
        /// </summary>
        public PERIODIC_TASK_X_STANDARD_TEXT()
        {
            UPDATED_DATE = DateTime.UtcNow;
            CREATION_DATE = DateTime.UtcNow;
        }

        /// <summary>
        /// Primary key for the PERIODIC_TASK_X_STANDARD_TEXT 
        /// </summary>
        [Key]
        [Required]
        public Guid GUID { get; set; }
        /// <summary>
        /// Foreign key referencing the DATA_OWNER to which the PERIODIC_TASK_X_STANDARD_TEXT belongs 
        /// </summary>
        public Guid? GUID_DATA_OWNER { get; set; }

        /// <summary>
        /// Navigation property representing the associated DATA_OWNER
        /// </summary>
        [ForeignKey("GUID_DATA_OWNER")]
        public DATA_OWNER? GUID_DATA_OWNER_DATA_OWNER { get; set; }
        /// <summary>
        /// Foreign key referencing the PERIODIC_TASK to which the PERIODIC_TASK_X_STANDARD_TEXT belongs 
        /// </summary>
        public Guid? GUID_PERIODIC_TASK { get; set; }

        /// <summary>
        /// Navigation property representing the associated PERIODIC_TASK
        /// </summary>
        [ForeignKey("GUID_PERIODIC_TASK")]
        public PERIODIC_TASK? GUID_PERIODIC_TASK_PERIODIC_TASK { get; set; }
        /// <summary>
        /// Foreign key referencing the STANDARD_TEXT to which the PERIODIC_TASK_X_STANDARD_TEXT belongs 
        /// </summary>
        public Guid? GUID_STANDARD_TEXT { get; set; }

        /// <summary>
        /// Navigation property representing the associated STANDARD_TEXT
        /// </summary>
        [ForeignKey("GUID_STANDARD_TEXT")]
        public STANDARD_TEXT? GUID_STANDARD_TEXT_STANDARD_TEXT { get; set; }

        /// <summary>
        /// UPDATED_DATE of the PERIODIC_TASK_X_STANDARD_TEXT 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UPDATED_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the PERIODIC_TASK_X_STANDARD_TEXT belongs 
        /// </summary>
        public Guid? GUID_USER_UPDATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_UPDATED_BY")]
        public USR? GUID_USER_UPDATED_BY_USR { get; set; }

        /// <summary>
        /// CREATION_DATE of the PERIODIC_TASK_X_STANDARD_TEXT 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CREATION_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the PERIODIC_TASK_X_STANDARD_TEXT belongs 
        /// </summary>
        public Guid? GUID_USER_CREATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_CREATED_BY")]
        public USR? GUID_USER_CREATED_BY_USR { get; set; }
    }
}