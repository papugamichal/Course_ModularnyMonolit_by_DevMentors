using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Saga.Messages
{
    internal record SendWelcomeMessage(string Email, string FullName) : ICommand;
}
