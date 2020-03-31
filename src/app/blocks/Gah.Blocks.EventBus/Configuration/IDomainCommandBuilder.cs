namespace TS.Blocks.DomainCommands.Configuration
{
    using TS.Blocks.DomainCommands;

    /// <summary>
    /// Interface <c>ICommandBuilder</c>.
    /// </summary>
    public interface IDomainCommandBuilder
    {
        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <typeparam name="TCommandHandler">The type of the t command handler.</typeparam>
        /// <returns>A/an <c>ICommandBuilder</c>.</returns>
        IDomainCommandBuilder AddCommand<TCommand, TCommandHandler>()
            where TCommand : IDomainCommand
            where TCommandHandler : class, IDomainCommandHandler<TCommand>;
    }
}
