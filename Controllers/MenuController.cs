using Microsoft.AspNetCore.Mvc;
using PavoWeb.Repository;

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
        private readonly ILogger<MenuController> _logger;
        public MenuController(MenuRepository menuRepository, SocialIconRepository socialIconRepository, ContentRepository contentRepository, StatisticsRepository statisticsRepository, SubscriptionRepository subscriptionRepository, ILogger<MenuController> logger)
        {
            _menuRepository = menuRepository;
            _socialIconRepository = socialIconRepository;
            _contentRepository = contentRepository;
            _statisticsRepository = statisticsRepository;
            _subscriptionRepository = subscriptionRepository;
            _logger = logger;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                var menu = await _menuRepository.GetAllMenu();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while GetAllMenu: {ex.Message}", ex);
                return StatusCode(500, new { Message = "An error occurred while fetching menus.", Details = ex.Message });
            }
        }

        [HttpGet("all-icons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllIcons()
        {
            try
            {
                var socialIcons = await _socialIconRepository.GetAllSocialIcons();
                if (socialIcons == null || !socialIcons.Any())
                    return NotFound(new { Message = "Not found.", StatusCode = 404 });
                return Ok(socialIcons);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while GetAllIcons: {ex.Message}", ex);
                return StatusCode(500, new { Message = "An error occurred while fetching social icons.", Details = ex.Message });
            }
        }

        [HttpGet("contents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllContent()
        {
            try
            {
                var contents = await _contentRepository.GetAllContent();
                if (contents == null || !contents.Any())
                    return NotFound(new { Message = "Not found.", StatusCode = 404 });
                
                return Ok(contents);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while GetAllContent: {ex.Message}", ex);
                return StatusCode(500, new { Message = "An error occurred while fetching contents.", Details = ex.Message});
            }
        }

        [HttpGet("statistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStatistics()
        {
            try
            {
                var statisticsReport = await _statisticsRepository.GetAllStatistics();
                if (statisticsReport == null || !statisticsReport.Any())
                    return NotFound(new { Message = "Not found.", StatusCode = 404 });

                return Ok(statisticsReport);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while GetAllStatistics: {ex.Message}", ex);
                return StatusCode(500, new { Message = "An error occurred while fetching statistics.", Details = ex.Message});
            }
        }

        [HttpGet("subscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSubscriptionDetails()
        {
            try
            {
                var SubscriptionRepository = await _subscriptionRepository.GetAllSubscriptionDetails();
                if (SubscriptionRepository == null || !SubscriptionRepository.Any())
                    return NotFound(new { Message = "Not found.", StatusCode = 404 });
                return Ok(SubscriptionRepository);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while GetAllSubscriptionDetails: {ex.Message}", ex);
                return StatusCode(500, new { Message = "An error occurred while fetching subscription.", Details = ex.Message});
            }
        }

        [HttpGet("feedbackContent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFeedbackContent()
        {
            try
            {
                var feedbackContent = await _contentRepository.GetFeedbackContent();
                if (feedbackContent == null || !feedbackContent.Any())
                    return NotFound(new { Message = "Not found.", StatusCode = 404 });

                return Ok(feedbackContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while GetAllSubscriptionDetails: {ex.Message}", ex);
                return StatusCode(500, new { Message = "An error occurred while fetching feedback contents.", Details = ex.Message });
            }
        }
    }
}
