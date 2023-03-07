using MessageService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Controllers
{
    /// <summary>
    /// Класс обработчика сообщений.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        /// <summary>
        /// Максимальное число пользователей для инициализации.
        /// </summary>
        private readonly int _baseCount = 10;
        /// <summary>
        /// Хранилище данных пользователей.
        /// </summary>
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// Конструктор обработчика сообщений.
        /// </summary>
        /// <param name="messageRepository"> Хранилище данных сообщений </param>
        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _messageRepository.ReadJsonFile();
        }

        // 2

        /// <summary>
        /// Инициализация списка сообщений
        /// </summary>
        /// <returns> Результат запроса </returns>
        [HttpPost("random-messages-base")]
        public ActionResult<IReadOnlyList<Text>> GetNewUsers()
        {
            try
            {
                _messageRepository.ClearList();
                var random = new Random();
                var response = Enumerable.Range(0, random.Next(0, _baseCount))
                    .Select(x => _messageRepository.GetRandomText())
                    .ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 4

        /// <summary>
        /// Получение списка сообщений по идентификатору отправителя и получателя.
        /// </summary>
        /// <param name="senderId"> Идентификатору отправителя </param>
        /// <param name="receiverId"> Идентификатору получателя </param>
        /// <returns> Список сообщений </returns>
        [HttpGet("GetByEmailSenderAndReceiver")]
        public IActionResult GetMessagesListBySenderAndReceiverEmail(string senderId, string receiverId)
        {
            try
            {
                var commonMessages = new List<Text>();
                foreach (var message in _messageRepository.GetAllMessages())
                {
                    if (message.SenderId == senderId && message.ReceiverId == receiverId)
                    {
                        commonMessages.Add(message);
                    }
                }
                if (commonMessages.Count == 0)
                {
                    return NotFound();
                }
                return Ok(commonMessages);
            }            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 5a

        /// <summary>
        /// Получение списка сообщений по идентификатору отправителя.
        /// </summary>
        /// <param name="senderId"> Идентификатору отправителя </param>
        /// <returns> Список сообщений </returns>
        [HttpGet("GetByEmailSender/{senderId}")]
        public IActionResult GetMessagesListBySenderEmail(string senderId)
        {
            try
            {
                var senderMessages = new List<Text>();
                foreach (var message in _messageRepository.GetAllMessages())
                {
                    if (message.SenderId == senderId)
                    {
                        senderMessages.Add(message);
                    }
                }
                if (senderMessages.Count == 0)
                {
                    return NotFound();
                }
                return Ok(senderMessages);
            }            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 5b

        /// <summary>
        /// Получение списка сообщений по идентификатору получателя.
        /// </summary>
        /// <param name="receiverId"> Идентификатору получателя </param>
        /// <returns> Список сообщений </returns>
        [HttpGet("GetByEmailReceiver/{receiverId}")]
        public IActionResult GetMessagesListByReceiverEmail(string receiverId)
        {
            try
            {
                var receiverMessages = new List<Text>();
                foreach (var message in _messageRepository.GetAllMessages())
                {
                    if (message.ReceiverId == receiverId)
                    {
                        receiverMessages.Add(message);
                    }
                }
                if (receiverMessages.Count == 0)
                {
                    return NotFound();
                }
                return Ok(receiverMessages);
            }            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 8

        /// <summary>
        /// Добавление информации о новом сообщении, 
        /// если отправитель и получатель сообщения – зарегистрированные пользователи.
        /// </summary>
        /// <param name="message"> Новое сообщение </param>
        /// <returns> Сообщение, которое добавляется к имеющемуся списку, если выполнено условие </returns>
        [HttpPost]
        public IActionResult Post(Text message)
        {
            try
            {
                if (_messageRepository.IsUserRegistered(message.SenderId)
                && _messageRepository.IsUserRegistered(message.ReceiverId))
                {
                    _messageRepository.GetText(message);
                    return Ok(message);
                }
                else
                {
                    return BadRequest(message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
