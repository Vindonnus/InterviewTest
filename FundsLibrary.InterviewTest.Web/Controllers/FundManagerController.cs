using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using PagedList;
using FundsLibrary.InterviewTest.Web.Attributes;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    [Authorize]
    public class FundManagerController : Controller
    {
        private readonly IFundManagerModelRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerModelRepository())
        {}

        public FundManagerController(IFundManagerModelRepository repository)
        {
            _repository = repository;
        }

        // GET: FundManager
         [FundManagerAuthorise(Roles = "Admin, Client")]
        public async Task<ActionResult> GetAll()
        {
            return View(await _repository.GetAll());
        }

        // GET: FundManager/Details/id
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                return View(await _repository.Get(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("FundManagerError", "Unknown Manager");
            }

            return View();
        }

        [FundManagerAuthorise(Roles = "Admin, Client")]
        public async Task<ActionResult> Index(int? page,  string currentOrder, SortDirecton? sortDirection)
        {
            int pageNumber = page ?? 1;
            int pageSize = 4;
            string current = currentOrder;
            var sort = sortDirection ?? SortDirecton.ASC;
            if (string.IsNullOrEmpty(currentOrder))
            {
                current = "Name"; 
            }

            ViewBag.CurrentSort = currentOrder;

            var managers = await _repository.GetPagedFundManager(current, sort);

            return View(managers.ToPagedList(pageNumber, pageSize));
        }

     
        // GET: FundManager/Edit/id
        [FundManagerAuthorise(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.ResultsSaved = false;
            return View(await _repository.Get(id));
        }


        // Update: Fundmanager/Edit/id
        /// <summary>
        /// Edit a fund manager
        /// </summary>
        [HttpPost]
        [FundManagerAuthorise(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ManagedSince,Biography,Location")] FundManagerModel fundsManager)
        {
            ViewBag.ResultsSaved = false;
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(fundsManager);
                    ViewBag.ResultsSaved = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("FundManagerError", "Edit: "+ ex.Message);
                }
            }

            return View(fundsManager);
        }

        [FundManagerAuthorise(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            ViewBag.ResultsSaved = false;
            return View(new FundManagerModel() { ManagedSince = DateTime.Today });
        }

        // Create: Fundmanager/Edit/id
        /// <summary>
        /// Edit a fund manager
        /// </summary>
        [HttpPost]
        [FundManagerAuthorise(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,ManagedSince,Biography,Location")] FundManagerModel fundsManager)
        {
            ViewBag.ResultsSaved = false;
            if (ModelState.IsValid)
            {
                try
                {
                   await _repository.Create(fundsManager);
                   ViewBag.ResultsSaved = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("FundManagerError", "Create: "+ ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
