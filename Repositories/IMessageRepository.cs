using System.Collections.Generic;

namespace MessageService.Repositories
{
    /// <summary>
    /// Интерфейс для взаимодействия с хранилищем данных сообщений.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Создаёт новое сообщение и добавляет его в список.
        /// </summary>
        /// <returns> Новое случайное сообщение </returns>
        Text GetRandomText();

        /// <summary>
        /// Возвращает список всех сообщений.
        /// </summary>
        /// <returns> Список сообщений </returns>
        IReadOnlyList<Text> GetAllMessages();

        /// <summary>
        /// Читает json-файл и инициализирует список сообщений.
        /// </summary>
        void ReadJsonFile();

        /// <summary>
        /// Записывает текущее состояние списка в json-файл.
        /// </summary>
        void WriteList();

        /// <summary>
        /// Чистит список сообщений.
        /// </summary>
        void ClearList();

        /// <summary>
        /// Проверяет по идентификатору, зарегистрирован ли пользователь в хранилище данных.
        /// </summary>
        /// <param name="id"> Идентификатор пользователя </param>
        /// <returns> <c>true</c>, если зарегистрирован, иначе <c>false</c> </returns>
        bool IsUserRegistered(string id);

        /// <summary>
        /// Добавляет новое сообщение в список.
        /// </summary>
        /// <param name="message"> Сообщение </param>
        void GetText(Text message);
    }
}
