using API.Errors;
using API.Helper;
using API.Helpers;
using Aspose.Cells;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Product>>> GetProducts(
         [FromQuery] FilterParams filterParams)
        {
            var totalItems = await _productService.GetProductsCount(filterParams);
            var products = await _productService.GetProducts(filterParams);
            return Ok(new Pagination<Product>(filterParams.PageIndex,
                filterParams.PageSize, totalItems, products));
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportProducts(
[FromQuery] FilterParams filterParams)
        {

            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];

            filterParams.PageSize = int.MaxValue;
            var products = await _productService.GetProducts(filterParams);

            ImportTableOptions imp = new ImportTableOptions();
            imp.InsertRows = true;

            // Importing the array of names to 1st row and first column vertically
            worksheet.Cells.ImportCustomObjects((System.Collections.ICollection)products, 0, 0, imp);

            MemoryStream ms = new MemoryStream();
            SaveFormat svfmt = (SaveFormat)workbook.FileFormat;
            workbook.Save(ms, svfmt);
            ms.Position = 0;
            string excelName = $"Cisco_Products_" + DateTime.Now.ToString("dd MMM yyyy") + ".xlsx";
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }


        [HttpPost("SaveBulkData"), DisableRequestSizeLimit]
        public async Task<IActionResult> SaveBulkData()
        {
            var formCollection = await Request.ReadFormAsync();
            var formFile = formCollection.Files.First();
            if (formFile == null || formFile.Length <= 0)
                return BadRequest(new ApiResponse(400, "File is empty"));
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new ApiResponse(400, "Not Support file extension"));

            using (var stream = new MemoryStream())
            {

                await formFile.CopyToAsync(stream);
                Workbook workbook = new Workbook(stream);

                List<Task> TaskList = new List<Task>();
                List<DataRow> dataRows = new List<DataRow>();
                List<Product> products = new List<Product>();

                var worksheets = workbook.Worksheets.Where(x => x.Name != "SKU Finder").ToList();

                foreach (var worksheet in worksheets)
                {
                    Task task = new Task(() => dataRows.AddRange(ExcelHelper.GetDataRows(worksheet)));
                    task.Start();
                    TaskList.Add(task);
                }

                Task.WaitAll(TaskList.ToArray());

                products = _mapper.Map<List<DataRow>, List<Product>>(dataRows);

                await _productService.SaveProducts(products);
            }

            return Ok(true);
        }
    }
}
