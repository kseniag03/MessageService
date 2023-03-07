using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.Repositories
{
    /// <summary>
    /// Класс объекта, который сравнивает пользователей.
    /// </summary>
    public class UserComparer : IComparer<User>
    {
        /// <summary>
        /// Сравнение пользователей по значениям их идентификаторов.
        /// </summary>
        /// <param name="firstUser"> Первый пользователь </param>
        /// <param name="secondUser"> Второй пользователь </param>
        /// <returns></returns>
        public int Compare(User? firstUser, User? secondUser)
        {
            if (firstUser is null || secondUser is null)
                throw new ArgumentException("Некорректное значение параметра");
            return String.Compare(firstUser.Email, secondUser.Email);
        }
    }
}
