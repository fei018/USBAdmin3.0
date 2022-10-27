﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using USBModel;

namespace USBAdminWebMVC.Controllers
{
    [Authorize]
    public class USBController : Controller
    {
        private readonly USBDBHelp _usbDb;
        private readonly EmailHelp _email;
        private readonly HttpContext _httpContext;

        public USBController(USBDBHelp usbDb, EmailHelp emailHelp, IHttpContextAccessor httpContextAccessor)
        {
            _usbDb = usbDb;          
            _email = emailHelp;
            _httpContext = httpContextAccessor.HttpContext;
        }


        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUsb(Tbl_UsbRequest usb)
        {
            try
            {
                if (usb != null)
                {
                    await _usbDb.UsbRequest_Register(usb);
                    return Content("Register Success.");
                }
                else
                {
                    return Content("Tbl_UsbRequest is Null.");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion


        // UsbRequest

        #region UsbRequest()
        public IActionResult UsbRequest()
        {
            return View();
        }
        #endregion

        #region RequestDetail(int id)
        public async Task<IActionResult> RequestDetail(int id)
        {
            try
            {
                var query = await _usbDb.UsbRequestVM_Get_ById(id);
                return View(query);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
        #endregion

        #region RequestReview
        public IActionResult RequestReview()
        {
            return View();
        }

        public async Task<IActionResult> RequestReviewList(int page, int limit)
        {
            try
            {
                var (total, list) = await _usbDb.UsbRequestVM_Get_ByStateType(page, limit, UsbRequestStateType.UnderReview);
                return JsonResultHelp.LayuiTableData(total, list);
            }
            catch (Exception ex)
            {
                return JsonResultHelp.LayuiTableData(ex.Message);
            }
        }
        #endregion

        #region RequestApprove
        public IActionResult RequestApprove()
        {
            return View();
        }

        public async Task<IActionResult> RequestApproveList(int page, int limit)
        {
            try
            {
                var (total, list) = await _usbDb.UsbRequestVM_Get_ByStateType(page, limit, UsbRequestStateType.Approve);
                return JsonResultHelp.LayuiTableData(total, list);
            }
            catch (Exception ex)
            {
                return JsonResultHelp.LayuiTableData(ex.Message);
            }
        }
        #endregion

        #region RequestReject
        public IActionResult RequestReject()
        {
            return View();
        }

        public async Task<IActionResult> RequestRejectList(int page, int limit)
        {
            try
            {
                var (total, list) = await _usbDb.UsbRequestVM_Get_ByStateType(page, limit, UsbRequestStateType.Reject);
                return JsonResultHelp.LayuiTableData(total, list);
            }
            catch (Exception ex)
            {
                return JsonResultHelp.LayuiTableData(ex.Message);
            }
        }
        #endregion

        #region RequestToApprove(int id)
        [HttpPost]
        public async Task<IActionResult> RequestToApprove(int id)
        {
            try
            {
                var usb = await _usbDb.UsbRequest_Get_ById(id);
                usb.RequestStateChangeBy = _httpContext.User?.Identity?.Name;

                await _usbDb.UsbRequest_ToApprove(usb);

                await _email.SendToUser_UsbReuqest_Result(usb);

                //ViewBag.OK = "Approve Succeed: " + usb.ToString();
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region RequestToReject(Tbl_UsbRequest usbRequest)
        [HttpPost]
        public async Task<IActionResult> RequestToReject(int id, string RejectReason)
        {
            try
            {
                var usb = await _usbDb.UsbRequest_Get_ById(id);
                usb.RejectReason = RejectReason;
                usb.RequestStateChangeBy = _httpContext.User?.Identity?.Name;

                await _usbDb.UsbRequest_ToReject(usb);

                await _email.SendToUser_UsbReuqest_Result(usb);

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region RequestToDelete(int id)
        [HttpPost]
        public async Task<IActionResult> RequestToDelete(int id)
        {
            try
            {
                await _usbDb.UsbRequest_Delete_ById(id);

                //ViewBag.OK = "Delete Succeed.";

                return Content("Delete Succeed.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
