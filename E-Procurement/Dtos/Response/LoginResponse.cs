﻿namespace E_Procurement.Dtos.Response;

public class LoginResponse
{
    public string Email { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
}