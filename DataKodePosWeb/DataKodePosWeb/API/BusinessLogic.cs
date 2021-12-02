using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataKodePosWeb.API
{
    public class BusinessLogic
    {
        public IRepository _repo;

        public BusinessLogic()
        {
            _repo = new Repository();
        }
        public dynamic Search()
        {
            dynamic retval = null;
            var _type = RequestData.Data.type;
            switch(_type)
            {
                case "0": retval = _repo.GetPropKab(); break;
                case "1": retval = _repo.GetKecamatan(); break;
                case "2": retval = _repo.GetKelurahan(); break;
            }
            
            return retval;
        }
        public dynamic GetKelurahanName()
        {
            dynamic retval = null;

            CommonResult res = new CommonResult();
            res.Data = _repo.GetKelurahanName();
            res.SearchType = "3";
            retval = res;
            return retval;
        }
        public dynamic Delete()
        {
            dynamic retval = null;

            CommonResult res = new CommonResult();
            res.Data = _repo.DeleteKelurahan();
            res.SearchType = "4";
            retval = res;
            return retval;
        }
    }
}