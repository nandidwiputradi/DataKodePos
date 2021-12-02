using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DataKodePosWeb.API
{
    public class Controller
    {
        BusinessLogic _logic;
        public Controller()
        {
            _logic = new BusinessLogic();
        }
        public dynamic Search()
        {
            dynamic retval = null;
            retval = JsonConvert.SerializeObject(_logic.Search());

            return retval;
        }
        public dynamic GetKelurahanName()
        {
            dynamic retval = null;
            var res = _logic.GetKelurahanName();
            retval = JsonConvert.SerializeObject(res);
            return retval;
        }
        public dynamic Delete()
        {
            dynamic retval = null;
            var res = _logic.Delete();
            retval = JsonConvert.SerializeObject(res);
            return retval;
        }
        public void SaveData()
        {

        }
    }
}