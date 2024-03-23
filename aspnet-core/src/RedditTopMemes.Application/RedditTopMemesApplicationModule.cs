using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RedditTopMemes.Authorization;
using RedditTopMemes.TopMemes;
using RedditTopMemes.TopMemes.Dto;
using System;

namespace RedditTopMemes
{
    [DependsOn(
        typeof(RedditTopMemesCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class RedditTopMemesApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<RedditTopMemesAuthorizationProvider>();

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<TopMemeItem, TopMemeItemDetailDto>()
                      .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                      .ForMember(u => u.TopMemeId, options => options.MapFrom(input => input.TopMemeId))
                      .ForMember(u => u.MemeId, options => options.MapFrom(input => input.MemeId))
                      .ForMember(u => u.Score, options => options.MapFrom(input => input.Score))
                      .ForMember(u => u.NumComments, options => options.MapFrom(input => input.NumComments))
                      .ForMember(u => u.Title, options => options.MapFrom(input => input.Meme != null ? input.Meme.Title : string.Empty))
                      .ForMember(u => u.Url, options => options.MapFrom(input => input.Meme != null ? input.Meme.Url : string.Empty));
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(RedditTopMemesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
