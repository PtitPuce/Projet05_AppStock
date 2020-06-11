using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Infrastructure.Services.Article;
using AppStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace AppStock.Controllers {
    public class CatalogueController : Controller {
        private readonly IArticleService _service_article;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CatalogueController(
                            ApplicationDbContext context,
                            IArticleService service_article,
                            IMapper mapper)
        {
            _context = context;
            _service_article = service_article ??  throw new ArgumentNullException(nameof(service_article));
            _mapper = mapper ??  throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service_article.GetAll());
        }
    }
}