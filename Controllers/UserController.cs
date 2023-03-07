using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MessageService.Repositories;

namespace MessageService.Controllers
{
    /// <summary>
    /// Класс обработчика пользователей.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// Максимальное число пользователей для инициализации.
        /// </summary>
        private readonly int _baseCount = 10;
        /// <summary>
        /// Хранилище данных пользователей.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор обработчика пользователей.
        /// </summary>
        /// <param name="userRepository"> Хранилище данных пользователей </param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userRepository.ReadJsonFile();
        }

        // 2

        /// <summary>
        /// Инициализация списка пользователей
        /// </summary>
        /// <returns> Результат запроса </returns>
        [HttpPost("random-users-base")]
        public ActionResult<IReadOnlyList<User>> GetNewUsers()
        {
            try
            {
                _userRepository.ClearList();
                var random = new Random();
                var response = Enumerable.Range(0, random.Next(0, _baseCount))
                    .Select(x => _userRepository.GetRandomUser())
                    .ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 3a

        /// <summary>
        /// Получение пользователя по уникальному email.
        /// </summary>
        /// <param name="email"> Идентификатор пользователя </param>
        /// <returns> Информация о пользователе </returns>
        [HttpGet("GetByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var user = _userRepository.GetAllUsers().SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 3b 

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <returns> Список пользователей </returns>
        [HttpGet]
        public IReadOnlyList<User> GetUsersList()
        {
            try
            {
                return _userRepository.GetAllUsers();                
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        // 7

        /// <summary>
        /// Регистрация новых пользователей
        /// </summary>
        /// <param name="count"> Кол-во добавляемых пользователей (не более 100) </param>
        /// <returns> Список новых пользователей, который добавляется к имеющемуся списку </returns>
        [HttpPost("random-user")]
        public ActionResult<IReadOnlyList<User>> GetNewUsers(int count)
        {
            try
            {
                if (count > 100)
                {
                    return BadRequest(count);
                }
                var response = Enumerable.Range(0, count)
                    .Select(x => _userRepository.GetRandomUser())
                    .ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 9

        /// <summary>
        /// Постраничная выборка пользователей
        /// </summary>
        /// <param name="limit"> Максимальное кол-во пользователей </param>
        /// <param name="offset"> Порядковый номер пользователя, начиная с которого 
        /// необходимо получать информацию (индексация с 0) </param>
        /// <returns> Список пользователей </returns>
        [HttpGet("choose-users")]
        public ActionResult<IReadOnlyList<User>> GetArchiveForecasts(int limit, int offset)
        {
            try
            {
                var response = _userRepository
                .GetAllUsers()
                .Skip(offset)
                .Take(limit)
                .ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
