using DapperFormation.Controllers;
using Xunit;

namespace DapperFormation.Tests
{
    public class ProjetsControllerTest
    {
        [Fact]
        public void ObtenirProjets()
        {
            var controller = new ProjetsController(null);
            var resultat = controller.ObtenirProjets().GetAwaiter().GetResult();

        }
    }
}
