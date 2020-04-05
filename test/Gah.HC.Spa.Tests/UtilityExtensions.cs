namespace Gah.HC.Spa.Tests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Moq;
    using System;

    /// <summary>
    /// Class UtilityExtensions.
    /// </summary>
    public static class UtilityExtensions
    {
        /// <summary>
        /// Sets the default context.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="objectValidatorMock">The object validator mock.</param>
        public static void SetDefaultContext(this ControllerBase controller, Mock<IObjectModelValidator> objectValidatorMock = null)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            objectValidatorMock ??= new Mock<IObjectModelValidator>(MockBehavior.Loose);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
            };
            controller.ObjectValidator = objectValidatorMock.Object;
        }

        /// <summary>
        /// Sets the state of the failed model.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public static void SetFailedModelState(this ControllerBase controller)
        {
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(x => x.Validate(It.IsAny<ActionContext>(), It.IsAny<ValidationStateDictionary>(), It.IsAny<string>(), It.IsAny<object>()))
                .Callback<ActionContext, ValidationStateDictionary, string, object>((ac, _, __, ___) => ac.ModelState.AddModelError("Name", "oops"));
            controller.SetDefaultContext(objectValidator);
        }
    }
}
