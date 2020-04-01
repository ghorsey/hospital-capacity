namespace Gah.HC.Spa.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class LoginModel.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class LoginModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<LoginModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginModel"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        public LoginModel(
            SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The input.</value>
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        /// <summary>
        /// Gets or sets the external logins.
        /// </summary>
        /// <value>The external logins.</value>
#pragma warning disable CA2227 // Collection properties should be read only
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
#pragma warning disable CA1056 // Uri properties should not be strings
        public string ReturnUrl { get; set; } = string.Empty;
#pragma warning restore CA1056 // Uri properties should not be strings

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        [TempData]
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// on get as an asynchronous operation.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
#pragma warning disable CA1054 // Uri parameters should not be strings
        public async Task OnGetAsync(string returnUrl = "")
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
            }

            returnUrl = returnUrl ?? this.Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            this.ReturnUrl = returnUrl;
        }

        /// <summary>
        /// on post as an asynchronous operation.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
#pragma warning disable CA1054 // Uri parameters should not be strings
        public async Task<IActionResult> OnPostAsync(string returnUrl = "")
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(
                    this.Input.Email,
                    this.Input.Password,
                    this.Input.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    return this.LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return this.RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = this.Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("User account locked out.");
                    return this.RedirectToPage("./Lockout");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        /// <summary>
        /// Class InputModel.
        /// </summary>
#pragma warning disable CA1034 // Nested types should not be visible
        public class InputModel
#pragma warning restore CA1034 // Nested types should not be visible
        {
            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            /// <value>The email.</value>
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            /// <value>The password.</value>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets a value indicating whether [remember me].
            /// </summary>
            /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
}
