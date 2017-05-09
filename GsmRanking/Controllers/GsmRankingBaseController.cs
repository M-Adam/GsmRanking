using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GsmRanking.Common;
using Microsoft.AspNetCore.Mvc;

namespace GsmRanking.Controllers
{
    public abstract class GsmRankingBaseController : Controller
    {
        protected void SetSuccess(string message)
        {
            TempData[ViewContextExtension.SuccessKey] = message;
        }

        protected void SetError(Exception e)
        {
            SetError(e.GetExceptionMessage());
        }

        protected void SetError(string error)
        {
            TempData[ViewContextExtension.ErrorKey] = error;
        }

        protected int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
