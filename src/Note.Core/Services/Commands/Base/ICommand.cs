namespace Note.Core.Services.Commands.Base
{
    public interface ICommand
    {
        bool IsValid { get; }
    }
}
