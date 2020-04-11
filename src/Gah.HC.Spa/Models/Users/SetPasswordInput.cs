namespace Gah.HC.Spa.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class SetPasswordInput.
    /// </summary>
    public class SetPasswordInput
    {
        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>The new password.</value>
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
