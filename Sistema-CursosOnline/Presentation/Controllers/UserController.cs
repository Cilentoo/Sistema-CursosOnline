﻿using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.Request;
using Sistema_CursosOnline.Domain.IServices;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _userService.AuthenticateAsync(request.Email, request.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var userDto = new UserDTO
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role.ToString(),
            };

            var user = await _userService.RegisterAsync(userDto, request.Password);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
    {
        try
        {
            await _userService.UpdateUserAsync(id, userDto);
            return Ok(new { Message = "Usuário atualizado com sucesso" });
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("inactivate/{id}")]
    public async Task<IActionResult> InactivateUser(int id)
    {
        try
        {
            await _userService.InactivateUserAsync(id);
            return Ok(new { Message = "User inactivated successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

}
