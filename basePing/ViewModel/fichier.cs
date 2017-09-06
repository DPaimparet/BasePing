using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace basePing.ViewModel
{
    public class fichier
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}