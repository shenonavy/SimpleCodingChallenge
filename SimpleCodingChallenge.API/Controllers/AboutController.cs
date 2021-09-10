using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleCodingChallenge.API.Models;
using SimpleCodingChallenge.Common.Configuration;

namespace SimpleCodingChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AboutController : Controller
    {
        private readonly SimpleCodingChallengeConfiguration configuration;

        public AboutController(IOptionsSnapshot<SimpleCodingChallengeConfiguration> optionsSnapshot)
        {
            this.configuration = optionsSnapshot.Value;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<AboutDetailsModel> Index()
        {
            return new AboutDetailsModel
            {
                Author = configuration.About.Author,
                FavoriteWebsite = configuration.About.Website
            };
        }
    }
}
