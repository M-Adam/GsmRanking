using System;
using System.Collections.Generic;
using System.Linq;
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
            TempData[ViewContextExtension.ErrorKey] = e.GetExceptionMessage();
        }

        public static string GetExceptionMessage(this Exception e)
        {
            return e.InnerException == null ? e.Message : GetExceptionMessage(e.InnerException);
        }
    }
}
