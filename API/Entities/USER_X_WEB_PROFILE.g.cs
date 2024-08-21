using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vitecdemo.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a user_x_web_profile entity with essential details
    /// </summary>
    [Table("USER_X_WEB_PROFILE", Schema = "dbo")]
    public class USER_X_WEB_PROFILE
    {
        /// <summary>
        /// Initializes a new instance of the USER_X_WEB_PROFILE class.
        /// </summary>
        public USER_X_WEB_PROFILE()
        {
            INDEX_POSITION = 0;
            UPDATED_DATE = DateTime.UtcNow;
            CREATION_DATE = DateTime.UtcNow;
        }

        /// <summary>
        /// Required field INDEX_POSITION of the USER_X_WEB_PROFILE 
        /// </summary>
        [Required]
        public int INDEX_POSITION { get; set; }

        /// <summary>
        /// Primary key for the USER_X_WEB_PROFILE 
        /// </summary>
        [Key]
        [Required]
        public Guid GUID { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the USER_X_WEB_PROFILE belongs 
        /// </summary>
        public Guid? GUID_USER { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER")]
        public USR? GUID_USER_USR { get; set; }
        /// <summary>
        /// Foreign key referencing the WEB_PROFILE to which the USER_X_WEB_PROFILE belongs 
        /// </summary>
        public Guid? GUID_WEB_PROFILE { get; set; }

        /// <summary>
        /// Navigation property representing the associated WEB_PROFILE
        /// </summary>
        [ForeignKey("GUID_WEB_PROFILE")]
        public WEB_PROFILE? GUID_WEB_PROFILE_WEB_PROFILE { get; set; }

        /// <summary>
        /// UPDATED_DATE of the USER_X_WEB_PROFILE 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UPDATED_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the USER_X_WEB_PROFILE belongs 
        /// </summary>
        public Guid? GUID_USER_UPDATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_UPDATED_BY")]
        public USR? GUID_USER_UPDATED_BY_USR { get; set; }

        /// <summary>
        /// CREATION_DATE of the USER_X_WEB_PROFILE 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CREATION_DATE { get; set; }
        /// <summary>
        /// Foreign key referencing the USR to which the USER_X_WEB_PROFILE belongs 
        /// </summary>
        public Guid? GUID_USER_CREATED_BY { get; set; }

        /// <summary>
        /// Navigation property representing the associated USR
        /// </summary>
        [ForeignKey("GUID_USER_CREATED_BY")]
        public USR? GUID_USER_CREATED_BY_USR { get; set; }
    }
}