using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Profile Model.
    /// </summary>
    public sealed class ProfileModel
    {
        /// <summary>
        /// MemberModel Constructor.
        /// </summary>
        /// <param name="id">사용자 식별자.</param>
        /// <param name="username">사용자 이름.</param>
        /// <param name="name">이름.</param>
        /// <param name="profileUri">프로필 주소.</param>
        public ProfileModel(MemberId id, Email username, string name, string profileUri)
        {
            Id = id.ToString();
            Username = username.Value;
            Name = name;
            ProfileUri = profileUri;
        }

        /// <summary>
        /// 사용자 식별자.
        /// </summary>
        [Required] public string Id { get; }

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