using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercice02.Models
{
    public class ErrorManager
    {
        public bool success { get; set; }
        public string message { get; set; }
        // Other DynamicAttribute Properties here
        public dynamic data { get; set; }

        public ErrorManager() { }
        public ErrorManager(bool success, string message, dynamic data)
        {
            this.success = success;
            this.message = message;
            this.data = data;
        }
    }
}