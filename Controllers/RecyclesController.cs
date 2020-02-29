using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WardRobe.Data;
using WardRobe.Models;

namespace WardRobe.Views.Recycles
{
    public class RecyclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecyclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recycles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recycle.ToListAsync());
        }

        public async Task<IActionResult> User()
        {
            return View(await _context.Recycle.ToListAsync());
        }

        // GET: Recycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recycle = await _context.Recycle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recycle == null)
            {
                return NotFound();
            }

            return View(recycle);
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            //Link to the appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            //Once link, time to read content from connection string
            CloudStorageAccount objectaccount =
                CloudStorageAccount.Parse(configure["ConnectionStrings:wardrobe4"]);
            CloudBlobClient blobclient = objectaccount.CreateCloudBlobClient();

            //create the container inside the stroage account
            CloudBlobContainer container = blobclient.GetContainerReference("wardrobe");
            return container;
        }

        // GET: Recycles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recycles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type,PhoneNo,Website,Location,ImageUrl,FileName")] Recycle recycle, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                //get temporary filepath 
                var filepath = Path.GetTempFileName();

                foreach (var FormFile in files)
                {
                    //check the file 
                    if (FormFile.Length <= 0)
                    {
                        TempData["message"] = "Please upload an image file";
                    }

                    //If file is valid proceed to transfer data 
                    {
                        //Get the information of the container
                        CloudBlobContainer container = GetCloudBlobContainer();
                        //create the container if not exist in the storage
                        ViewBag.Success = container.CreateIfNotExistsAsync().Result;
                        ViewBag.BlobContainerName = container.Name; //get the container name

                        //Give a name for the blob 
                        CloudBlockBlob blob = container.GetBlockBlobReference(Path.GetFileName(FormFile.FileName));
                        try
                        {
                            using (var stream = FormFile.OpenReadStream())
                            {
                                await blob.UploadFromStreamAsync(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["message"] = ex.ToString();
                        }

                        //get uri of the uploaded blob and save in database
                        var blobUrl = blob.Uri.AbsoluteUri;
                        recycle.ImageUrl = blobUrl.ToString();
                        recycle.FileName = FormFile.FileName.ToString();
                        _context.Add(recycle);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }
                TempData["createmessage"] = "Please upload an image for your clothes";
                return RedirectToAction(nameof(Create));

            }
            return View(recycle);
        }

        // GET: Recycles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recycle = await _context.Recycle.FindAsync(id);
            if (recycle == null)
            {
                return NotFound();
            }
            return View(recycle);
        }

        // POST: Recycles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Type,PhoneNo,Website,Location,ImageUrl,FileName")] Recycle recycle, List<IFormFile> files, string filename)
        {
            if (id != recycle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var filepath = Path.GetTempFileName();

                foreach (var FormFile in files)
                {
                    if (FormFile.Length <= 0)
                    {
                        TempData["editmessage"] = "Please upload a proper image file";
                    }

                    else
                    {
                        if (filename != null)
                        {
                            //delete file from blob
                            CloudBlobContainer container1 = GetCloudBlobContainer();
                            CloudBlockBlob blobfile = container1.GetBlockBlobReference(filename);
                            string name = blobfile.Name;
                            var result = blobfile.DeleteIfExistsAsync().Result;

                            if (result == false)
                            {
                                TempData["message"] = "Unable to delete file";
                            }
                            else
                            {
                                TempData["message"] = "File is deleted";
                            }
                        }
                        //first all, get the container information
                        CloudBlobContainer container = GetCloudBlobContainer();
                        //give a name for the blob
                        CloudBlockBlob blob = container.GetBlockBlobReference(Path.GetFileName(FormFile.FileName));
                        try
                        {
                            using (var stream = FormFile.OpenReadStream())
                            {
                                await blob.UploadFromStreamAsync(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["message"] = ex.ToString();
                        }

                        // get the uri of the specific uploaded blob and save it
                        var blobUrl = blob.Uri.AbsoluteUri;
                        recycle.ImageUrl = blobUrl.ToString();
                        recycle.FileName = FormFile.FileName.ToString();
                        _context.Update(recycle);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }
                try
                {
                    _context.Update(recycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecycleExists(recycle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recycle);
        }

        // GET: Recycles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recycle = await _context.Recycle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recycle == null)
            {
                return NotFound();
            }

            return View(recycle);
        }

        // POST: Recycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filename)
        {
            var recycle = await _context.Recycle.FindAsync(id);

            if (filename != null)
            {
                //delete file from blob
                CloudBlobContainer container = GetCloudBlobContainer();
                CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                string name = blob.Name;
                var result = blob.DeleteIfExistsAsync().Result;

                if (result == false)
                {
                    TempData["message"] = "Unable to delete image";
                }
                else
                {
                    TempData["message"] = "Image is deleted";
                }
            }

            _context.Recycle.Remove(recycle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecycleExists(int id)
        {
            return _context.Recycle.Any(e => e.ID == id);
        }
    }
}
