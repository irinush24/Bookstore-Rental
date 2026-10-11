using SharpQuest.Contracts;

namespace Capstone.Core.Models.Interfaces;

public interface IUser
{
    string UserName { get; }

    void Borrow(IItem item);

    void Return(IItem item);
}
