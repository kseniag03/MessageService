namespace MessageService
{
    /// <summary>
    /// Класс объекта сообщение.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        public string ReceiverId { get; set; }
    }
}
