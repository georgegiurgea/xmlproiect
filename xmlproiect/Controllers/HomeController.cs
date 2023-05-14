using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using xmlproiect.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Xml.Xsl;


namespace xmlproiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }



        public IActionResult Index()
        {
            //string xmlFilePath = "C:\\Users\\George\\Desktop\\Lab DWT\\rezervare.xml";
            //ReservationInfo reservationInfo = ParseXml(xmlFilePath);
            return View();
        }
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            ReservationInfo reservationInfo;

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                string content = streamReader.ReadToEnd();

                try
                {
                    if (file.ContentType == "application/xml" || Path.GetExtension(file.FileName).ToLower() == ".xml")
                    {
                        string uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadsFolderPath))
                        {
                            Directory.CreateDirectory(uploadsFolderPath);
                        }

                        string filePath = Path.Combine(uploadsFolderPath, file.FileName);

                        if (!System.IO.File.Exists(filePath))
                        {
                            System.IO.File.WriteAllText(filePath, content);
                        }

                        var xmlSerializer = new XmlSerializer(typeof(ReservationInfo));
                        using (var stringReader = new StringReader(content))
                        {
                            reservationInfo = (ReservationInfo)xmlSerializer.Deserialize(stringReader);
                            ViewData["tip"] = "Fisier este de tip XML";
                            ViewData["UploadedFilePath"] = filePath;
                            return View("Index", reservationInfo);
                        }
                    }
                    else if (file.ContentType == "application/json" || Path.GetExtension(file.FileName).ToLower() == ".json")
                    {
                        reservationInfo = new ReservationInfo { Reservations = JsonConvert.DeserializeObject<List<Reservation>>(content) };
                        ViewData["tip"] = "Fisier este de tip JSON";
                        return View("Index", reservationInfo);
                    }
                    else
                    {
                        throw new Exception("Unsupported file format. Please upload an XML or JSON file.");
                    }
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return View("Index");
                }
            }
        }


        public IActionResult TransformToXslt(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return RedirectToAction("Index");
            }

            string xsltPath = Path.Combine(_env.WebRootPath, "upload", "XMLFile.xslt");
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            using (StringReader sr = new StringReader(System.IO.File.ReadAllText(filePath)))
            {
                using (XmlReader xr = XmlReader.Create(sr, settings))
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        xslt.Transform(xr, null, sw);
                        ViewData["TransformedContent"] = sw.ToString();

                        // Save the transformed content to a file if it doesn't exist
                        string outputFileName = "TransformedFile.html";
                        string outputPath = Path.Combine(_env.WebRootPath, "upload", outputFileName);

                        if (!System.IO.File.Exists(outputPath))
                        {
                            System.IO.File.WriteAllText(outputPath, sw.ToString());
                        }

                        return View("transform");
                    }
                }
            }
        }



        public IActionResult TransformXml()
        {
            string xmlPath = Path.Combine(_env.WebRootPath, "example.xml");
            string xsltPath = Path.Combine(_env.WebRootPath, "Reservations.xslt");
            string xmlContent = System.IO.File.ReadAllText(xmlPath);
            string xsltContent = System.IO.File.ReadAllText(xsltPath);

            var xslt = new XslCompiledTransform();
            using (var xsltReader = XmlReader.Create(new StringReader(xsltContent)))
            {
                xslt.Load(xsltReader);
            }

            using (var xmlReader = XmlReader.Create(new StringReader(xmlContent)))
            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
            {
                xslt.Transform(xmlReader, xmlWriter);
                ViewBag.TransformedXml = stringWriter.ToString();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public static ReservationInfo ParseXml(string xmlFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReservationInfo));
            ReservationInfo reservationInfo;

            using (StreamReader reader = new StreamReader(xmlFilePath))
            {
                reservationInfo = (ReservationInfo)serializer.Deserialize(reader);
                
            }

            return reservationInfo;
        }
    }
    }