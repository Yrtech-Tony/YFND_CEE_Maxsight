using System.Collections.Generic;

namespace INFI.API.Models.UsersDto
{
    public class UserRoleDto
    {
        public UserRoleDto()
        {
            RoleList = new List<RoleDto>();
            ZionList = new List<ZionDto>();
            DepartmentList = new List<DepartmentDto>();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string UserTypeName { get; set; }
        public string DisName { get; set; }
        public string OrgZionName { get; set; }
        public string OrgZionId { get; set; }
        public string OrgAreaName { get; set; }
        public string OrgAreaId { get; set; }
        public string OrgServerName { get; set; }
        public string OrgServerId { get; set; }
        public string OrgDepartmentName { get; set; }
        public string OrgDepartmentId { get; set; }
        public List<RoleDto> RoleList { get; set; }
        public List<ZionDto> ZionList { get; set; }
        public List<DepartmentDto> DepartmentList { get; set; }
    }
    public class RoleDto
    {
        public int DisplaySeq { get; set; }
        public int? ParentId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
