using System;
using System.Collections.Generic;

namespace INFI.Web.Common.Module
{
    public class UserInfo
    {
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
        public DateTime LoggedInAt { get; set; }
        public List<RoleInfo> RoleList { get; set; }
        public List<ZionDto> ZionList { get; set; }
        public List<DepartmentDto> DepartmentList { get; set; }
    }
    public class RoleInfo
    {
        public int DisplaySeq { get; set; }
        public int? ParentId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
    public class ZionDto
    {
        public ZionDto()
        {
            AreaList = new List<AreaDto>();
        }
        public string QId { get; set; }
        public string QCode { get; set; }
        public string QName { get; set; }
        public List<AreaDto> AreaList { get; set; }
    }
    public class AreaDto
    {
        public AreaDto()
        {
            ServerList = new List<ServerDto>();
        }
        public string AId { get; set; }
        public string ACode { get; set; }
        public string AName { get; set; }
        public string QId { get; set; }
        public List<ServerDto> ServerList { get; set; }
    }
    public class ServerDto
    {
        public string SId { get; set; }
        public string SCode { get; set; }
        public string SName { get; set; }
        public string AId { get; set; }
    }
    public class DepartmentDto
    {
        public string DId { get; set; }
        public string DName { get; set; }
    }
}
