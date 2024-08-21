using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vitecdemo.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a equipment_template entity with essential details
    /// </summary>
    [Table("EQUIPMENT_TEMPLATE", Schema = "dbo")]
    public class EQUIPMENT_TEMPLATE
    {
        /// <summary>
        /// Initializes a new instance of the EQUIPMENT_TEMPLATE class.
        /// </summary>
        public EQUIPMENT_TEMPLATE()
        {
            UPDATED_DATE = DateTime.UtcNow;
            CREATION_DATE = DateTime.UtcNow;
        }

        /// <summary>
        /// Primary key for the EQUIPMENT_TEMPLATE 
        /// </summary>
        [Key]
        [Required]
        public Guid GUID { get; set; }
        /// <summary>
        /// Foreign key referencing the DATA_OWNER to which the EQUIPMENT_TEMPLATE belongs 
        /// </summary>
        public Guid? GUID_DATA_OWNER { get; set; }

        /// <summary>
        /// Navigation property representing the associated DATA_OWNER
        /// </summary>
        [ForeignKey("GUID_DATA_OWNER")]
        public DATA_OWNER? GUID_DATA_OWNER_DATA_OWNER { get; set; }
        /// <summary>
        /// Foreign key referencing the EQUIPMENT_TEMPLATE to which the EQUIPMENT_TEMPLATE belongs 
        /// </summary>
        public Guid? GUID_PARENT_EQUIPMENT_TEMPLATE { get; set; }

        /// <summary>
        /// Navigation property representing the associated EQUIPMENT_TEMPLATE
        /// </summary>
        [ForeignKey("GUID_PARENT_EQUIPMENT_TEMPLATE")]
        public EQUIPMENT_TEMPLATE? GUID_PARENT_EQUIPMENT_TEMPLATE_EQUIPMENT_TEMPLATE { get; set; }
        /// <summary>
        /// Foreign key referencing the EQUIPMENT_CATEGORY to which the EQUIPMENT_TEMPLATE belongs 
        /// </summary>
        public Guid? GUID_EQUIPMENT_CATEGORY { get; set; }

        /// <summary>
        /// Navigation property representing the associated EQUIPMENT_CATEGORY
        /// </summary>
        [ForeignKey("GUID_EQUIPMENT_CATEGORY")]
        public EQUIPMENT_CATEGORY? GUID_EQUIPMENT_CATEGORY_EQUIPMENT_CATEGORY { get; set; }
        /// <summary>
        /// ID of the EQUIPMENT_TEMPLATE 
        /// </summary>
        public string? ID { get; set; }
        /// <summary>
        /// DESCRIPTION of the EQUIPMENT_TEMPLATE 
        /// </summary>
        public string? DESCRIPTION { get; set; }

        /// <summary>
        /// UPDATED_DATE of the EQUIPMENT_TEMPLATE 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UPDATED_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the EQUIPMENT_TEMPLATE belongs 
        /// </summary>
        public Guid? GUID_USER_UPDATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_UPDATED_BY")]
        public USR? GUID_USER_UPDATED_BY_USR { get; set; }

        /// <summary>
        /// CREATION_DATE of the EQUIPMENT_TEMPLATE 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CREATION_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the EQUIPMENT_TEMPLATE belongs 
        /// </summary>
        public Guid? GUID_USER_CREATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_CREATED_BY")]
        public USR? GUID_USER_CREATED_BY_USR { get; set; }
    }
}