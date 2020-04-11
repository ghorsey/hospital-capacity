namespace Gah.HC.Spa.Models.Shared
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class ChangeMyPasswordInputModel.
    /// </summary>
    public class ChangeMyPasswordInputModel
    {
        /// <summary>
        /// Gets or sets the current password.
        /// </summary>
        /// <value>The current password.</value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets new password.
        /// </summary>
        /// <value>The new password.</value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirm new password.
        /// </summary>
        /// <value>The confirm new password.</value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
