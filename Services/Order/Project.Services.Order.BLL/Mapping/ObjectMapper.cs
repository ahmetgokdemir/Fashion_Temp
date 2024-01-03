using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.BLL.Mapping
{
    //  lazy loading
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>(); // * CustomMapping
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;


        /*
         
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(new_configmap);

        public static IMapper Mapper => lazy.Value;


        public static MapperConfiguration  configmap = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomMapping>();
        });

        public static IMapper new_configmap = configmap.CreateMapper();
        */

        /*
        public static IMapperConfigurationExpression cfg2;
        cfg2.AddProfile<CustomMapping>();
        */



    }
}
