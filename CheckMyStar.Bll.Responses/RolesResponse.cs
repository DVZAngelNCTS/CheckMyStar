using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class RolesResponse : BaseResponse
    {
        public List<RoleModel>? Roles { get; set; }
    }
}
