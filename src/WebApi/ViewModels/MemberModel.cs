using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Member Model.
    /// </summary>
    public sealed class MemberModel
    {
        /// <summary>
        /// MemberModel Constructor.
        /// </summary>
        /// <param name="username">사용자 이름.</param>
        /// <param name="name">이름.</param>
        /// <param name="profileUri">프로필 주소.</param>
        public MemberModel(Email username, string name, string profileUri)
        {
            Username = username.Value;
            Name = name;
            ProfileUri = profileUri;
        }

        /// <summary>
        /// 사용자 이름.
        /// </summary>
        [Required] public string Username { get; }

        /// <summary>
        /// 이름.
        /// </summary>
        [Required] public string Name { get; }

        /// <summary>
        /// 프로필 주소.
        /// </summary>
        [Required] public string ProfileUri { get; }
    }
}