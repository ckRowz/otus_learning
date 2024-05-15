using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualAssistant.API.Data;
using VirtualAssistant.API.Data.Domain;
using VirtualAssistant.API.Models;

namespace VirtualAssistant.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(VirtualAssistantContext context) : ControllerBase
    {
        /// <summary>
        /// Получить пользователя
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id, CancellationToken token)
        {
            var user = await context.Users.FindAsync([id], cancellationToken: token);

            if (user == default)
                return NoContent();

            return Ok(CreateFromDomain(user));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UserDto>>> All(CancellationToken token)
            => new(await context.Users.Select(x => CreateFromDomain(x)).ToListAsync(token));

        /// <summary>
        /// Создать пользователя
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto user, CancellationToken token)
        {
            var createdUser = await context.Users.AddAsync(CreateFromDto(user), token);
            _ = await context.SaveChangesAsync(token);

            return CreatedAtAction(nameof(CreateUser), CreateFromDomain(createdUser.Entity));
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto user, CancellationToken token)
        {
            if (user is { Id: < 1 })
                return BadRequest("Не указан Id");

            var affectedCount = await context.Users.Where(x => x.Id == user.Id).ExecuteUpdateAsync(setters => setters
                    .SetProperty(b => b.Username, user.Username)
                    .SetProperty(b => b.FirstName, user.FirstName)
                    .SetProperty(b => b.LastName, user.LastName)
                    .SetProperty(b => b.Email, user.Email)
                    .SetProperty(b => b.Phone, user.Phone), cancellationToken: token);

            return Ok(new { affectedCount });
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id, CancellationToken token)
        {
            var affectedCount = await context.Users.Where(x => x.Id == id)
                .ExecuteDeleteAsync(token);

            if (affectedCount == 0)
                return NoContent();

            return Ok(new { affectedCount });
        }

        private static UserDto CreateFromDomain(User domain)
            => new()
            {
                Id = domain.Id,
                Username = domain.Username,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = domain.Email,
                Phone = domain.Phone
            };

        private static User CreateFromDto(UserDto dto)
            => new()
            {
                Username = dto.Username,
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone
            };
    }
}
