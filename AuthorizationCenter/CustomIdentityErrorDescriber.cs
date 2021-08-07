using Microsoft.AspNetCore.Identity;

namespace AuthorizationCenter
{
    /// <summary>
    ///     自定义身份验证错误描述
    /// </summary>
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new()
            {
                Code = nameof(DefaultError),
                Description = "发生了未知的故障。请联系管理员"
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new()
            {
                Code = nameof(ConcurrencyFailure),
                Description = "Optimistic concurrency failure, object has been modified."
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new() {Code = nameof(PasswordMismatch), Description = "密码错误"};
        }

        public override IdentityError InvalidToken()
        {
            return new() {Code = nameof(InvalidToken), Description = "无效的Token"};
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new() {Code = nameof(LoginAlreadyAssociated), Description = "用户名已存在"};
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new() {Code = nameof(InvalidUserName), Description = $"用户名 '{userName}' 无效，只能包含字母或数字。"};
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new() {Code = nameof(InvalidEmail), Description = $"电子邮件 '{email}' 无效"};
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new() {Code = nameof(DuplicateUserName), Description = $"用户名 '{userName}' 已存在."};
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() {Code = nameof(DuplicateEmail), Description = $"电子邮件 '{email}' 已存在."};
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new() {Code = nameof(InvalidRoleName), Description = $"Role name '{role}' is invalid."};
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new() {Code = nameof(DuplicateRoleName), Description = $"Role name '{role}' is already taken."};
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new() {Code = nameof(UserAlreadyHasPassword), Description = "User already has a password set."};
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new() {Code = nameof(UserLockoutNotEnabled), Description = "Lockout is not enabled for this user."};
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new() {Code = nameof(UserAlreadyInRole), Description = $"User already in role '{role}'."};
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new() {Code = nameof(UserNotInRole), Description = $"User is not in role '{role}'."};
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() {Code = nameof(PasswordTooShort), Description = $"密码必须至少 {length} 位."};
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() {Code = nameof(PasswordRequiresNonAlphanumeric), Description = "密码必须至少有一个非字母数字字符."};
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new() {Code = nameof(PasswordRequiresDigit), Description = "密码必须至少有一个数字 ('0'-'9')."};
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new() {Code = nameof(PasswordRequiresLower), Description = "密码必须至少有一个小写字母 ('a'-'z')."};
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new() {Code = nameof(PasswordRequiresUpper), Description = "密码必须至少有一个大写字母 ('A'-'Z')."};
        }
    }
}