using Application.MapProfile;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    [TestFixture]
    public class ApplicationAutoMapperProfilesConfigurationIsValidTests
    {
        private static IEnumerable<MapperConfiguration> TestCases()
        {
            yield return CreateConfiguration(
              new EntityToDtoMap());
        }

        [TestCaseSource($"{nameof(TestCases)}")]
        public void ConfigurationTest(MapperConfiguration configuration)
        {
            IMapper mapper = configuration.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        protected static MapperConfiguration CreateConfiguration(params Profile[] profiles)
        {
            return new MapperConfiguration((Action<IMapperConfigurationExpression>)(x => x.AddProfiles((IEnumerable<Profile>)profiles)));
        }
    }
}
