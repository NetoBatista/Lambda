using System;
using System.Collections.Generic;
using System.Text;

namespace SendMail.Model
{
    public class Input
    {
        public string to { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public bool isHtml { get; set; }
    }
}
