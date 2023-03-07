using System.Collections.Generic;

namespace MessageService.Repositories
{
    /// <summary>
    /// Интерфейс для взаимодействия с хранилищем данных пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Создаёт нового пользователя и добавляет его в список.
        /// </summary>
        /// <returns> Нового случайного пользователя </returns>
        User GetRandomUser();

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns> Список пользователей </returns>
        IReadOnlyList<User> GetAllUsers();

        /// <summary>
        /// Читает json-файл и инициализирует список пользователей.
        /// </summary>
        void ReadJsonFile();

        /// <summary>
        /// Сортирует список пользователей и записывает его текущее состояние в json-файл.
        /// </summary>
        void SortList();

        /// <summary>
        /// Чистит список пользователей.
        /// </summary>
        void ClearList();
    }
}
