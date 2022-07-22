using MessageService.UI.Dtos;
using MessageService.UI.Repos.IRepos;
using MessageService.UI.StaticDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IMessageRepo _messageRepo;
        private readonly Security _secret;
        public ContactController(IUserRepo userRepo, IMessageRepo messageRepo, IOptions<Security> security)
        {
            _userRepo = userRepo;
            _messageRepo = messageRepo;
            _secret = security.Value;
        }
        [Route("contact-list")]
        public async Task<IActionResult> GetContactList()
        {
            HttpContext.Request.Cookies.TryGetValue("userName", out string value);
            var usersData = await _userRepo.GetList(a => a.Name != value);
            return View(usersData);
        }
        [Route("chat")]
        public async Task<IActionResult> GetChat(int id)
        {
            HttpContext.Request.Cookies.TryGetValue("userId", out string value);
            var urlTo = Endpoints.api + Endpoints.getmessages + id + Endpoints.addFrom + value;
            var urlFrom = Endpoints.api + Endpoints.getmessages + Convert.ToInt32(value) + Endpoints.addFrom + id;

            var resultTo = await _messageRepo.GetMessages(urlTo);
            var resultFrom = await _messageRepo.GetMessages(urlFrom);
            MessageDto messageDto = new MessageDto();
            #region value assignment
            foreach (var itemRT in resultTo.Data)
            {
                InMessageDto inMessageDto = new InMessageDto();
                var content = await _messageRepo.GetMessage(Endpoints.api + Endpoints.getmessage + itemRT.Id);

                inMessageDto.IsDeleted = itemRT.IsDeleted;
                inMessageDto.Id = itemRT.Id;
                inMessageDto.ToId = itemRT.ToId;
                inMessageDto.FromId = itemRT.FromId;
                inMessageDto.RelatedId = itemRT.RelatedId;
                inMessageDto.DateAdded = itemRT.DateAdded;
                inMessageDto.IpAddr = itemRT.IpAddr;
                inMessageDto.IsDeleted = itemRT.IsDeleted;
                inMessageDto.MessageContent = content.Data.MessageContent;
                messageDto.InMessageDtos.Add(inMessageDto);
            }
            
            foreach (var itemFR in resultFrom.Data)
            {
                InMessageDto inMessageDto = new InMessageDto();
                var content = await _messageRepo.GetMessage(Endpoints.api + Endpoints.getmessage + itemFR.Id);

                inMessageDto.IsDeleted = itemFR.IsDeleted;
                inMessageDto.Id = itemFR.Id;
                inMessageDto.ToId = itemFR.ToId;
                inMessageDto.FromId = itemFR.FromId;
                inMessageDto.RelatedId = itemFR.RelatedId;
                inMessageDto.DateAdded = itemFR.DateAdded;
                inMessageDto.IpAddr = itemFR.IpAddr;
                inMessageDto.IsDeleted = itemFR.IsDeleted;
                inMessageDto.MessageContent = content.Data.MessageContent;
                messageDto.InMessageDtos.Add(inMessageDto);
            }

            messageDto.OutMessageDto.ToId = id;
            messageDto.OutMessageDto.FromId = Convert.ToInt32(value);
            messageDto.OutMessageDto.IsDeleted = false;
            messageDto.OutMessageDto.RelatedId =id;
            messageDto.OutMessageDto.ReplyCount = 0;
            messageDto.OutMessageDto.IpAddr = HttpContext.Connection.RemoteIpAddress?.ToString();
            messageDto.OutMessageDto.MessageContent = "";
            messageDto.InMessageDtos = messageDto.InMessageDtos.OrderBy(a => a.RelatedId).ToList();
            #endregion
            return View(messageDto);
        }

        [HttpPost("postchat")]
        public async Task<IActionResult> PostChat(OutMessageDto outMessageDto)
        {
            var result = await _messageRepo.SendMessage(Endpoints.api+Endpoints.sendmessage, outMessageDto);
            var jsonres = JsonConvert.SerializeObject(result);
            return Json(jsonres);
        }
    }
}
