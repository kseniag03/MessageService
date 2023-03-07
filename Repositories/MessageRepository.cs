using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace MessageService.Repositories
{
    /// <summary>
    /// Класс хранилища данных сообщений.
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        /// <summary>
        /// Файл для хранения списка сообщений.
        /// </summary>
        internal const string FileNameMessage = "Messages.json";
        /// <summary>
        /// Объект для генерации случайных чисел.
        /// </summary>
        private static readonly Random Random = new();
        /// <summary>
        /// Список возможных почт для идентификатора отправителя и получателя.
        /// </summary>
        private readonly List<string> Emails = new();
        /// <summary>
        /// Список сообщений.
        /// </summary>
        private List<Text> _listOfMessages = new();

        /// <summary>
        /// Возвращает список всех сообщений.
        /// </summary>
        /// <returns> Список сообщений </returns>
        public IReadOnlyList<Text> GetAllMessages()
        {
            WriteList();
            return _listOfMessages;
        }

        /// <summary>
        /// Создаёт новое сообщение и добавляет его в список.
        /// </summary>
        /// <returns> Новое случайное сообщение </returns>
        public Text GetRandomText()
        {
            FillEmails();

            var message = new Text()
            {
                Subject = GetRandomSubject(),
                Message = GetRandomMessage(),
                SenderId = GetSomeUserEmail(),
                ReceiverId = GetSomeUserEmail()
            };

            _listOfMessages.Add(message);
            WriteList();

            return message;
        }

        /// <summary>
        /// Читает json-файл и инициализирует список сообщений.
        /// </summary>
        public void ReadJsonFile()
        {
            using StreamReader sr = new(FileNameMessage);
            string json = sr.ReadToEnd();
            _listOfMessages = JsonSerializer.Deserialize<List<Text>>(json);
        }

        /// <summary>
        /// Записывает текущее состояние списка в json-файл.
        /// </summary>
        public void WriteList()
        {
            using var fs = new FileStream(FileNameMessage, FileMode.Create);
            var formatter = new DataContractJsonSerializer(typeof(List<Text>));
            formatter.WriteObject(fs, _listOfMessages);
        }

        /// <summary>
        /// Чистит список сообщений.
        /// </summary>
        public void ClearList()
        {
            _listOfMessages = new List<Text>();
        }

        /// <summary>
        /// Проверяет по идентификатору, зарегистрирован ли пользователь в хранилище данных.
        /// </summary>
        /// <param name="id"> Идентификатор пользователя </param>
        /// <returns> <c>true</c>, если зарегистрирован, иначе <c>false</c> </returns>
        public bool IsUserRegistered(string id)
        {
            var listOfUsers = ReadUsersJson();
            if (listOfUsers != null && listOfUsers.Count > 0)
            {
                foreach (var user in listOfUsers)
                {
                    if (user.Email == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Добавляет новое сообщение в список.
        /// </summary>
        /// <param name="message"> Сообщение </param>
        public void GetText(Text message)
        {
            _listOfMessages.Add(new Text()
            {
                Subject = message.Subject,
                Message = message.Message,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId
            });
            WriteList();
        }

        /// <summary>
        /// Получает случайно сгенерированную тему сообщения (не осмысленную).
        /// </summary>
        /// <returns> Тему сообщения </returns>
        private static string GetRandomSubject()
        {
            var subject = "";
            var length = Random.Next(1, 11);            
            for (var i = 0; i < length; i++)
            {
                subject += (char)Random.Next('A', 'Z' + 1);
            }
            return subject;
        }

        /// <summary>
        /// Получает случайно сгенерированный текст сообщения (не осмысленный).
        /// </summary>
        /// <returns> Текст сообщения </returns>
        private static string GetRandomMessage()
        {
            var message = "";
            var length = Random.Next(1, 21);
            var alphabetList = new List<int>();
            for (var c = '!'; c <= '~'; c++)
            {
                alphabetList.Add(c);
            }
            int[] alphabet = alphabetList.ToArray();            
            for (var i = 0; i < length; i++)
            {
                message += (char)alphabet[Random.Next(0, alphabet.Length)];
            }
            return message;
        }

        /// <summary>
        /// Читает json-файл и получается список пользователей.
        /// </summary>
        /// <returns> Список пользователей </returns>
        private static List<User> ReadUsersJson()
        {
            using StreamReader sr = new(UserRepository.FileNameUser);
            string json = sr.ReadToEnd();
            return JsonSerializer.Deserialize<List<User>>(json);
        }

        /// <summary>
        /// Заполняет список возможными почтами (случайно сгенерированными с использованием 
        /// генераторов из класса UserRepository или полученными из списка пользователей).
        /// </summary>
        private void FillEmails()
        {
            var listOfUsers = ReadUsersJson();

            if (listOfUsers == null || listOfUsers.Count == 0)
            {
                var length = Random.Next(1, 10);
                for (var i = 0; i < length; i++)
                {
                    Emails.Add(UserRepository.GetRandomEmail(UserRepository.GetRandomName()));
                }
            }
            else
            {
                foreach (var user in listOfUsers)
                {
                    Emails.Add(user.Email);
                }
            }
        }

        /// <summary>
        /// Возвращает случайную почту из списка возможных почт.
        /// </summary>
        /// <returns> Случайная почта </returns>
        private string GetSomeUserEmail()
        {
            return Emails[Random.Next(0, Emails.Count)];
        }
    }
}