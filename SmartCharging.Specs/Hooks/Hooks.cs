using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using SmartCharging;
using SmartCharging.Domain.Command.Commands.Group;
using SmartCharging.Specs.API;

namespace SpecFlowCalculator.Specs.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static IHost _host = null!;

        private readonly GroupCommandApi groupCommandApi;
        private readonly GroupQueryApi groupQueryApi;

        public Hooks(GroupCommandApi groupCommandApi, GroupQueryApi groupQueryApi)
        {
            this.groupCommandApi = groupCommandApi;
            this.groupQueryApi = groupQueryApi;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {           
            _host = Program.CreateWebApplication(null);

            _host.Start();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {           
            _host.StopAsync().Wait();
        }

        [AfterScenario]
        public async Task AfterEachScenario()
        {
            var allGroups = await this.groupQueryApi.GetAllGroups();
            foreach (var group in allGroups)
            {
                await this.groupCommandApi.RemoveGroup(new RemoveGroupCommand { Id = group.Id });
            }
        }
    }
}
