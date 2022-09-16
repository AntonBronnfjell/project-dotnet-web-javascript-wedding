using Microsoft.AspNetCore.Mvc;

namespace Wedding.Models.Request
{
    [BindProperties]
    public class InvitationRequest
    {
        public int Id { get; set; }
    }
}
