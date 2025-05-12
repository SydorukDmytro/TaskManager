﻿namespace TaskManager.dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
