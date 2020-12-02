using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TGuidReport.Service.Services;
using TGuidReportModels;
using TGuidReportModels.IRepositories;

namespace TGuidReportAPI.Controllers
{
    [Route("api/[controller]")]
    public class TGuideReportController : Controller
    {
        
        [HttpGet]
        public TGuideConsumerModel GetReport()
        {
            TGuideConsumerModelRepository guideConsumerModelRepository = new TGuideConsumerModelRepository();
            return  guideConsumerModelRepository.Consumer();
        }
    }
}
