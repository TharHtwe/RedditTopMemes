using Abp.Application.Services.Dto;

namespace RedditTopMemes.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

