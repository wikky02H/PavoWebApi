using Microsoft.AspNetCore.Mvc;
using PavoWeb.Repository;
using System;
using System.Threading.Tasks;

namespace PavoWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly MenuRepository _menuRepository;
        private readonly SocialIconRepository _socialIconRepository;
        private readonly ContentRepository _contentRepository;
        private readonly StatisticsRepository _statisticsRepository;
        private readonly SubscriptionRepository _subscriptionRepository;

        public MenuController(MenuRepository menuRepository, SocialIconRepository socialIconRepository, ContentRepository contentRepository, StatisticsRepository statisticsRepository, SubscriptionRepository subscriptionRepository)
        {
            _menuRepository = menuRepository;
            _socialIconRepository = socialIconRepository;
            _contentRepository = contentRepository;
            _statisticsRepository = statisticsRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                var menu = await _menuRepository.GetAllMenu();
                return Ok(menu);
                //return Ok(new
                //{
                //    status = 200, // HTTP Status Code for Success
                //    data = menu   // The actual menu data
                //});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching menus.", Details = ex.Message });
            }
        }

        [HttpGet("all-icons")]
        public async Task<IActionResult> GetAllIcons()
        {
            try
            {
                var socialIcons = await _socialIconRepository.GetAllSocialIcons();
                return Ok(socialIcons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching social icons.", Details = ex.Message });
            }
        }

        [HttpGet("contents")]
        public async Task<IActionResult> GetAllContent()
        {
            try
            {
                var socialIcons = await _contentRepository.GetAllContent();
                return Ok(socialIcons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching contents.", Details = ex.Message });
            }
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetAllStatistics()
        {
            try
            {
                var statisticsReport = await _statisticsRepository.GetAllStatistics();
                return Ok(statisticsReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching statistics.", Details = ex.Message });
            }
        }

        [HttpGet("subscription")]
        public async Task<IActionResult> GetAllSubscriptionDetails()
        {
            try
            {
                var SubscriptionRepository = await _subscriptionRepository.GetAllSubscriptionDetails();
                return Ok(SubscriptionRepository);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching subscription.", Details = ex.Message });
            }
        }

        [HttpGet("feedbackContent")]
        public async Task<IActionResult> GetFeedbackContent()
        {
            try
            {
                var feedbackContent = await _contentRepository.GetFeedbackContent();
                return Ok(feedbackContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching feedback contents.", Details = ex.Message });
            }
        }
    }
}
