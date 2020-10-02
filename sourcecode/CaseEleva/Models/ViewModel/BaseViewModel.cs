using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseEleva.Models.ViewModel
{
    public class BaseViewModel
    {

        public BaseViewModel(){
            FieldsWithError = new List<string>();
        }

        public bool StatusOperation { get; set; }
        public string StatusMessage { get; set; }
        public List<string> FieldsWithError { get; set; }
    }
}