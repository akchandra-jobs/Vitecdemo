using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vitecdemo.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a layer_group_set entity with essential details
    /// </summary>
    [Table("LAYER_GROUP_SET", Schema = "dbo")]
    public class LAYER_GROUP_SET
    {
        /// <summary>
        /// Initializes a new instance of the LAYER_GROUP_SET class.
        /// </summary>
        public LAYER_GROUP_SET()
        {
            UPDATED_DATE = DateTime.UtcNow;
            CREATION_DATE = DateTime.UtcNow;
        }

        /// <summary>
        /// Primary key for the LAYER_GROUP_SET 
        /// </summary>
        [Key]
        [Required]
        public Guid GUID { get; set; }
        /// <summary>
        /// Foreign key referencing the DATA_OWNER to which the LAYER_GROUP_SET belongs 
        /// </summary>
        public Guid? GUID_DATA_OWNER { get; set; }

        /// <summary>
        /// Navigation property representing the associated DATA_OWNER
        /// </summary>
        [ForeignKey("GUID_DATA_OWNER")]
        public DATA_OWNER? GUID_DATA_OWNER_DATA_OWNER { get; set; }
        /// <summary>
        /// ID of the LAYER_GROUP_SET 
        /// </summary>
        public string? ID { get; set; }

        /// <summary>
        /// UPDATED_DATE of the LAYER_GROUP_SET 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UPDATED_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the LAYER_GROUP_SET belongs 
        /// </summary>
        public Guid? GUID_USER_UPDATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_UPDATED_BY")]
        public USR? GUID_USER_UPDATED_BY_USR { get; set; }

        /// <summary>
        /// CREATION_DATE of the LAYER_GROUP_SET 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CREATION_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the LAYER_GROUP_SET belongs 
        /// </summary>
        public Guid? GUID_USER_CREATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_CREATED_BY")]
        public USR? GUID_USER_CREATED_BY_USR { get; set; }
    }
}